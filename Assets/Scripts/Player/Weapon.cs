using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float knockbackSpeed = 5.0f;
    public CapsuleCollider capsuleCollider;

    private void Update()
    {
        bool isAttacking = transform.parent.gameObject.GetComponent<Attack>().isAttacking;
        if (isAttacking)
        {
            capsuleCollider.enabled = true;
        } else
        {
            capsuleCollider.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        float distanceBetweenObjects = Vector3.Distance(transform.position, collision.gameObject.transform.position);
        if (collision.gameObject.CompareTag("enemy"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * knockbackSpeed, ForceMode.Impulse);
            if (collision.gameObject.GetComponent<Enemy>().enemyType == EnemyType.drone)
            {
                StartCoroutine(droneCollisionTime(collision));
            }
            collision.gameObject.GetComponent<Enemy>().gotHit();
        }
    }

    IEnumerator droneCollisionTime(Collision collision) {
        yield return new WaitForSeconds(0.25f);
        collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

}
