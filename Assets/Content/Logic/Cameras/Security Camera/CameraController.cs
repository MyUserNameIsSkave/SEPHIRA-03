using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject PlayerCameraSocket;


    #region Variables

    //Variables
    private GameObject CameraHeadSupport;

    private float BasePitch;
    private float BaseYaw;
 



    //Slot Clamps Settings
    private float UpClamp;
    private float DownClamp;

    private float RightClamp;
    private float LeftClamp;

    #endregion



    private void Awake()
    {
        #region Get the Variables

        //Get Pivot Point
        CameraHeadSupport = gameObject.GetComponent<SlotSettings>().CameraHeadSupport;

        //BaseRotation
        BasePitch = gameObject.GetComponent<SlotSettings>().BasePitch;
        BaseYaw = gameObject.GetComponent<SlotSettings>().BaseYaw;


        //Get Clamp Variables (local rotation)
        UpClamp = gameObject.GetComponent<SlotSettings>().UpClamp;
        DownClamp = gameObject.GetComponent<SlotSettings>().DownClamp;

        RightClamp = gameObject.GetComponent<SlotSettings>().RightClamp;
        LeftClamp = gameObject.GetComponent<SlotSettings>().LeftClamp;

        #endregion
    }






    //Movement controllé de la camera
    public void RotateCamera(float PitchInput, float YawInput)
    {
        BasePitch = Mathf.Clamp(BasePitch + PitchInput, -UpClamp, DownClamp);
        BaseYaw = Mathf.Clamp(BaseYaw + YawInput, -LeftClamp, RightClamp);

        CameraHeadSupport.transform.localRotation = Quaternion.Euler(BasePitch, BaseYaw, 0);
    }
}
        


