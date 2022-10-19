using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform partToRotate;
    Transform target;
    public Transform bullet;
    public Transform shootStartPoint;
    public Transform shootStartPoint2;
    public Transform shootEndPoint;
    private Transform currentShootStartPoint;
    public GameObject _particles;

    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public bool isDrone = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isDrone)
        {
            currentShootStartPoint = shootStartPoint;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDrone)
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        } else
        {
            if (fireCountdown <= 0f && target != null)
            {
                Shoot();
                fireCountdown = 1f / fireRate;
            }
        }
        
        fireCountdown -= Time.deltaTime;
    }


    void showParticles(Transform target)
    {
        GameObject particles = Instantiate(_particles, target.position, target.rotation);
        //target.GetComponent<Enemy>().EnemyHit(0.03f);
        Destroy(particles, 0.3f);
    }

    void Shoot()
    {
        if(isDrone)
        {
            GameObject currentBullet = Instantiate(bullet, currentShootStartPoint.position, currentShootStartPoint.rotation).gameObject;
            currentBullet.GetComponent<Bullet>().setTarget(target);

            target = null;
            if(currentShootStartPoint == shootStartPoint)
            {
                currentShootStartPoint = shootStartPoint2;
            } else
            {
                currentShootStartPoint = shootStartPoint;
            }
        } else
        {
            GameObject currentBullet = Instantiate(bullet, shootStartPoint.position, shootStartPoint.rotation).gameObject;
            currentBullet.GetComponent<Bullet>().setTarget(shootEndPoint);
            target = null;
        }
    }

    

    void lockTarget(GameObject player)
    {
        target = player.transform;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            lockTarget(other.gameObject);
        }
    }
}
