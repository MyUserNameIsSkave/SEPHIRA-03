using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class CameraController : MonoBehaviour
{
    // ----- SETTINGS VARIABLES -----

    [SerializeField, Tooltip ("Serializable only for debugging purpose")]
    private CameraBase _currentCamera;

    public CameraBase CurrentCamera
    {
        get { return _currentCamera; }
        set { _currentCamera = value; ChangeFOV(Mathf.Clamp(value.currentCameraFOV, value.FOVRange.x, value.FOVRange.y)); }
    }




    [Space(15)]
        [Header("     ROTATION")]
        [Space(7)]

    [Range(0f, 1f)]
    public float HorizontalThreshold = 0.2f;

    [Range(0f, 1f)]
    public float VerticalThreshold = 0.2f;

        [Space(7)]

    public AnimationCurve SensivityProgresion;

    [SerializeField]
    private float RotationSensivity;

        [Space(15)]
        [Header("     ZOOM")]
        [Space(7)]

    public float ZoomSensivity;




    // ----- WORKING VARIABLES -----

    [HideInInspector]
    public Camera camera;



    [HideInInspector]
    public float AjustedRotationSensivity;


    //the FOV of the Camera before launching. Used to adjuste the sensitivity of the camera throughout  the FOV change
    [Tooltip ("The FOV of the Camera before launching. Used to adjuste the sensitivity of the camera throughout  the FOV change")]
    private float referenceFOV;



    [HideInInspector]
    public float currentFOV;



    // Coroutines
    Coroutine moveCamera = null;










    // ----- LOGIC -----


    private void Awake()
    {
        camera = Camera.main;
        camera.fieldOfView = CurrentCamera.baseFOV;
        referenceFOV = camera.fieldOfView;
        currentFOV = referenceFOV;
    }




    private void LateUpdate()
    {
        transform.position = CurrentCamera.CameraPoint.transform.position;
        transform.rotation = CurrentCamera.CameraPoint.transform.rotation;
    }


    #region CAMERA ROTATION

    //Start ans Stop MoveCamera Coroutine depending on the Inputs
    private void OnCameraRotation(InputValue value)
    {
        

        if (value.Get<float>() != 0)
        {
            moveCamera = StartCoroutine(CameraRotation());
        }
        else
        {
            StopCoroutine(moveCamera);
        }
    }

    // MoveCamera Coroutine, Responsible for Camera Movement
    private IEnumerator CameraRotation()
    {
        while (true)
        {
            // SHOULD ADD TIME DETLA TIME POUR LE BUILD, EN EDITOR CA POSE DES PROBLEME
            AjustedRotationSensivity = RotationSensivity * (currentFOV / referenceFOV);
            CurrentCamera.RotateCamera();
            yield return null;
        }
    }

    #endregion





    private void OnZoom(InputValue value)
    {
        CurrentCamera.Zoom(value.Get<float>() * ZoomSensivity);
    }


    public void ChangeFOV(float newFOV)
    {
        currentFOV = newFOV;
        camera.fieldOfView = currentFOV;
    }

}
