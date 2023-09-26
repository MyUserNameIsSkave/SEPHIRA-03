using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemisAnimation : MonoBehaviour
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
    }


}
