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
    private float verticalVelocity = 0;
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
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        if (characterController.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                canDoubleJump = true;
                verticalVelocity = jumpSpeed;
                moveDirection.y = verticalVelocity;
            } else
            {
                verticalVelocity = 0;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
            {
                verticalVelocity += 4f;
                canDoubleJump = false;
            }
        }
        verticalVelocity -= gravity * Time.deltaTime;
        moveDirection = new Vector3(xMove, verticalVelocity, zMove);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection.x *= speed;
        moveDirection.z *= speed;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        
    }

    void OnCollisionStay()
    {
    }
}
