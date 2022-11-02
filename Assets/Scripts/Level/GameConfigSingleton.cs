using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigSingleton : MonoBehaviour
{
    public static GameConfigSingleton instance;
    public string retryNameLevel = "";

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public string getRetryNameLevel()
    {
        return retryNameLevel;
    }

    public void modifyRetryLevel(string retryLevelName)
    {
        retryNameLevel = retryLevelName;
    }

}


