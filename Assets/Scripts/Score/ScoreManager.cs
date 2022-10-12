using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public Text scoreText;

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
        scoreText.text = Mathf.Round(getScore() + score).ToString();
    }
}

