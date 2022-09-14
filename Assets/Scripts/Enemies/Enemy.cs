using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    public float speed = 2f;
    private bool isPatrolling = true;
    private Vector3 originalPosition;
    public Transform startPatrolPoint;
    public Transform endPatrolPoint;
    private Transform currentPatrolPoint;
   
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        currentPatrolPoint = startPatrolPoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            if(transform.position != originalPosition && isPatrolling == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, originalPosition, speed * Time.deltaTime);
            }

            if (transform.position == originalPosition)
            {
                isPatrolling = true;
            }

            if (isPatrolling == true)
            {
                if (Vector3.Distance(transform.position, currentPatrolPoint.position) < 0.5f)
                {
                    if(currentPatrolPoint == startPatrolPoint)
                    {
                        print("Hacia la derecha");
                        currentPatrolPoint = endPatrolPoint;
                    } else if (currentPatrolPoint == endPatrolPoint)
                    {
                        print("Hacia la izquierda");
                        currentPatrolPoint = startPatrolPoint;
                    }
                }
                else
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position,
                        currentPatrolPoint.position,
                        speed * Time.deltaTime);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPatrolling = false;
            player = other.gameObject;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
        }
    }
}
