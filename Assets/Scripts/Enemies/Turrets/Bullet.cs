using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform _target;
    private GameObject player;
    public GameObject _particles;
    public EnemyType enemyType;
    public float speed = 70;
    public float radius = 10f;

    public void setTarget(Transform target)
    {
        _target = target;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            hitTarget();
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(_target);

    }

    void hitTarget()
    {
        destroy(_target);
    }

    void destroy(Transform target)
    {
        GameObject particles = Instantiate(_particles, target.position, target.rotation);
        //target.GetComponent<Enemy>().EnemyHit(1);
        Destroy(gameObject);
        Destroy(particles, 0.4f);
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
}