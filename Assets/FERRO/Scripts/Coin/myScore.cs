using UnityEngine;
using System.Collections;

public class myScore : MonoBehaviour {
	public GUIText score;
    public Font scoreFont;
	// Use this for initialization
	void Start () {
        score.font = scoreFont;
        score.transform.position = new Vector3(0.01f, 0.99f, 0);
        score.fontSize = 123;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
