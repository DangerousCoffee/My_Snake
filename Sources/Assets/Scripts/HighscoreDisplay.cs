using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreDisplay : MonoBehaviour
{
    private static Text highscoreText;

    // Start is called before the first frame update
    void Awake()
    {
        highscoreText = transform.Find("Highscore").GetComponent<Text>();

        int highscoreInt = HighscoreDisplay.getHighscore();
        highscoreText.text = "HIGHSCORE: " + HighscoreDisplay.getHighscore();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private static int getHighscore()
    {
        return PlayerPrefs.GetInt("Highscore", 0);
    }

    public static bool setNewHighscore(int score)
    {
        int highscore = getHighscore();
        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            PlayerPrefs.Save();
            return true;
        }
        else return false;
    }

}
