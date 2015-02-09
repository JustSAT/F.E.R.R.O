using UnityEngine;
using System.Collections;

public class funny : MonoBehaviour {
	int dir = 1;
	bool a = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.forward*Time.deltaTime*dir);
		if (Mathf.Abs (this.transform.position.x) > 4.0f)
						dir *= -1;
	}

	void OnGUI()
	{
		if (GUI.Button (new Rect (10, 10, 100, 50), "Click me!")) {
				if(!a){
					this.transform.renderer.material.color = Color.blue;
					a = true;
				}
				else
				{
					this.transform.renderer.material.color = Color.red;
					a = false;
				}

		}				
	}
}
