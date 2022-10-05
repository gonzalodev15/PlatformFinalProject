using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    float timeCounter = 0;

    public float speed;
    float width;
    float height;
    private GameObject Player;
    public GameObject PlayerMaintainer;
    private Transform originalPosition;
    public bool isBackwards;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform;
        width = 0.1f;
        height = 0.1f;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        timeCounter += Time.deltaTime * speed;
        float x;
        float y;
        if (isBackwards)
        {
            x = (Mathf.Cos(-timeCounter) * width) + originalPosition.position.x;
            y = (Mathf.Sin(-timeCounter) * height) + originalPosition.position.y;
        } else
        {
            x = (Mathf.Cos(timeCounter) * width) + originalPosition.position.x;
            y = (Mathf.Sin(timeCounter) * height) + originalPosition.position.y;
        }
     
        float z = originalPosition.position.z;
        transform.position = new Vector3(x, y, z);
    }


    private void OnTriggerEnter(Collider other)
    {
        Vector3 myScale = transform.localScale;
        PlayerMaintainer.transform.localScale = new Vector3(1f / myScale.x, 1f / myScale.y,
                1f / myScale.z);

        PlayerMaintainer.transform.SetParent(transform, false);

        if (other.gameObject.transform.CompareTag("Player"))
        {
            Player = other.gameObject;
            Player.transform.parent = PlayerMaintainer.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            Player.transform.parent = null;
            Player = null;
        }
    }
}