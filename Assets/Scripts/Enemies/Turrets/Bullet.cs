using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    private GameObject player;
    public GameObject _particles;
    public Vector3 targetPosition;
    public EnemyType enemyType;
    public float speed = 70;
    public float radius = 10f;

    public void setTarget(Transform target)
    {
        _target = target;
        targetPosition = _target.position;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y + 90, rotation.z + 25);
    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = targetPosition - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            hitTarget();
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void hitTarget()
    {
        if (radius == 0)
        {
            destroy(_target);
        }
        else
        {
            explode();
        }
    }

    void destroy(Transform target)
    {
        GameObject particles = Instantiate(_particles, targetPosition, _target.rotation);
        Destroy(gameObject);
        Destroy(particles, 0.4f);
    }

    void explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Player")
            {
                Player _player  = collider.gameObject.GetComponent<Player>();
                player = _player.gameObject;
                bool hasInvincibility = player.GetComponent<Player>().hasInvincibility;
                bool hasInvincibilityShield = player.GetComponent<Player>().hasInvincibilityShield;
                if (!hasInvincibility)
                {
                    substractPlayerLife();
                }
                else if (hasInvincibility && hasInvincibilityShield)
                {
                    player.GetComponent<Player>().shutDownInvincibilityShield();
                }
            }
        }
        destroy(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Se entró a TriggerEnter");
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            bool hasInvincibility = player.GetComponent<Player>().hasInvincibility;
            bool hasInvincibilityShield = player.GetComponent<Player>().hasInvincibilityShield;
            print("Es invencible: " + hasInvincibility);
            if (!hasInvincibility)
            {
                destroy(player.transform);
                substractPlayerLife();
            }
            else if (hasInvincibility && hasInvincibilityShield)
            {
                player.GetComponent<Player>().shutDownInvincibilityShield();
            }
        }
    }

    private void substractPlayerLife()
    {
        switch (enemyType)
        {
            case EnemyType.robot:
                player.GetComponent<Player>().substractHealth(1.0f);
                break;
            case EnemyType.turret:
                player.GetComponent<Player>().substractHealth(2.0f);
                break;
            case EnemyType.drone:
                player.GetComponent<Player>().substractHealth(2.0f);
                break;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(targetPosition, radius);
    }
}