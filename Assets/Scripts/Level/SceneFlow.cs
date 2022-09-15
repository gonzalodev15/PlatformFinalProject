using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFlow : MonoBehaviour
{
    public string retryLevelName;

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadSceneAsync(levelName);
    }

    public void RetryLevel()
    {
        SceneManager.LoadSceneAsync(retryLevelName);
    }
}
