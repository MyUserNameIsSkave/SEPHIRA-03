using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeAnimator : MonoBehaviour, IEventTriggerable
{
    public RuntimeAnimatorController newAnimatorController; // Référence au nouvel AnimatorController que vous voulez assigner




    void Start()
    {
        // Récupère l'Animator actuellement assigné au GameObject
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
        // Récupère l'Animator actuellement assigné au GameObject
        Animator currentAnimator = GetComponent<Animator>();

        if (currentAnimator != null)
        {
            if (newAnimatorController != null)
            {
                // Met à jour l'Animator actuel avec le nouvel AnimatorController
                currentAnimator.runtimeAnimatorController = newAnimatorController;
            }
            else
            {
                Debug.LogWarning("Aucun AnimatorController assigné à la variable newAnimatorController.");
            }
        }
        else
        {
            Debug.LogWarning("Aucun Animator trouvé sur le GameObject.");
        }
    }
}

