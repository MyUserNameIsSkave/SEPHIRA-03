using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinahAnimation : MonoBehaviour
{

    [SerializeField]
    private Animator Animator;



    public void Struggling()
    {
        Animator.SetBool("Struggling", true);
    }




    public void OnPunch()
    {
        Animator.SetBool("Punch", true);

    }




    public void OnPunched()
    {
        Animator.SetBool("Punched", true);
        Invoke("ResetAnimation", 1f);
    }





    public void ResetAnimation()
    {
        Animator.SetBool("Struggling", false);
        Animator.SetBool("Punch", false);
        Animator.SetBool("Punch", false);


    }


}
