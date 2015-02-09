using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour
{
    private int currentAxis = 1;                //Временная переменная для состояния текущего нарпавления движение персонажа

    private string debugText = "";
    private string debugText2 = "";

    private Rigidbody2D myRigid;                //Ригидбоди которое используется для движения персонажа

    private float jumpSpeed = 40.0f;            //Некая величина указывающая на скорость прыжка
    private float doubleJumpSpeed = 15.0f;
    private float airJumpSpeed = 10.0f;
    private float antiGravity = 9.8f;           //Величина помогающая преодалеть гравитацию
    private float moveSpeed = 3.001f;             //Скорость передвижения персонажа
    private float runSpeed = 6.3f;              //Скорость бега персонажа
    private float slowSlideSpeed;
    public float curSpeed;                     //Текущая скорость передвижения

    public bool isRun = false;                  //Переменная указывающая на то бежит ли персонаж или нет
    public bool isGrounded;                     //- / - на полу ли он
    public bool doubleJump;                     //- / - может ли он делать дабл джамп
    public bool fingerEnded = true;             //- / - поднялся ли палец с екрана
    public bool preSlidePlayer = false;         //Когда true означает что пора менять положение персонажа, помогает ибежать трудностей с переходом состояния персонажа. Запускает Coroutine
    public bool slidePlayer = false;            //- / - находится в состоянии скольжения
    public bool setRun = true;
    public bool finished = false;
    public bool jumpEnded = true;
    public bool jumpStarted = false;

    public bool isPlayerDead = false;           //Мертв ли персонаж

    private bool failStandUp = false;
    Vector2 sPos = new Vector2(0, 0);
    Vector2 curPos = new Vector2(0, 0);
    Animator myAnimator;

    // Use this for initialization
    void Start()
    {
        curSpeed = moveSpeed;
        myAnimator = transform.GetComponent<Animator>();
        slowSlideSpeed = moveSpeed / 2;
        myRigid = this.transform.rigidbody2D;			//Player rigidbody
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    void Update()
    {
        if (!isPlayerDead)
        {
            myAnimator.speed = curSpeed * 0.266666666667f;
            //if left/right shift was pressed we check it for walk or run
            if (!slidePlayer)
            {
                if (setRun)
                {
                    isRun = true;

                    if (curSpeed < runSpeed)
                    {
                        curSpeed += Time.deltaTime * 3;
                        //myAnimator.speed = curSpeed * 0.25f;
                    }
                }
                else
                {
                    isRun = false;
                    if (curSpeed > moveSpeed)
                    {
                        curSpeed -= Time.deltaTime * 4;
                        //myAnimator.speed = curSpeed * 0.25f;
                    }
                    if (curSpeed < moveSpeed)
                    {
                        curSpeed += Time.deltaTime;
                        //myAnimator.speed = curSpeed * 0.25f;
                    }
                }
                if (isGrounded)
                {
                    if (curSpeed >= 3.5f)
                    {
                        myAnimator.Play("sad_run");
                    }
                    if (curSpeed < 3.5f)
                    {
                        myAnimator.Play("sad_walk");
                    }
                }
                else
                {
                    if (transform.rigidbody2D.velocity.y >= 0)
                    {
                        myAnimator.Play("sad_fall_up");
                    }
                    if (transform.rigidbody2D.velocity.y < 0 )
                    {
                        myAnimator.Play("sad_fall");
                    }
                }
            }
            //Check with raycast when player grounded

            if (Physics2D.Raycast(transform.position - new Vector3(transform.localScale.x / 13.5f, 0), -Vector2.up, transform.localScale.x / 2))
            {
                if (!isGrounded)
                {
                    myAnimator.Play("sad_jump_end");

                }
                doubleJump = true;
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
            //Если один палец каснулся екрана, делаем манипуляции с движением этого пальца. Узнаем куда и от куда
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    //Debug.Log ("do began");
                    sPos = touch.position;
                }
                //Если палец делал движение, и он отпущен
                if (touch.phase == TouchPhase.Moved && fingerEnded)
                {
                    curPos = touch.position;
                    //Движение по сенсору снизу вверх
                    if (sPos.y < touch.position.y && touch.position.y - sPos.y > 50.0f && !slidePlayer)
                    {

                        if ((isGrounded || doubleJump) && fingerEnded)
                        {

                            if (!isGrounded)
                                this.transform.rigidbody2D.AddForce(Vector2.up * jumpSpeed * 1.5f * antiGravity); //Add force to rigidbody if double jump
                            else
                                this.transform.rigidbody2D.AddForce(Vector2.up * jumpSpeed * antiGravity); //Add force to rigidbody
                            fingerEnded = false;
                            if (!isGrounded && doubleJump)
                                doubleJump = false;
                        }
                    }
                    //Движение по сенсору сверху вниз
                    if (sPos.y > touch.position.y && sPos.y - touch.position.y > 50.0f && !slidePlayer && curSpeed >= 3.0f)
                    {

                        if (isGrounded && fingerEnded )
                        {
                            fingerEnded = false;
                            preSlidePlayer = true;
                        }
                    }
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerEnded = true;
                }
            }

            //Состояние готовности к проползанию. Включаем анимации. Готовим переменные
            if (preSlidePlayer)
            {
                myAnimator.Play("start_slide");
                slidePlayer = true;
                preSlidePlayer = false;
                StartCoroutine(StandUp(1));
                //transform.GetChild(0).gameObject.SetActive(false);
            }
            //Случай когда персонаж не может встать, т.к. вверху преграда.
            if (failStandUp)
            {
                if (!Physics2D.Raycast(transform.position, Vector2.up, 0.5f) && !Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector2.up, 0.5f) && !Physics2D.Raycast(transform.position - new Vector3(0.5f, 0, 0), Vector2.up, 0.5f))
                {
                    failStandUp = false;
                    StartCoroutine(StandUp(0));
                }
            }
            //Когда пацан скользит замедляем его движение
            if (slidePlayer)
            {
                if (curSpeed > 0.5f)
                    curSpeed -= Time.deltaTime;
            }
            if (!isPlayerDead || finished)
            {
                //Player always run. And i check when he has conflict with any wall.
                if (!slidePlayer && (!Physics2D.Raycast(transform.position - new Vector3(0, transform.localScale.x / 2 - 0.3f, 0), Vector2.right, 0.129f) && !Physics2D.Raycast(transform.position + new Vector3(0, transform.localScale.x / 2 - 0.3f, 0), Vector2.right, 0.129f)))
                    transform.Translate(new Vector3(curSpeed * Time.deltaTime, 0, 0));
                if (slidePlayer && (!Physics2D.Raycast(transform.position, Vector2.right, 0.5f)))
                    transform.Translate(new Vector3(curSpeed * Time.deltaTime, 0, 0));
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if ((isGrounded || doubleJump) && !slidePlayer)
                {
                    if (isGrounded)
                    {
                        myAnimator.Play("sad_jump_start");
                    }
                    if (!isGrounded)
                    {
                        //this.transform.rigidbody2D.AddForce(Vector2.up * jumpSpeed * 1.5f * antiGravity); //Add force to rigidbody if double jump
                        if (Mathf.Abs(transform.rigidbody2D.velocity.y) > 0.12f)
                            transform.rigidbody2D.velocity = new Vector2(transform.rigidbody2D.velocity.x, doubleJumpSpeed);
                        else
                            transform.rigidbody2D.velocity = new Vector2(transform.rigidbody2D.velocity.x, airJumpSpeed);
                    }
                    else
                        this.transform.rigidbody2D.AddForce(Vector2.up * jumpSpeed * antiGravity); //Add force to rigidbody
                    fingerEnded = false;
                    if (!isGrounded && doubleJump)
                        doubleJump = false;
                }
            }
            /*if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
            {
                SetRun(true);
            }
            else
                SetRun(false);*/
            if (Input.GetKey(KeyCode.S) && isGrounded && !slidePlayer && curSpeed >= 3.0f)
            {
                fingerEnded = false;
                preSlidePlayer = true;
            }
        }
    }
    IEnumerator SetStandUp(float sec)
    {
        yield return new WaitForSeconds(sec);
        //transform.GetChild(0).gameObject.SetActive(true);
        slidePlayer = false;
    }
    IEnumerator StandUp(float sec)
    {
        yield return new WaitForSeconds(sec);
        if (!Physics2D.Raycast(transform.position, Vector2.up, 0.5f) && !Physics2D.Raycast(transform.position + new Vector3(0.5f, 0, 0), Vector2.up, 0.5f) && !Physics2D.Raycast(transform.position - new Vector3(0.5f, 0, 0), Vector2.up, 0.5f))
        {
            myAnimator.Play("sad_end_slide");
            StartCoroutine(SetStandUp(0.29f));
        }
        else
        {
            failStandUp = true;
        }
    }
    public void SetRun(bool value)
    {

        setRun = value;
    }
    private void DoVibration()
    {
        
    }
}
