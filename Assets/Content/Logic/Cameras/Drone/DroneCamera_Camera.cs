using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DroneCamera_Camera : CameraBase
{


    //Same in other cameras
    protected override void Transitionned()
    {
        GetComponent<DroneMovements>().TakeControl();
        isPossessed = true;

        if (binahJoinTargetPosition != null && binahMaxDistanceBeforeAutoJoin != 0f)
        {
            if (binahMaxDistanceBeforeAutoJoin <= Vector3.Distance(GameManager.Instance.Binah.transform.position, binahJoinTargetPosition.position) || GameManager.Instance.BinahManager.IsAutomaticalyMovingToCamera)
            {
                if (!GameManager.Instance.BinahManager.InRestrictiveZone)
                {
                    GameManager.Instance.BinahManager.SendBinahToLocation(binahJoinTargetPosition.position);
                }
            }
        }

        return;
    }

    private void Start()
    {
        // VerticalAdjustment = -1;


        if (cameraController.CurrentCamera == this)
        {
            alreadyUsed = true;
        }
    }


    private bool isPossessed = false;

    private void FixedUpdate()
    {
        isCameraAccessible = GameManager.Instance.CameraController.CurrentCamera.accessibleCameras.Contains(this);

        if (!isPossessed)
        {
            return;
        }

        if (GameManager.Instance.CameraController.CurrentCamera != this)
        {
            isPossessed = false;
            GetComponent<DroneMovements>().LoseControl();
        }
    }
}
