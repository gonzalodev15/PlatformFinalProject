using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FallDetection : MonoBehaviour
{
    private Player playerObject;
    public static Action PlayerDiedFromFall;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObject.characterController.velocity.y < -35) {
            PlayerDiedFromFall?.Invoke();
        }
    }
}
