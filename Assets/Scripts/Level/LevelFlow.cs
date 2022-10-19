using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFlow : MonoBehaviour
{
    public SceneFlow _sceneFlow;

    public string _winSceneName;
    public float _levelLoadDelay;

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
}