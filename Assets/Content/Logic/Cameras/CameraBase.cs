using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using GD.MinMaxSlider;
using Unity.VisualScripting;
using System.Linq;

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

    public CameraBase[] accessibleCameras;

    [HideInInspector]
    public bool isCameraAccessible;



    [Space(15)]
    [Header("    AUTO BINAH JOIN")]
    [Space(20)]


    public float binahMaxDistanceBeforeAutoJoin;

    public Transform binahJoinTargetPosition;



    [Space(15)]
    [Header("    CAMERA SOUNDS")]
    [Space(5)]

    public AudioSource CameraRotate;

    public AudioSource CameraZoomIn;
    public AudioSource CameraZoomOut;

    public AudioSource CameraSwitch;


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
    public bool alreadyUsed = false;


    private CameraIndicator cameraIndicatorScript;

    [HideInInspector]
    public float DistanceWithPlayer;


    private bool isFirstCamera = false;

    protected float VerticalAdjustment = 1f;






    // ----- lOGIC -----

    
    private void Start()
    {


    }


    #region INTERFACE

    public void SelectedByPlayer()
    {
        //Recieve Input

        if (GameManager.Instance.playerInputLocked)
        {
            return;
        }


        if (GameManager.Instance.CameraController.CurrentCamera.accessibleCameras.Length != 0)
        {
            if (!isCameraAccessible)
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

        if (!CameraSwitch.isPlaying)
        {
            CameraSwitch.Play();
        }
           

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
        alreadyUsed = false;


        StartCoroutine(SmoothZoom());


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
        isCameraAccessible = GameManager.Instance.CameraController.CurrentCamera.accessibleCameras.Contains(this);
    }




    #region CAMERA ROTATION

    /// <summary>
    /// The metode use to Rotate the Camera on its Pivot Point
    /// </summary>
    public void RotateCamera()
    {
        Vector2 inputs = GetCameraInput();
        bool isPitchAtLimit = BasePitch == -VerticalRange.y || BasePitch == -VerticalRange.x;
        bool isYawAtLimit = BaseYaw == HorizontalRange.x || BaseYaw == HorizontalRange.y;


        if (inputs == Vector2.zero)
        {
            CameraRotate.Stop();
            return;
        }

        if (!isPitchAtLimit && !isYawAtLimit)
        {
            if (!CameraRotate.isPlaying)
            {
                CameraRotate.Play();
            }
        }
        else 
        {
            // Check if the player is trying to move beyond the limits
           

            bool isTryingToMovePitch = -inputs.x != 0;
            bool isTryingToMoveYaw = inputs.y != 0;

            if (isPitchAtLimit && isTryingToMoveYaw && !isYawAtLimit || isYawAtLimit && isTryingToMovePitch && !isPitchAtLimit)
            {
               
                if (!CameraRotate.isPlaying)
                {
                    CameraRotate.Play();
                }
            }
            else
            {
                CameraRotate.Stop();
            }
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
            VerticalInput = (cameraController.SensivityProgresion.Evaluate(PositionInBorder) * Mathf.Sign(HeightPositionhPercentage)) * cameraController.AjustedRotationSensivity * Time.deltaTime * 10 * VerticalAdjustment;
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
        if (Mathf.Sign(zoomOffset) == Mathf.Sign(ZoomIncrement))
        {
            zoomOffset += ZoomIncrement;
        }
        else
        {
            //Make the Zoom Snappier.
            zoomOffset = 0;
            zoomOffset += ZoomIncrement;
        }

        // Vérifier si le FOV est déjà à sa valeur maximale ou minimale
        bool isAtMaxFOV = currentCameraFOV >= FOVRange.y;
        bool isAtMinFOV = currentCameraFOV <= FOVRange.x;

        // Play the appropriate sound based on the zoom direction
        if (ZoomIncrement > 0) // Zooming in
        {
            if (!CameraZoomIn.isPlaying && !isAtMaxFOV)
            {
                CameraZoomIn.Play();
            }
        }
        else if (ZoomIncrement < 0 && !isAtMinFOV) // Zooming out
        {
            if (!CameraZoomOut.isPlaying)
            {
                CameraZoomOut.Play();
            }
        }
        // Vérifier si le FOV a atteint sa valeur maximale ou minimale après la mise à jour
        bool isAtMaxFOVAfterUpdate = currentCameraFOV >= FOVRange.y;
        bool isAtMinFOVAfterUpdate = currentCameraFOV <= FOVRange.x;

        // Arrêter les sons associés si le FOV a atteint sa valeur maximale ou minimale
        if (isAtMaxFOVAfterUpdate)
        {
            CameraZoomIn.Stop();
        }
        if (isAtMinFOVAfterUpdate)
        {
            CameraZoomOut.Stop();
        }
    }

    private float zoomOffset;

    //Settings
    private float zoomSmoothing = 85f;

    private float smallZoomSmoothing = 0.02f;
    private float bigZoomSmoothing = 3f;
    private float bigZoomThershold = 12f;




    IEnumerator SmoothZoom()
    {
        yield return new WaitForNextFrameUnit();

        while (true)
        {
            if (cameraController.CurrentCamera == this)
            {

                float adjustment = Mathf.Lerp(smallZoomSmoothing, bigZoomSmoothing, Mathf.Clamp01(Mathf.Abs(zoomOffset) / bigZoomThershold));


                if (Mathf.Abs(zoomOffset) >= zoomSmoothing * Time.deltaTime * adjustment)
                {
                    float offsetType = Mathf.Sign(zoomOffset);

                    zoomOffset -= zoomSmoothing * offsetType * Time.deltaTime * adjustment;
                    currentCameraFOV = Mathf.Clamp(currentCameraFOV + zoomSmoothing * offsetType * Time.deltaTime * adjustment, FOVRange.x, FOVRange.y);

                    cameraController.ChangeFOV(currentCameraFOV);

                    yield return 0;

                }
                else
                {
                    // Stop the sounds when the zoom ends
                    if (CameraZoomIn.isPlaying)
                    {
                        CameraZoomIn.Stop();
                    }
                    if (CameraZoomOut.isPlaying)
                    {
                        CameraZoomOut.Stop();
                    }

                    yield return 0;
                }
            }
            yield return 0;
        }
    }


    #endregion

}

