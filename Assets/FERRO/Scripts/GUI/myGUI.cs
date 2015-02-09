using UnityEngine;
using System.Collections;

public class myGUI : MonoBehaviour
{
    public float topPadding = 10.0f;
    public float leftPadding = 10.0f;
    public int fontSize = 80;
    public GUIText score;
    public Font scoreFont;
    public GUIStyle restart;
    public GUIStyle home;
    // Use this for initialization
    void Start()
    {
        score.font = scoreFont;
        score.transform.position = new Vector3((float)leftPadding / Screen.width, (float)(Screen.height - topPadding) / Screen.height, 0);
        score.fontSize = (int)((Screen.height * Screen.width) * 1.30208333333e-4);
        if (score.fontSize < 55)
            score.fontSize = 55;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 75 * 1.5005861664712778429073856975381f, 10, 75 * 1.5005861664712778429073856975381f, 75), "", restart))
        {
            Application.LoadLevel("DemoScene");
        }
        float buttonHeight = 0.10769230769230769230769230769231f * Screen.height;
        if (GUI.Button(new Rect(10, Screen.height * 0.184f, buttonHeight * 1.5005861664712778429073856975381f, buttonHeight), "", home))
        {
            Application.LoadLevel("Level1");
        }
    }
}
