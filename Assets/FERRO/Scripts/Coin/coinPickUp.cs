using UnityEngine;
using System.Collections;

public class coinPickUp : MonoBehaviour
{
    enum CoinType
    {
        standartCoin = 10
    };
    CoinType type = CoinType.standartCoin;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.GetComponent<playerScore>().currentScore += (int)CoinType.standartCoin;
            transform.collider2D.enabled = false;
            GameObject.FindGameObjectWithTag("GUI").GetComponent<myGUI>().score.text = other.transform.GetComponent<playerScore>().currentScore.ToString();
            transform.GetChild(0).animation.Play("coin_picked_up");

            StartCoroutine(destroyObj(0.5f));
        }
    }
    IEnumerator destroyObj(float sec)
    {
        yield return new WaitForSeconds(sec);
        GameObject.Destroy(transform.gameObject);

    }
}
