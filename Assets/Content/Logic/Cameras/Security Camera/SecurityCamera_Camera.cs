using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera_Camera : CameraBase
{
    protected override void Transitionned()
    {
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
