using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    bool isFalling = false;
    bool wasTouched = false;
    float downSpeed = 0;
    public float fallingDelay = 2.0f;
    public float upDelay = 3.0f;
    private Color initialColor;
    private float t = 0;

    Vector3 initialPosition;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            wasTouched = true;
            StartCoroutine(WaitForPlatform());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            StartCoroutine(WaitForGoingUp());
        }
    }

    private void Start()
    {
        initialPosition = transform.position;
        initialColor = gameObject.GetComponent<Renderer>().material.color;
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
                transform.position = Vector3.Lerp(transform.position, initialPosition, 0.3f);
                StartCoroutine(WaitForAssigningValue());
            }
            else if (transform.position == initialPosition&& wasTouched)
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
            transform.position = new Vector3(transform.position.x, initialPosition.y-6.0f, transform.position.z);
            isFalling = false;
            wasTouched = false;
            downSpeed = 0;
            ResetColor();
        }
    }

    private IEnumerator WaitForAssigningValue()
    {
        yield return new WaitForSeconds(0.2f);
        transform.position = initialPosition;
    }

    void ChangeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, t);

        if (t < 1)
        {
            t += Time.deltaTime / fallingDelay;
        }
    }

    void ResetColor()
    {
        gameObject.GetComponent<Renderer>().material.color = initialColor;
        t = 0;
    }
}

