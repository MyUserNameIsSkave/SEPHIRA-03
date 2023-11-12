using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using GD.MinMaxSlider;
using static UnityEngine.InputSystem.Controls.AxisControl;
using System.Transactions;

public class CameraBase : MonoBehaviour, IInteractable
{
    // ----- VARIABLES -----

        [Header ("     ROTATION")]
        [Space(7)]


    public GameObject PivotPoint;

    public GameObject CameraPoint;

        [Space(7)]

    [MinMaxSlider(-90, 90)]
    public Vector2 HorizontalRange;

    [MinMaxSlider(-90, 90)]
    public Vector2 VerticalRange;

        [Space (7)]

    [Range(-180, 180)]
    public float BaseHorizontalRotation;

    [Range(-180, 180)]
    public float BaseVerticalRotation;


        [Space(15)]
        [Header("     ZOOM")]
        [Space(7)]

    public float baseFOV;


    [SerializeField, MinMaxSlider(10, 110)]
    private Vector2 FOVRange;
    [SerializeField]

    private float ZoomDuration;



    // ----- WORKING VARIABLES -----

    //General
    private GameObject playerObject;
    private CameraController cameraController;

    //Rotation
    private float BasePitch;
    private float BaseYaw;

    //Zoom
    private float currentCameraFOV;
    private float ZoomLeft = 0f;
    private float currentLerpedFOV = 0f;
    private Coroutine ZoomLerping;








    // ----- lOGIC -----



    #region INTERFACE

    public void SelectedByPlayer()
    {
        //Recieve Input
        Interaction();
    }

    public void Interaction()
    {
        //Change Camera
        cameraController.currentCamera = this;
        cameraController.ChangeFOV(Mathf.Clamp(currentCameraFOV, FOVRange.x, FOVRange.y));
    }

    #endregion



    private void OnValidate()
    {
        BasePitch = Mathf.Clamp(BaseVerticalRotation, VerticalRange.x, VerticalRange.y);
        BaseYaw = Mathf.Clamp(BaseHorizontalRotation, HorizontalRange.x, HorizontalRange.y);

        PivotPoint.transform.localRotation = Quaternion.Euler(BasePitch, BaseYaw, 0);
    }



    private void Awake()
    {
        currentCameraFOV = baseFOV;

        playerObject = GameObject.FindGameObjectWithTag("Player");
        cameraController = playerObject.GetComponent<CameraController>();
    }


    private void Start()
    {
        BaseYaw = BaseHorizontalRotation;
        BasePitch = -BaseVerticalRotation;
    }




    #region CAMERA ROTATION

    /// <summary>
    /// The metode use to Rotate the Camera on its Pivot Point
    /// </summary>
    public void RotateCamera()
    {
        Vector2 inputs = GetCameraInput();

        float HorizontalInput = inputs.y;
        float VerticalInput = -inputs.x;

        BasePitch = Mathf.Clamp(BasePitch + VerticalInput, -VerticalRange.y, -VerticalRange.x);
        BaseYaw = Mathf.Clamp(BaseYaw + HorizontalInput, HorizontalRange.x, HorizontalRange.y);

        PivotPoint.transform.localRotation = Quaternion.Euler(-BasePitch, BaseYaw, 0);
    }



    /// <summary>
    /// Return the Rotation values from the mouse position relative to the edges of the screen
    /// </summary>
    public Vector2 GetCameraInput()
    {
        //Mouse Position Relative to Border
        float WidthPositionPercentage = Mathf.Clamp((((Input.mousePosition.x - Screen.width / 2) / Screen.width) * 2), -1, 1);
        float HeightPositionhPercentage = Mathf.Clamp((((Input.mousePosition.y - Screen.height / 2) / Screen.height) * 2), -1, 1);

        float HorizontalInput = 0f;
        float VerticalInput = 0f;


        //Mouse Position to Input
        if (Mathf.Abs(WidthPositionPercentage) >= 1 - cameraController.HorizontalThreshold)
        {
            float PositionInBorder = Mathf.InverseLerp(1 - cameraController.HorizontalThreshold, 1, Mathf.Abs(WidthPositionPercentage));
            HorizontalInput = (cameraController.SensivityProgresion.Evaluate(PositionInBorder) * Mathf.Sign(WidthPositionPercentage)) * cameraController.AjustedRotationSensivity * Time.deltaTime * 10;
        }


        if (Mathf.Abs(HeightPositionhPercentage) >= 1 - cameraController.VerticalThreshold)
        {
            float PositionInBorder = Mathf.InverseLerp(1 - cameraController.VerticalThreshold, 1, Mathf.Abs(HeightPositionhPercentage));
            VerticalInput = (cameraController.SensivityProgresion.Evaluate(PositionInBorder) * Mathf.Sign(HeightPositionhPercentage)) * cameraController.AjustedRotationSensivity * Time.deltaTime * 10;
        }


        return new Vector2(VerticalInput, HorizontalInput);
    }

    #endregion






    #region CAMERA ZOOM

    /// <summary>
    /// The basic Zooming Method, Zooming by changing the FOV. Include Zoom Smoothing.
    /// </summary>
    public void Zoom(float ZoomIncrement)
    {
        //Stop if alreaddy at the limit
        if (cameraController.currentFOV + ZoomIncrement == FOVRange.x || cameraController.currentFOV + ZoomIncrement == FOVRange.y)
        {
            ZoomLeft = 0;
            return;
        }


        //Variables
        ZoomLeft += ZoomIncrement;
        float currentFOV = cameraController.currentFOV;


        //Stop coroutine if can
        if (ZoomLerping != null)
        {
            StopCoroutine(ZoomLerping);
        }


        //Call ZoomLerping Coroutine
        ZoomLerping = StartCoroutine(LerpFOV(currentFOV, currentFOV + ZoomLeft, ZoomDuration));
    }



    private IEnumerator LerpFOV(float startFOV, float endFOV, float duration)
    {
        //Initialization
        float startTime = Time.time;
        float elapsedTime = 0f;



        while (elapsedTime < duration)
        {
            // Progression
            float t = elapsedTime / duration;
            elapsedTime = Time.time - startTime;


            // Lerp
            float lerpedFOV = Mathf.Lerp(startFOV, endFOV, t);


            //Stop if alreaddy at the limit
            if (lerpedFOV < FOVRange.x || lerpedFOV > FOVRange.y)
            {
                ZoomLeft = 0;
                yield break;
            }


            // Apply Variables Values
            currentLerpedFOV = lerpedFOV;
            currentCameraFOV = lerpedFOV;
            ZoomLeft -= lerpedFOV - currentLerpedFOV;

            //Apply FOV
            cameraController.ChangeFOV(lerpedFOV);


            yield return null;
        }



        //Make sure the FOV value is exact
        cameraController.ChangeFOV(endFOV);
        currentCameraFOV = endFOV;
        ZoomLeft = 0;
    }

    #endregion

}

