using UnityEngine;
using System.Collections;

public class cameraGUI : MonoBehaviour {
    public GUIStyle runButt;
    public Transform runButton;
    public float buttonWidth = 10.0f;
    public float buttonHeight = 10.0f;

	// Use this for initialization
	void Start () {
        
        
        Vector3 p = camera.ScreenToWorldPoint(new Vector3((Screen.width  * 0.0971698113207547169811320754717f), (Screen.height * 0.13f), camera.nearClipPlane));
        runButton.position = p;
        runButton.position = new Vector3(runButton.position.x, runButton.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount == 1)
        {
            Touch myTouch = Input.GetTouch(0);
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;
            if (myTouch.phase == TouchPhase.Began)
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    runButton.GetComponent<SpriteRenderer>().color = Color.black;
                    transform.GetComponent<cameraControll>().player.GetComponent<playerControl>().SetRun(true);
                }
            }
            if (myTouch.phase == TouchPhase.Ended)
            {
                runButton.GetComponent<SpriteRenderer>().color = Color.white;
                transform.GetComponent<cameraControll>().player.GetComponent<playerControl>().SetRun(false);
            }
        }
        
	}
    void OnGUI()
    {
        /*buttonHeight = Screen.height * 0.18575851393188854489164086687307f;
        buttonWidth = buttonHeight * 1.3444444444444444444444444444444f;
        if (GUI.RepeatButton(new Rect(0, Screen.height - (buttonHeight - (buttonHeight * 0.08333333333333333333333333333333f)), buttonWidth, buttonHeight), "", runButton))
        {
            transform.GetComponent<cameraControll>().player.GetComponent<playerControl>().SetRun(true);
        }
        else
        {
            transform.GetComponent<cameraControll>().player.GetComponent<playerControl>().SetRun(false);
        }*/
    }
}
