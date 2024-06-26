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

    bool triggeredOnce = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        if (triggeredOnce && !isBeingReversed)
        {
            animator.SetFloat("AnimationSpeed", 0);
            return;
        }

        if (triggeredOnce && isBeingReversed)
        {
            animator.SetFloat("AnimationSpeed", reverseSpeed);
            return;
        }


        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            if (!isBeingReversed)
            {
                animator.SetFloat("AnimationSpeed", 0);
            }
            else
            {
                animator.SetFloat("AnimationSpeed", reverseSpeed);
            }
        }
        else
        {
            if (!isBeingReversed)
            {
                animator.SetFloat("AnimationSpeed", 1);

            }
        }
    }



    private void OnMouseDrag()
    {
        isBeingReversed = true; 
        //animator.SetFloat("AnimationSpeed", reverseSpeed);
    }

    private void OnMouseUp()
    {
        isBeingReversed = false;
        //animator.SetFloat("AnimationSpeed", 0);
    }

    private void OnMouseDown()
    {
        triggeredOnce = true;

        isBeingReversed = true;
        //animator.SetFloat("AnimationSpeed", 0);
    }
}
