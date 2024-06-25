using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAccessibleCamera : MonoBehaviour, IEventTriggerable
{
    public CameraBase[] resetCamera;


    public void TriggerEvent()
    {
        foreach (CameraBase camera in resetCamera)
        {
            camera.accessibleCameras = new CameraBase[]{camera};
        }
    }
}
