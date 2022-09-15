using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallDetection : MonoBehaviour
{
    private GameObject player;
    public static Action PlayerDied;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < -10)
        {
            PlayerDied.Invoke();
        }
    }
}
