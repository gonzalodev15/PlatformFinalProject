using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator attackAnimator = null;
    [SerializeField] private GameObject particles;
    private bool canAttack = true;
    public bool isAttacking = false;
    // Start is called before the first frame update

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            attackAnimator.Play("PlayerAttack");
            StartCoroutine(AttackAction());
        }
    }

    IEnumerator AttackAction()
    {
        isAttacking = true;
        particles.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        isAttacking = false;
        particles.SetActive(false);
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

}
