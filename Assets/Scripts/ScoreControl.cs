using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    int totalScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToScore(int num)
    {
        totalScore += num;
        scoreText.text = totalScore.ToString();
    }
}
