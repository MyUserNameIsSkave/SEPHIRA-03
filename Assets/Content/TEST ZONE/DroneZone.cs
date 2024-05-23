using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class DroneZone : MonoBehaviour
{
    [SerializeField]
    private DroneMovements drone;


    private void Start()
    {
        if (drone == null)
        {
            Debug.LogError("Empty Reference");
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        drone.IsInsideMovementRange = true;
        print("enter");
    }

    private void OnTriggerExit(Collider other)
    {
        drone.IsInsideMovementRange = false;
        print("exit");

        Vector3 basePosition = drone.transform.position;
        Vector3 currSpeed = drone.currentSpeed;
        print(currSpeed);

        drone.transform.DOMove(basePosition + currSpeed / 4, 0.5f).SetEase(Ease.OutQuad);

    }
}
