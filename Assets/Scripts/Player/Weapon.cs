using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float knockbackSpeed = 5.0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            print(direction);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * knockbackSpeed, ForceMode.Impulse);
            print("Se aplicó la fuerza con éxito");
            collision.gameObject.GetComponent<Enemy>().gotHit();
        }
    }
}
