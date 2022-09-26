﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthItemEnum
{
    [InspectorName("Red potion")]
    red,
    [InspectorName("Green potion")]
    green,
    [InspectorName("Blue potion")]
    blue
}

public class HealthItem : MonoBehaviour
{
    [Searchable] public HealthItemEnum item;
    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Player>();
            if (player.playerMaxHealth != player.playerCurrentHealth)
            {
                Destroy(gameObject);
                applyEffect();
            }  
        }
    }

    private void applyEffect()
    {
        switch (item)
        {
            case HealthItemEnum.red:
                player.restoreHealth(1.0f);
                break;
            case HealthItemEnum.green:
                player.restoreHealth(2.0f);
                break;
            case HealthItemEnum.blue:
                player.restoreHealth(2.0f);
                break;
        }
    }
}
