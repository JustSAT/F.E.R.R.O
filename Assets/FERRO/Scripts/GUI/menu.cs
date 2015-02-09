using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class menu : MonoBehaviour {

    public List<Texture2D> myLogos;
    private int currentLogoId = 0;
    private float logoWidth;
    private float logoHeight;

    public float startButtonHeight;
    public float startButtonWidth;
    public float buttonsHeight;
    public float buttonsWidth;

    public float toggleSize = 55;
    private int toggleFontSize = 40;

    private float indent = 50.0f;
    private float startButtonIndent;
    private bool preClose = false;
    public bool openedAbout = false;
    private bool openedOptions = false;

    public GameObject aboutUsGO;
    public GameObject prefab;

    public GUIStyle startGame;
    public GUIStyle options;
    public GUIStyle aboutUs;
    public GUIStyle toggle;
    public GUIStyle textField;
    public GUIStyle label;

    public GameObject myOptions;

    public string playerName = "Player";
	// Use this for initialization
	void Start () {
        if(!GameObject.FindGameObjectWithTag("Options")){
            Instantiate(myOptions);
        }
        if (myLogos.Count > 0)
        {
            currentLogoId = Random.Range(0, myLogos.Count);
        }
	}
	
	// Update is called once per frame
	void Update () {
        logoHeight = Screen.height * 0.5343f;
        logoWidth = logoHeight * 1.6676f;
        startButtonIndent = Screen.height * 0.53991f;
        startButtonHeight = Screen.height * 0.415f;
        startButtonWidth = startButtonHeight * 1.5005861664712778429073856975381f;
        buttonsHeight = Screen.height * 0.215f;
        buttonsWidth = buttonsHeight * 1.5005861664712778429073856975381f;
	}
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        playerName = GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().playerName;
    }
    void OnGUI()
    {
        if (!openedAbout && !openedOptions && !preClose)
        {
            GUI.DrawTexture(new Rect(Screen.width / 2 - logoWidth / 2, 0, logoWidth, logoHeight), myLogos[currentLogoId]);
            if (GUI.Button(new Rect(Screen.width / 2 - startButtonWidth / 2, startButtonIndent, startButtonWidth, startButtonHeight), "", startGame))
            {
                Application.LoadLevel("DemoScene");
            }
            if (GUI.Button(new Rect(Screen.width / 4 - buttonsWidth / 2, Screen.height - buttonsHeight-2, buttonsWidth, buttonsHeight), "", options))
            {
                openedOptions = true;
            }
            if (GUI.Button(new Rect(Screen.width - Screen.width / 4 - buttonsWidth / 2, Screen.height - buttonsHeight-2, buttonsWidth, buttonsHeight), "", aboutUs))
            {
                openedAbout = true;
                OpenAbout();
                Handheld.Vibrate();
            }
        }
        if (openedOptions)
        {
            toggleSize = Screen.width * 0.0453720508166969147005444646098f;
            toggleFontSize = (int)(toggleSize * 0.72f);
            toggle.fontSize = toggleFontSize;
            toggle.contentOffset = new Vector2(toggleFontSize+toggleFontSize*0.502f, toggleFontSize * 0.1025641025641025641025641025641f);
            
            GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().vibrationAfterDeath = GUI.Toggle(new Rect((Screen.width - (toggleSize + "Vibration".Length)) / 2, 50, toggleSize, toggleSize), GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().vibrationAfterDeath, "Vibration", toggle);
            GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().Save();
            if (GUI.Button(new Rect(Screen.width / 2 - buttonsWidth / 2, 3 * indent + 2 * buttonsHeight, buttonsWidth, buttonsHeight), "", options))
            {
                openedOptions = false;
            }

            label.fontSize = toggleFontSize;

            textField.fontSize = toggleFontSize;
            textField.contentOffset = new Vector2(toggleFontSize * 0.502f, 0);
            GUI.Label(new Rect((Screen.width - (toggleSize + "Vibration".Length)) / 2, 50 + toggleSize + 10, 400, toggleFontSize * 1.2f), "Player name:", label);
            playerName = GUI.TextField(new Rect((Screen.width - (toggleSize + "Vibration".Length)) / 2, 50 + toggleSize * 2 + 10, 0.273321f*Screen.width, toggleFontSize * 1.2f), playerName, 15, textField);
            GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().playerName = playerName;
        }
        if (openedAbout)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - buttonsWidth / 2, 3 * indent + 2 * buttonsHeight, buttonsWidth, buttonsHeight), "", aboutUs))
            {
                CloseAbout();
                preClose = true;
            }
        }
        if (preClose)
        {
            if (!aboutUsGO.GetComponent<ContentScript>().unHideAll)
            {
                preClose = false;
                Destroy(aboutUsGO);
            }
        }
        
    }
    void OpenAbout()
    {
        aboutUsGO = Instantiate(prefab) as GameObject;
        aboutUsGO.SetActive(true);
    }
    void CloseAbout()
    {
        openedAbout = false;
        aboutUsGO.GetComponent<ContentScript>().unHideAll = true;
    }
}
