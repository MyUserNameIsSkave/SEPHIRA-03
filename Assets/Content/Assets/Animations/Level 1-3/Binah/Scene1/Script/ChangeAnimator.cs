using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeAnimator : MonoBehaviour, IEventTriggerable
{
    public RuntimeAnimatorController newAnimatorController; // R�f�rence au nouvel AnimatorController que vous voulez assigner




    void Start()
    {
        // R�cup�re l'Animator actuellement assign� au GameObject
        Animator currentAnimator = GetComponent<Animator>();


        //Change automatiquement si respawn
        if (GameManager.CurrentIndex != -1)
        {
            TriggerEvent();
            GameManager.Instance.playerInputLocked = false;

        }
    }



    public void TriggerEvent()
    {
        // R�cup�re l'Animator actuellement assign� au GameObject
        Animator currentAnimator = GetComponent<Animator>();

        if (currentAnimator != null)
        {
            if (newAnimatorController != null)
            {
                // Met � jour l'Animator actuel avec le nouvel AnimatorController
                currentAnimator.runtimeAnimatorController = newAnimatorController;
            }
            else
            {
                Debug.LogWarning("Aucun AnimatorController assign� � la variable newAnimatorController.");
            }
        }
        else
        {
            Debug.LogWarning("Aucun Animator trouv� sur le GameObject.");
        }
    }
}

