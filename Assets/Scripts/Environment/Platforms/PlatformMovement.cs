using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform startMarker;
    public Transform endMarker;
    private GameObject Player;
    public GameObject PlayerMaintainer;
    // Start is called before the first frame update

    public float speed = 1.0f;
    public float journeyLenght = 1.0f;
    private float startTime;
    public bool loop = false;

    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!loop)
        {
            float distCovered = (Time.time - startTime) * speed;
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, distCovered / journeyLenght);
        }

        if (loop)
        {
            float distCovered = Mathf.PingPong(Time.time - startTime, journeyLenght / speed);
            transform.position = Vector3.Lerp(startMarker.position, endMarker.position, distCovered / journeyLenght);
        }
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
