using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFlow : MonoBehaviour
{
    public SceneFlow _sceneFlow;

    public string _winSceneName;
    public float _levelLoadDelay;

    public string _loseSceneName;

    private void OnEnable()
    {
        Goal.GoalReached += LoadWinLevel;
        FallDetection.PlayerDied += LoadLoseLevel;
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
        StartCoroutine(LoadLevel(_loseSceneName));
    }

    private void OnDisable()
    {
        Goal.GoalReached -= LoadWinLevel;
        FallDetection.PlayerDied -= LoadLoseLevel;
    }
}