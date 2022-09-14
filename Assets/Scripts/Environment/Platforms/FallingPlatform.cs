using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    bool isPlayerInPlatform = false;
    float downSpeed = 0;
    float fallingDelay = 2.0f;
    float upDelay = 3.0f;

    float duration = 1.5f;
    private float t = 0;

    Vector3 initialPosition;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isPlayerInPlatform = true;
            StartCoroutine(WaitForPlatform());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isPlayerInPlatform = false;
            //StopAllCoroutines();
            StartCoroutine(WaitForGoingUp());
        }
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFalling)
        {
            downSpeed += Time.deltaTime;
            transform.position = new Vector3(transform.position.x, transform.position.y - downSpeed, transform.position.z);
        }

        if (!isFalling)
        {
            if (transform.position.y != initialPosition.y)
            {
                ResetColor();
                transform.position = Vector3.Lerp(transform.position, initialPosition, 0.2f);
            }
            else if (initialPosition == transform.position && isPlayerInPlatform)
            {
                ChangeColor();
            }
        }
    }

    private IEnumerator WaitForPlatform()
    {
        yield return new WaitForSeconds(fallingDelay);
        isFalling = true;
    }

    private IEnumerator WaitForGoingUp()
    {
        yield return new WaitForSeconds(upDelay);
        if (isFalling)
        {
            transform.position = new Vector3(transform.position.x, -6.0f, transform.position.z);
            isFalling = false;
            downSpeed = 0;
            ResetColor();
        }
    }

    void ChangeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, t);

        if (t < 1)
        {
            t += Time.deltaTime / duration;
        }
    }

    void ResetColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.grey;
        t = 0;
    }
}

