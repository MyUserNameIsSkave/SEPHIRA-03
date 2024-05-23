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

    [SerializeField]
    private GameObject movementRangeBox;


    [SerializeField, Tooltip("The average speed (U/s) of the drone movements.")]
    private float automaticAverageSpeed = 1f;

    [SerializeField, Tooltip("The average speed (U/s) of the drone movements.")]
    private float controledSpeed = 1f;

    [SerializeField]
    private Transform[] targets;

    private Vector3 lastPosition;
    [HideInInspector]
    public Vector3 currentSpeed;

    [HideInInspector]
    public bool IsInsideMovementRange = true;



    void Start()
    {
        StartCoroutine(CalculatesSpeed());
        AutomaticMovements();
    }


    private void Update()
    {
        if (movementRangeBox == null)
        {
            Debug.LogError("Empty Reference");
        }

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



    public void ControlMovements()
    {
        if (Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") != 0)
        {
            if (IsInsideMovementRange)
            {
                transform.position += ((transform.right * -Input.GetAxis("Horizontal")) + (transform.up * Input.GetAxis("Vertical"))) * Time.deltaTime * controledSpeed;
            }
            else
            {
                if (Vector3.Distance(movementRangeBox.transform.position, transform.position) > 
                    Vector3.Distance(movementRangeBox.transform.position, transform.position + transform.right * -Input.GetAxis("Horizontal") + transform.up * Input.GetAxis("Vertical")))
                {
                    transform.position += ((transform.right * -Input.GetAxis("Horizontal")) + (transform.up * Input.GetAxis("Vertical"))) * Time.deltaTime * controledSpeed;
                }

            }

        }
    }
}
