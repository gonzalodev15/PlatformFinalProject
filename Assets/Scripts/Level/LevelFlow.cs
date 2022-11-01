using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelFlow : MonoBehaviour
{
    public SceneFlow _sceneFlow;
    public GameObject blackOutSquare;

    public string _winSceneName;
    public float _levelLoadDelay;
    public static bool playerDied = false;

    public string _loseSceneName;

    private void OnEnable()
    {
        Player.OnPlayerDied += LoadLoseLevel;
        Goal.GoalReached += LoadWinLevel;
        FallDetection.PlayerDiedFromFall += resetPlayerPositionToCheckpoint;
    }

    private IEnumerator LoadLevel(string levelName)
    {
        yield return new WaitForSeconds(_levelLoadDelay);
        _sceneFlow.LoadLevel(levelName);
    }

    private void LoadWinLevel()
    {
        StartCoroutine(LoadLevel(_winSceneName));
    }

    private void LoadLoseLevel()
    {
        playerDied = true;
        StartCoroutine(FadeBlackOutSquare());
        StartCoroutine(LoadLevel(_loseSceneName));
    }

    private void resetPlayerPositionToCheckpoint()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        player.transform.position = player.respawnPoint;
    }

    private void OnDisable()
    {
        Player.OnPlayerDied -= LoadLoseLevel;
        Goal.GoalReached -= LoadWinLevel;
        FallDetection.PlayerDiedFromFall -= resetPlayerPositionToCheckpoint;
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 1)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        float fadeAmount;

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmount = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmount = objectColor.a - (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        yield return new WaitForEndOfFrame();
    }
}