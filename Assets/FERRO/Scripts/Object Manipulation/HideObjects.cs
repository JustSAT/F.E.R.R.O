using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HideObjects : MonoBehaviour {
    public List<GameObject> objectsForHide;
    public List<GameObject> objectsForActive;
	// Use this for initialization
	void Start () {
        //objectsForHide = new List<GameObject>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach(GameObject i in objectsForHide)
            {
                i.SetActive(false);
            }
            foreach(GameObject i in objectsForActive)
            {
                i.SetActive(true);
            }
            this.active = false;
        }
    }
}
