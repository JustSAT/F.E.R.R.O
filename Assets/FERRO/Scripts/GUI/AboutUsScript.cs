using UnityEngine;
using System.Collections;

public class AboutUsScript : MonoBehaviour {
    private TextMesh mesh;
    private float myWidth;
	// Use this for initialization
	void Start () {
        mesh = transform.GetComponent<TextMesh>();
	}

    // Update is called once per frame   (mesh.text.Length + mesh.fontSize/10) * mesh.characterSize
	void Update () {
        myWidth = ((mesh.text.Length) * mesh.fontSize * mesh.characterSize * mesh.tabSize) / 1280;
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3((Screen.width - myWidth * Screen.width) / 2, Screen.height - 25, 10)).x, transform.position.y, 0);
	}
}
