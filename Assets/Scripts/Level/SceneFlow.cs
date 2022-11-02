using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    public string retryNameLevel;

    private void Start()
    {
        if (retryNameLevel != "")
        {
            GameConfigSingleton.instance.retryNameLevel = retryNameLevel;
        }
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }

    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync(GameConfigSingleton.instance.retryNameLevel);
    }
}
