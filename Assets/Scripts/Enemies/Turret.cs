using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Transform partToRotate;
    Transform target;
    public Transform bullet;
    public Transform shootStartPoint;
    public GameObject _particles;
    public float range = 15f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, .5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");

        if (enemies.Length > 0)
        {
            int index = -1;
            float smallestDistance = Mathf.Infinity;

            for (int i = 0; i < enemies.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, enemies[i].transform.position);

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    index = i;
                }
            }

            if (enemies[index] != null && smallestDistance <= range)
            {
                target = enemies[index].transform;
            }
            else
            {
                target = null;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        lockTarget();

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

    void lockTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Shoot()
    {
        GameObject currentBullet = Instantiate(bullet, shootStartPoint.position, shootStartPoint.rotation).gameObject;
        //currentBullet.GetComponent<Bullet>().setTarget(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}