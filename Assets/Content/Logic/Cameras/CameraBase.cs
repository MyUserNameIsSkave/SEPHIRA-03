using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using GD.MinMaxSlider;
using static UnityEngine.InputSystem.Controls.AxisControl;


public class CameraBase : MonoBehaviour, IInteractable
{
    // ----- VARIABLES -----

    public GameObject PivotPoint;

    public GameObject CameraPoint;

        [Space(20)]

    [MinMaxSlider(-90, 90)]
    public Vector2 HorizontalRange;

    [MinMaxSlider(-90, 90)]
    public Vector2 VerticalRange;


    [Range (-180, 180)]
    public float BaseHorizontalRotation;

    [Range(-180, 180)]
    public float BaseVerticalRotation;



    // ----- WORKING VARIABLES -----

    private GameObject playerObject;
    private CameraController cameraController;

    private float BasePitch;
    private float BaseYaw;




    // ----- INTERFACE -----
    public void SelectedByPlayer()
    {
        //CHange Camera
        cameraController.currentCamera = this;
    }

    public void Interaction()
    {
        //Leave Empty
    }




    // ----- lOGIC -----


    private void OnValidate()
    {
        BasePitch = Mathf.Clamp(BaseVerticalRotation, VerticalRange.x, VerticalRange.y);
        BaseYaw = Mathf.Clamp(BaseHorizontalRotation, HorizontalRange.x, HorizontalRange.y);

        PivotPoint.transform.localRotation = Quaternion.Euler(BasePitch, BaseYaw, 0);
    }



    private void Awake()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        cameraController = playerObject.GetComponent<CameraController>();
    }


    public void RotateCamera(Vector2 inputs)
    {
        float HorizontalInput = inputs.y;
        float VerticalInput = -inputs.x;

        BasePitch = Mathf.Clamp(BasePitch + VerticalInput, -VerticalRange.y, -VerticalRange.x);
        BaseYaw = Mathf.Clamp(BaseYaw + HorizontalInput, HorizontalRange.x, HorizontalRange.y);

        PivotPoint.transform.localRotation = Quaternion.Euler(-BasePitch, BaseYaw, 0);
    }
}


