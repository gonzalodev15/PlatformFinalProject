using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;
    public static Action OnTargetScoreReached;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Error: instance already created");
            return;
        }
        instance = this;
    }

    public float getScore()
    {
        return float.Parse(scoreText.text);
    }

    public void addScore(float score)
    {
        if (LevelFlow.playerDied == false)
        {
            scoreText.text = Mathf.Round(getScore() + score).ToString();
            if (getScore() >= 700)
            {
                substractScore(700);
                OnTargetScoreReached?.Invoke();
            }
        }
    }

    public void substractScore(float score)
    {
        scoreText.text = Mathf.Round(getScore() - score).ToString();
    }
}

