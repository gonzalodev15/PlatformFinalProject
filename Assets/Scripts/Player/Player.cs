using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float speed = 2f;
    public float gravity = 8f;
    public float jumpSpeed = 4f;
    public float playerMaxHealth = 8.0f;
    public float playerCurrentHealth = 8.0f;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 impact = Vector3.zero;
    private float verticalVelocity = 0;
    private float mass = 1.0f; // defines the character mass
    private bool canDoubleJump;
    public bool hasInvincibility = false;
    public static Action OnPlayerDamaged;
    public static Action OnItemObtained;
    public static Action OnPlayerDied;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
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
                moveDirection.y = verticalVelocity;
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

        if (impact.magnitude > 0.2) characterController.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
    }

    void AddImpact(Vector3 direction, float force)
    {
        direction.Normalize();
        if (direction.y < 0) direction.y = -direction.y; // reflect down force on the ground
        impact += direction.normalized * force / mass;
    }

    public void restoreHealth(float healthToRestore)
    {
        playerCurrentHealth += healthToRestore;
        OnItemObtained?.Invoke();
    }

    public void substractHealth(float healthToSubstract)
    {
        if (hasInvincibility) { return; }
        playerCurrentHealth -= healthToSubstract;
        OnPlayerDamaged?.Invoke();
        if (playerCurrentHealth <= 0)
        {
            OnPlayerDied?.Invoke();
            return;
        }
        StartCoroutine(AttackInmunity());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            AddImpact(transform.position - collision.gameObject.transform.position, 8.0f);
        }
    }

    IEnumerator AttackInmunity()
    {
        hasInvincibility = true;
        yield return new WaitForSeconds(1.5f);
        hasInvincibility = false;
    }
}
