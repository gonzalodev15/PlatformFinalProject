using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] private Animator attackAnimator = null;
    // Start is called before the first frame update

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            attackAnimator.Play("PlayerAttack");
        }
    }
}
