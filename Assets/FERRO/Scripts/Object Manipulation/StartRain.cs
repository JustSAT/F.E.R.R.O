using UnityEngine;
using System.Collections;

public class StartRain : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            transform.GetComponent<randomActions>().enabled = true;
            transform.GetComponent<randomActions>().StartGen();
            transform.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
