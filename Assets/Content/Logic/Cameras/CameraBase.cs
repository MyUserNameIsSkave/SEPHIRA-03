using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using GD.MinMaxSlider;

public abstract class CameraBase : MonoBehaviour, IInteractable
{
    // ----- VARIABLES -----

    [Header("     ROTATION")]
    [Space(7)]


    public bool CanBeManualySelected = true;





        [Space(15)]
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


    [MinMaxSlider(10, 110)]
    public Vector2 FOVRange;


        [Space(15)]
        [Header("     INTERACTION")]
        [Space(7)]

    public float MaxInteractionDistance = 0;





    // ----- WORKING VARIABLES -----

    //General
    private GameObject playerObject;
    protected CameraController cameraController;


    //Rotation
    [HideInInspector]
    public float BasePitch;

    [HideInInspector]
    public float BaseYaw;


    //Zoom
    [HideInInspector]
    public float currentCameraFOV;

    [HideInInspector]
    public float ZoomLeft = 0f;

    private float currentLerpedFOV = 0f;

    [HideInInspector]
    public Coroutine ZoomLerping;

    [HideInInspector]
    public bool alreadyUsed = false;


    private CameraIndicator cameraIndicatorScript;

    [HideInInspector]
    public float DistanceWithPlayer;


    private bool isFirstCamera = false;




    // ----- lOGIC -----





    private void Start()
    {
        if (this == cameraController.CurrentCamera)
        {
            isFirstCamera = true;
        }
    }







    #region INTERFACE

    public void SelectedByPlayer()
    {
        //Recieve Input


        if (MaxInteractionDistance != 0)
        {
            if (DistanceWithPlayer > MaxInteractionDistance)
            {
                return;
            }
        }



        if (CanBeManualySelected)
        {
            Interaction();
        }
    }

    public void Interaction()
    {


        //Change Camera



        cameraController.CurrentCamera = this;
        UiCamerBars.Instance.UpdateUI();


        //Send information to the new Camera that it is the new one
        Transitionned();

        cameraIndicatorScript.TransitionnedFrom();
        cameraController.CurrentCamera.alreadyUsed = true;
    }

    #endregion


    protected abstract void Transitionned();
    


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
        cameraController = GameManager.Instance.CameraController;

        cameraIndicatorScript = GetComponent<CameraIndicator>();

        currentCameraFOV = baseFOV;

        BaseYaw = BaseHorizontalRotation;
        BasePitch = -BaseVerticalRotation;
    }


    private void FixedUpdate()
    {
        DistanceWithPlayer = Vector3.Distance(transform.position, playerObject.transform.position);
    }

    #region CAMERA ROTATION

    /// <summary>
    /// The metode use to Rotate the Camera on its Pivot Point
    /// </summary>
    public void RotateCamera()
    {
        Vector2 inputs = GetCameraInput();


        if (inputs == Vector2.zero)
        {
            return;
        }
        
        
        float HorizontalInput = inputs.y;
        float VerticalInput = -inputs.x;

        BasePitch = Mathf.Clamp(BasePitch + VerticalInput, -VerticalRange.y, -VerticalRange.x);
        BaseYaw = Mathf.Clamp(BaseYaw + HorizontalInput, HorizontalRange.x, HorizontalRange.y);

        PivotPoint.transform.localRotation = Quaternion.Euler(-BasePitch, BaseYaw, 0);

        if (UiCamerBars.Instance == null)
        {
            Debug.LogError("NO CANVAS REFERENCE IN GAME MANAGER");
        }

        UiCamerBars.Instance.UpdateUI();
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
        //Variables
        ZoomLeft += ZoomIncrement;
        float currentFOV = cameraController.currentFOV;


        //Stop coroutine if can
        if (ZoomLerping != null)
        {
            StopCoroutine(ZoomLerping);
        }


        if (cameraController.ZoomDuration != 0)
        {
            //Call ZoomLerping Coroutine
            ZoomLerping = StartCoroutine(LerpFOV(currentFOV, currentFOV + ZoomLeft, cameraController.ZoomDuration));
        }
        else
        {
            cameraController.ChangeFOV(Mathf.Clamp(currentFOV + ZoomIncrement, FOVRange.x, FOVRange.y));
        }
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


            UiCamerBars.Instance.UpdateUI();


            yield return null;
        }




        //Make sure the FOV value is exact
        cameraController.ChangeFOV(endFOV);
        currentCameraFOV = endFOV;
        ZoomLeft = 0;
    }

    #endregion

}

