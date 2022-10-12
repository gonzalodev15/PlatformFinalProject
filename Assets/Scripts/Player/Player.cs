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
    private Vector3 impact = Vector3.zero;
    private float verticalVelocity = 0;
    private float mass = 1.0f; // defines the character mass
    private bool canDoubleJump;
    private bool isJumping = false;
    public bool hasInvincibility = false;
    public bool hasInvincibilityShield = false;
    private Color initialColor;
    public static Action OnPlayerDamaged;
    public static Action OnItemObtained;
    public static Action OnPlayerDied;
    [SerializeField] private Animator animator = null;

    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = transform.GetComponent<CharacterController>();
        initialColor = gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontal, 0, vertical);
        movementDirection.Normalize();

        verticalVelocity += (Physics.gravity.y - 2)* Time.deltaTime;

        if (characterController.isGrounded)
        {
            isJumping = false;
            canDoubleJump = true;

            characterController.stepOffset = 0.01f;

            if (Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpSpeed;
                isJumping = true;
            }
            else
            {
                verticalVelocity = -1.0f;
            }
        }
        else
        {
            characterController.stepOffset = 0;

            if (isJumping && canDoubleJump && Input.GetButtonDown("Jump"))
            {
                verticalVelocity = jumpSpeed;
                canDoubleJump = false;
            }
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = verticalVelocity;

        characterController.Move(velocity * Time.deltaTime);

        if (movementDirection != Vector3.zero)
        {
            Quaternion fromRotation = transform.rotation;
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(fromRotation, toRotation, 700 * Time.deltaTime);
        }

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
        if (playerCurrentHealth >= playerMaxHealth) {
            playerCurrentHealth = playerMaxHealth;
        }
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
        StartCoroutine(AttackInmunity(1.0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            AddImpact(transform.position - collision.gameObject.transform.position, 8.0f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            AddImpact(transform.position - other.gameObject.transform.position, 8.0f);
        }
    }

    IEnumerator AttackInmunity(float inmunitySeconds)
    {
        hasInvincibility = true;
        animator.Play("PlayerHurt");
        yield return new WaitForSeconds(inmunitySeconds);
        hasInvincibility = false;
        gameObject.GetComponent<Renderer>().material.color = initialColor;
    }

    IEnumerator RestoreInvincibilityShield()
    {
        StartCoroutine(AttackInmunity(1.0f));
        hasInvincibility = false;
        hasInvincibilityShield = false;
        gameObject.GetComponent<Renderer>().material.color = initialColor;
        yield return new WaitForSeconds(20.0f);
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 16, 100, 0);
        hasInvincibility = true;
        hasInvincibilityShield = true;
    }

    public void applyInvincibilityVisualEffect()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 16, 100, 0);
        StartCoroutine(AttackInmunity(6.0f));
    }

    public void applyHighJump()
    {
        jumpSpeed += 2.0f;
    }

    public void applyBroaderRange()
    {
        Transform weapon = transform.Find("Capsule");
        weapon.localScale = new Vector3(weapon.localScale.x, 1.0f, weapon.localScale.z);
    }

    public void applyInvincibilityShield()
    {
        gameObject.GetComponent<Renderer>().material.color = new Color(0, 16, 100, 0);
        hasInvincibility = true;
        hasInvincibilityShield = true;
    }

    public void shutDownInvincibilityShield()
    {
        StartCoroutine(RestoreInvincibilityShield());
    }

}
