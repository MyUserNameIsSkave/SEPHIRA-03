using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RencontreBinah_Cinematic : MonoBehaviour, IEventTriggerable
{
    public Animator animator;
    public GameObject Binah;


    public int index = 0;

    void Start()
    {
        Binah.SetActive(false);
    }
    public void TriggerEvent()
    {
        print("activated");


        switch (index)
        {
            case 0:
                animator.SetTrigger("StartCinematic");
                print("StartCinematic");
                break;

            case 1:
                animator.SetTrigger("NextAnim1");
                print("NextAnim1");
                break;

            case 2:
                animator.SetTrigger("NextAnim2");
                print("NextAnim2");
                break;

            case 3:
                animator.SetTrigger("NextAnim3");
                print("NextAnim3");
                break;

            case 4:
                animator.SetTrigger("Idle");
                print("Idle");
                break;

            case 5:
                Binah.SetActive(true);
                print("SetActive");
                break;
        }

        index += 1;
    }
}
