using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    public TMPro.TMP_Text timerText;
    public GameObject gameOverObject;
    public GameObject touchController;
    int totalScore = 0;
    float timerTime = 60.0f;
    public bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        scoreText.text = "0";
        timerTime = 60.0f;
        timerText.text = "1:00";
        isGameOver = false;
        gameOverObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameOver)
        {
            timerTime -= Time.deltaTime;

            float minutes = Mathf.FloorToInt(timerTime / 60);
            float seconds = Mathf.FloorToInt(timerTime % 60);
            timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);

            if (timerTime <= 0)
            {
                isGameOver = true;
                timerText.text = "0:00";
                gameOverObject.SetActive(true);
                // disable touch controls
                touchController.SetActive(false);
            }
        }
    }

    public void AddToScore(int num)
    {
        totalScore += num;
        scoreText.text = totalScore.ToString();
    }
}
