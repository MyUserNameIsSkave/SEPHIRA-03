using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReversAnimation : MonoBehaviour
{
    [SerializeField]
    private float reverseSpeed;

    Animator animator;

    bool isBeingReversed= false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
 
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            animator.SetFloat("AnimationSpeed", 0);
        }
        else
        {
            animator.SetFloat("AnimationSpeed", 1);
        }
        
    }



    private void OnMouseDrag()
    {
        isBeingReversed = true; 
        animator.SetFloat("AnimationSpeed", reverseSpeed);
    }

    private void OnMouseUp()
    {
        isBeingReversed = false;
        animator.SetFloat("AnimationSpeed", 1);
    }
}
