using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{

    private Text scoreText;
    // Start is called before the first frame update
    void Awake()
    {
        this.scoreText = transform.Find("ScoreText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = Game_script.getScore().ToString();
    }
}
