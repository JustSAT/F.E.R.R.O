using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ContentScript : MonoBehaviour {

    private TextMesh mesh;

    public List<GameObject> contents;
    public List<TextMesh> meshes;
    public List<string> content;

    private List<bool> startUnHide;
    public bool unHideAll = false;

    // Use this for initialization
    void Start()
    {
        startUnHide = new List<bool>();
        for (int i = 0; i < 5; i++)
        {
            startUnHide.Add(false);
        }

        content = new List<string>();
        content.Add("Main programmer: Artem Shevchenko");
        content.Add("Programmer: Denys Evtehov");
        content.Add("Main designer: Dmytro Zverev");
        content.Add("Designer: Max Shpetny");
        content.Add("Documentation, programmer's  assistant: Max Lebed");

        meshes = new List<TextMesh>();
        mesh = contents[0].transform.GetComponent<TextMesh>();
        contents[0].transform.renderer.material.color = new Color(0, 0, 0, 0f);
        mesh.text = content[0];
        meshes.Add(mesh);

        for (int i = 1; i <= 4; i++)
        {
            GameObject go = Instantiate(contents[0].transform.gameObject, contents[0].transform.position - new Vector3(0, 0.45f * i, 0), Quaternion.identity) as GameObject;
            go.transform.parent = transform;
            go.transform.renderer.material.color = new Color(0, 0, 0, 0f);
            go.transform.GetComponent<TextMesh>().text = content[i];
            meshes.Add(go.transform.GetComponent<TextMesh>());
            contents.Add(go);
        }
        StartCoroutine(unHideObject(0));
    }

    // Update is called once per frame   (mesh.text.Length + mesh.fontSize/10) * mesh.characterSize
    void Update()
    {
        foreach(GameObject i in contents)
        {
            mesh = i.transform.GetComponent<TextMesh>();
            float myWidth = ((mesh.text.Length) * mesh.fontSize * mesh.characterSize * mesh.tabSize) / 1280;
            i.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(new Vector3((Screen.width - myWidth * Screen.width) / 2, Screen.height - 25, 10)).x, i.transform.position.y, 0);
        }
        for (int i = 0; i < 5; i++)
        {
            if (startUnHide[i])
            {
                if (contents[i].transform.renderer.material.color.a < 1.0f)
                {
                    contents[i].transform.renderer.material.color += new Color(1, 1, 1, 0.008f);
                }
            }
            else
            {
                startUnHide[i] = false;
            }
        }
        if (unHideAll)
        {

            for (int i = 0; i < 5; i++)
            {
                startUnHide[i] = false;
                if (contents[i].transform.renderer.material.color.a > 0.0f)
                {
                    contents[i].transform.renderer.material.color -= new Color(0, 0, 0, 0.01f);
                }
                if (contents[0].transform.renderer.material.color.a <= 0.0f && 
                    contents[1].transform.renderer.material.color.a <= 0.0f &&
                    contents[2].transform.renderer.material.color.a <= 0.0f &&
                    contents[3].transform.renderer.material.color.a <= 0.0f &&
                    contents[4].transform.renderer.material.color.a <= 0.0f)
                    unHideAll = false;

                
            }
            if (transform.renderer.material.color.a > 0.0f)
            {
                transform.renderer.material.color -= new Color(0, 0, 0, 0.01f);
            }
        }
    }

    public IEnumerator unHideObject(int i)
    {
        yield return new WaitForSeconds(0.5f);
        startUnHide[i] = true;
        if(i < 4)
            StartCoroutine(unHideObject(i + 1));
    }
}
