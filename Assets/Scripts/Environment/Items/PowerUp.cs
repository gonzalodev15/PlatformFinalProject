using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpEnum
{
    [InspectorName("Red potion")]
    highJump,
    [InspectorName("Green potion")]
    increasedAttack,
    [InspectorName("Blue potion")]
    untouchable
}

public class PowerUp : MonoBehaviour
{
    [Searchable] public PowerUpEnum item;
    private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<Player>();

            Destroy(gameObject);
            applyEffect();
        }
    }

    private void applyEffect()
    {
        switch (item)
        {
            case PowerUpEnum.highJump:
                player.applyHighJump();
                break;
            case PowerUpEnum.increasedAttack:
                player.restoreHealth(2.0f);
                break;
            case PowerUpEnum.untouchable:
                player.applyInvincibilityVisualEffect();
                break;
        }
    }
}