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
            return Vector3.Distance(transform.position, targets[CurrentTarget].transform.position) / automaticAverageSpeed;
        }
    }


    private bool isControledByPlayer = false;


    [SerializeField, Tooltip("The average speed (U/s) of the drone movements.")]
    private float automaticAverageSpeed = 1f;

    [SerializeField, Tooltip("The average speed (U/s) of the drone movements.")]
    private float controledSpeed = 1f;

    [SerializeField]
    private Transform[] targets;

    private Vector3 lastPosition;
    private Vector3 currentSpeed;

    private Vector2 xRange;
    private Vector2 yRange;


    void Start()
    {
        GetMovementRange();
        StartCoroutine(CalculatesSpeed());
        AutomaticMovements();
    }


    private void Update()
    {
        if (isControledByPlayer)
        {
            ControlMovements();
        }
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




    IEnumerator CalculatesSpeed()
    {

        while (true)
        {
            lastPosition = transform.position;
            yield return new WaitForEndOfFrame();
            currentSpeed = (transform.position - lastPosition) / Time.deltaTime;
        }
    }



    private void GetMovementRange()
    {
        Transform lowestTransform = targets[0];
        Transform uppestTransform = targets[0]; 
        Transform rightestTransform = targets[0];
        Transform leftestTransform = targets[0];

        foreach (Transform target in targets)
        {
            //Height
            if (lowestTransform.transform.position.y > target.position.y)
            {
                lowestTransform = target;
            }
            if (lowestTransform.transform.position.y < target.position.y)
            {
                uppestTransform = target;
            }

            //Side
            if (lowestTransform.transform.position.x > target.position.x)
            {
                lowestTransform = target;
            }
            if (lowestTransform.transform.position.x < target.position.x)
            {
                uppestTransform = target;
            }

  

            Vector3 delta = (target.position - transform.position).normalized;
            Vector3 cross = Vector3.Cross(delta, transform.forward);

            if (cross == Vector3.zero)
            {
                // Target is straight ahead
            }
            else if (cross.y > 0)
            {
                print("Right");
            }
            else
            {
                print("Left");
            }

        }



    }


    public void ControlMovements()
    {
        //if (transform.position )




        if (Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") != 0)
        {
            transform.position += ((transform.right * -Input.GetAxis("Horizontal")) + (transform.up * Input.GetAxis("Vertical"))) * Time.deltaTime * controledSpeed;
        }


    }
}
