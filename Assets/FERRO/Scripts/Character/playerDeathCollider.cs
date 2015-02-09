using UnityEngine;
using System.Collections;
using System.IO;
using System;
public class playerDeathCollider : MonoBehaviour {
    bool startSave = false;
    private float textFieldWidth;
    private float textFieldHeight;
    string recordContent = "";
    public GUIStyle textField;
	// Use this for initialization
	void Start () {
        textField.fontSize = (int)((Screen.height * Screen.width) * 1.10208333333e-4);
	}
	
	// Update is called once per frame
	void Update () {
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag != "Finish" && col.transform.tag != "Coin" && col.transform.tag != "ground" && col.transform.tag != "actionTrigger")
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>().isPlayerDead = true;
            transform.parent.GetComponent<Animator>().Play("sad_dead");
            
            if (GameObject.FindGameObjectWithTag("Options") && GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().vibrationAfterDeath)
            {
                Handheld.Vibrate();
            }
            if(!startSave)
                StartCoroutine(SaveScore());
        }
        if (col.transform.tag == "Finish")
        {
            transform.parent.GetComponent<playerControl>().finished = true;
        }
    }
    void OnGUI()
    {
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>().isPlayerDead || GameObject.FindGameObjectWithTag("Player").GetComponent<playerControl>().finished)
        {
            textFieldHeight = 0.58922558922558922558922558922559f * Screen.height;
            textFieldWidth = textFieldHeight * 1.4285714285714285714285714285714f;
            GUI.TextField(new Rect(Screen.width / 2 - textFieldWidth / 2, Screen.height / 2 - textFieldHeight / 2, textFieldWidth, textFieldHeight), recordContent, textField);
        }
    }
    IEnumerator SaveScore()
    {
        startSave = true;
        yield return new WaitForSeconds(0.1f);
        string filePath = Application.persistentDataPath + "/records.txt";
        if (!File.Exists(filePath))
        {
            string[] toScore = new string[1];
            toScore[0] = GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().playerName+" "+ GameObject.FindGameObjectWithTag("Player").GetComponent<playerScore>().currentScore.ToString();
            File.WriteAllLines(filePath, toScore);
        }
        else
        {
            string[] sGetScores = File.ReadAllLines(filePath);

            string[] sScores = new string[sGetScores.Length + 1];
            
            int[] iScores = new int[sScores.Length];
            for (int i = 0; i < sGetScores.Length; i++)
            {
                sScores[i] = sGetScores[i];
                iScores[i] = Int32.Parse(sScores[i].Remove(0,sScores[i].LastIndexOf(' ')+1));
            }
            sScores[sGetScores.Length] = GameObject.FindGameObjectWithTag("Options").GetComponent<Options>().playerName + " " + GameObject.FindGameObjectWithTag("Player").GetComponent<playerScore>().currentScore.ToString();
            iScores[sGetScores.Length] = GameObject.FindGameObjectWithTag("Player").GetComponent<playerScore>().currentScore;
            bool changed = true;
            while (changed)
            {
                changed = false;
                for (int i = iScores.Length-1; i >= 1; i--)
                {
                    if (iScores[i - 1] < iScores[i])
                    {
                        changed = true;
                        string sTmp = sScores[i];
                        sScores[i] = sScores[i - 1];
                        sScores[i - 1] = sTmp;
                        int tmp = iScores[i];
                        iScores[i] = iScores[i-1];
                        iScores[i-1] = tmp;
                    }
                }
            }
            int to = sGetScores.Length;
            if (sGetScores.Length < 5)
                to = iScores.Length;
            sGetScores = new string[to];
            for (int i = 0; i < to; i++)
            {
                sGetScores[i] = sScores[i];
                recordContent += sScores[i] + "\n";
            }
            File.Delete(filePath);
            File.WriteAllLines(filePath, sGetScores);
            
        }
    }
}
