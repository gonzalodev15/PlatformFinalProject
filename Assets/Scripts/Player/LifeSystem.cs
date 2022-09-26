using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    GameObject[] hearts;
    private float life;
    // Start is called before the first frame update
    void Start()
    {
        life = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
    }
}
