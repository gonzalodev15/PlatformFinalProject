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
            collision.gameObject.GetComponent<Enemy>().gotHit();
        }
    }

}
