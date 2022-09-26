using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public Player player;
    List<HealthHeart> hearts = new List<HealthHeart>();

    public void DrawHearts()
    {
        ClearHearts();
        float maxHealthRemainder = player.playerMaxHealth % 2;
        int heartsToMake = (int)((player.playerMaxHealth / 2) + maxHealthRemainder);
        for (int i= 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        for(int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = (int)Mathf.Clamp(player.playerCurrentHealth - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }

    void Start()
    {
        DrawHearts();
    }

    private void OnEnable()
    {
        Player.OnPlayerDamaged += DrawHearts;
        Player.OnItemObtained += DrawHearts;
    }

    private void OnDisable()
    {
        Player.OnPlayerDamaged -= DrawHearts;
        Player.OnItemObtained -= DrawHearts;
    }
}
