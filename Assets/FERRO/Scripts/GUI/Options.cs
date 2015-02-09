using UnityEngine;
using System.Collections;
using System.IO;

public class Options : MonoBehaviour {
    public bool vibrationAfterDeath = false;
    public string playerName = "Player";
    private string filePath = Application.persistentDataPath + "/settings.txt";
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        Debug.Log(filePath);
        if (!File.Exists(filePath))
        {
            Save();
        }
        else
        {
            string[] options = File.ReadAllLines(filePath);
            if (options[0].EndsWith("True"))
            {
                vibrationAfterDeath = true;
            }
            else
            {
                vibrationAfterDeath = false;
            }
            playerName = options[1].Substring(options[1].IndexOf('=')+1);
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void Save()
    {
        string[] toSettings = new string[2];
        if (vibrationAfterDeath)
        {
            toSettings[0] = "vibrationAfterDeath = " + (vibrationAfterDeath).ToString();
        }
        toSettings[1] = "playerName = " + playerName.ToString();
        File.WriteAllLines(filePath, toSettings);

    }
}
