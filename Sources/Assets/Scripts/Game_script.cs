using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_script : MonoBehaviour
{

    private static Game_script handler;
    private static int score;
    private static int increment;

    // Start is called before the first frame update
    void Awake()
    {
        handler = this;
        increment = 1;
        resetScore();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public static int getScore()
    {
        return score;
    }

    public static void updateScore()
    {
        if (score < 10)
        {
        } else if (score < 100)
        {
            increment = 10;
        } else
        {
            increment = 100;
        }
        score += increment;
        //PopupHandler.popupText(increment.ToString());
    }

    public static void resetScore()
    {
        score = 0;
    }
}
