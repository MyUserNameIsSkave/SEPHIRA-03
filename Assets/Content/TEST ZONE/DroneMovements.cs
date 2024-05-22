using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class DroneMovements : MonoBehaviour
{


    private int CurrentTarget
    {
        set
        {
            if (value == targets.Length)
            {
                currentTarget = 0;
                CurrentTarget = 0;
                return;
            }

            currentTarget = value;
        }
        get
        {
            return currentTarget;
        }
    }

    private int currentTarget;



    private float currentTimeBetweenTargets;
    private float CurrentTimeBetweenTargets
    {
        set
        {
            currentTimeBetweenTargets = value;
        }
        get
        {
            return Vector3.Distance(transform.position, targets[CurrentTarget].transform.position) / targetSpeed;
        }
    }


    private bool isControledByPlayer = false;



    [SerializeField, Tooltip("The average speed (U/s) of the drone movements.")]
    private float targetSpeed = 1f;

    [SerializeField]
    private Transform[] targets;




    void Start()
    {
        AutomaticMovements();
    }




    private void AutomaticMovements()
    {
        CurrentTarget += 1;
        transform.DOMove(targets[CurrentTarget].transform.position, CurrentTimeBetweenTargets).SetEase(Ease.InOutQuad).onComplete = AutomaticMovements;
    }

    public void TakeControl()
    {
        isControledByPlayer = true;
        DOTween.KillAll();
        transform.DOMove(transform.position + ((targets[CurrentTarget].transform.position - transform.position).normalized * 1), 1.25f).SetEase(Ease.OutQuad);
    }

    public void LoseControl()
    {
        isControledByPlayer = false;
        DOTween.KillAll();
        CurrentTarget -= 1;
        AutomaticMovements();
    }

    public void ControlMovements()
    {
        //Recupérer la zone jouable dans le start a partir de deux point. 
        //Definir un plan.
        //Definir la range de mouvement possible.

            //OU

        //Utiliser un Dot Product avec la target vers laquel on va pour savoir si on l'a dépassé ou pas ? Et lock le mouvement si on l'a déplassé (avec un offset pour pas trop decaller)
        

        //Récupérer les input.
        //Press         -->     Accelerer avec Tween.
        //Tween fini    -->     Continuer le mouvement.
        //Release       -->     Ralentissement avec Tween.
    }
}
