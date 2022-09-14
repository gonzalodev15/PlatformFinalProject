using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float gravity = 8f;
    public float jumpSpeed = 4f;
    private Vector3 moveDirection = Vector3.zero;
    private bool canDoubleJump;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isGrounded)
        {
            float xMove = Input.GetAxisRaw("Horizontal");
            float zMove = Input.GetAxisRaw("Vertical");
            moveDirection = new Vector3(xMove, 0f, zMove);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canDoubleJump = true;
                moveDirection.y = jumpSpeed;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
            {
                moveDirection.y += 3;
                canDoubleJump = false;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
    }

    void OnCollisionStay()
    {
    }
}
