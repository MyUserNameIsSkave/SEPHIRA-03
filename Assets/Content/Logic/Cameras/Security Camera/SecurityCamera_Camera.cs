using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera_Camera : CameraBase
{






    //Same in other cameras
    protected override void Transitionned()
    {
        if (binahJoinTargetPosition != null && binahMaxDistanceBeforeAutoJoin != 0f)
        {
            if (binahMaxDistanceBeforeAutoJoin <= Vector3.Distance(GameManager.Instance.Binah.transform.position, binahJoinTargetPosition.position) || GameManager.Instance.BinahManager.IsAutomaticalyMovingToCamera)
            {
                if (!GameManager.Instance.BinahManager.InRestrictiveZone)
                {
                    if (!alreadyUsed)
                    {
                        GameManager.Instance.BinahManager.SendBinahToLocation(binahJoinTargetPosition.position);

                    }
                }
            }
        }

        return;
    }

    private void Start()
    {
        if (cameraController.CurrentCamera == this)
        {
            alreadyUsed = true;
        }
    }

}
