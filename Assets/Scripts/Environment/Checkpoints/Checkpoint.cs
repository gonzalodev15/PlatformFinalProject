using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Player player;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) {
            player = other.gameObject.GetComponent<Player>();
            player.respawnPoint = transform.position;
        }
    }
}
