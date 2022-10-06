using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform partToRotate;
    Transform target;
    public Transform bullet;
    public Transform shootStartPoint;
    public Transform shootEndPoint;
    public bool isLaser = false;
    public LineRenderer lineRenderer;
    public GameObject _particles;

    public float fireRate = 1f;
    public float fireCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
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
        GameObject currentBullet = Instantiate(bullet, shootStartPoint.position, shootStartPoint.rotation).gameObject;
        currentBullet.GetComponent<Bullet>().setTarget(shootEndPoint);
    }
}
