using UnityEngine;
using System.Collections;

public class randomActions : MonoBehaviour {
    public GameObject go;
    public Color[] colors ;
    public float startRandom = 0.3f;
    public float endRandom = -7.7f;
    public float fromHeihgt = 6.12f;
    public float timeToRespawn = 0.5f;
    public float lastBoxPosition;
	// Use this for initialization
	void Start () {
        StartCoroutine(GenerateCube(Random.Range(0, timeToRespawn)));
        colors = new Color[2];
        colors[0] = Color.yellow;
        colors[1] = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            if (GameObject.FindGameObjectWithTag("Player").transform.position.x >= startRandom)
                startRandom = GameObject.FindGameObjectWithTag("Player").transform.position.x - 1.5f;
        }
	}
    public void StartGen()
    {
        StartCoroutine(GenerateCube(Random.RandomRange(0, timeToRespawn)));
    }
    IEnumerator GenerateCube(float s)
    {
        yield return new WaitForSeconds(s);
        GameObject g = Instantiate(go, new Vector3(Random.RandomRange(startRandom, endRandom), Random.RandomRange(fromHeihgt, fromHeihgt+5), 0), Quaternion.EulerAngles(0, 0, Random.RandomRange(0, 360))) as GameObject;
        if (Mathf.Abs(lastBoxPosition - g.transform.position.x) < 0.5f)
            g.transform.position = new Vector3(g.transform.position.x + Random.RandomRange(1.0f, 3.0f), g.transform.position.y, 0);
        int i = (int)Random.Range(0,2);
        g.GetComponent<SpriteRenderer>().color = colors[i];
        g.renderer.material.color = colors[i];
        StartCoroutine(GenerateCube(Random.RandomRange(0, timeToRespawn)));
        Random.seed = Random.RandomRange(0, 1020130);
    }
    
}
