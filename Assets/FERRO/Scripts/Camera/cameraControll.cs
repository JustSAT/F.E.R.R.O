using UnityEngine;
using System.Collections;

public class cameraControll : MonoBehaviour {
	public Transform leftBorder;
	public Transform rightBorder;
	public Transform player;
    public float upFollowSpeed = 20.0f;
	// Use this for initialization
	void Start () {
		rightBorder = GameObject.FindGameObjectWithTag ("endCameraPosition").transform;
		leftBorder = GameObject.FindGameObjectWithTag ("startCameraPosition").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs(player.transform.position.x - leftBorder.position.x) > 4.0f && Mathf.Abs(player.transform.position.x - rightBorder.position.x) > 4.0f)
						transform.position = new Vector3 (player.position.x, this.transform.position.y, this.transform.position.z);
       /* if (player.transform.position.y >= 0.0f && player.GetComponent<playerControl>().isGrounded)
        {
            transform.position = new Vector3(this.transform.position.x, player.position.y, this.transform.position.z);
        }*/
	}
}
