using UnityEngine;
using System.Collections;

public class TriggerObjectDestroyer : MonoBehaviour
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "playerDeathCollider")
        {
            Destroy(other.gameObject);
        }
    }
}
