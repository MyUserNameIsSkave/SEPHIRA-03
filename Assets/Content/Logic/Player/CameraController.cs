using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
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

        set
        { //Stop Zoom Lerping
            //if (CurrentCamera.ZoomLerping != null)
            //{
            //    CurrentCamera.StopCoroutine(CurrentCamera.ZoomLerping);
            //    CurrentCamera.ZoomLeft = 0;
            //}



            //Set FOV to New Camerra FOV
            if (cameraReference != null)
            {
                ChangeFOV(Mathf.Clamp(value.currentCameraFOV, value.FOVRange.x, value.FOVRange.y));
            }

            //Change Reference
            _currentCamera = value;
        }
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
    [Tooltip ("Can't be equal to 0")]
    public float ZoomDuration;




    // ----- WORKING VARIABLES -----

    [HideInInspector]
    public Camera cameraReference;



    [HideInInspector]
    public float AjustedRotationSensivity;


    //the FOV of the Camera before launching. Used to adjuste the sensitivity of the camera throughout  the FOV change
    [Tooltip ("The FOV of the Camera before launching. Used to adjuste the sensitivity of the camera throughout  the FOV change")]
    private float referenceFOV;



    [HideInInspector]
    public float currentFOV;

    //Panic button
    private CameraBase[] cameraArray;
    private GameObject binah;


    // Coroutines
    Coroutine moveCamera = null;




    // ----- LOGIC -----


    private void Awake()
    {
        cameraReference = GameManager.Instance.mainCamera;
        cameraReference.fieldOfView = CurrentCamera.baseFOV;
        referenceFOV = cameraReference.fieldOfView;
        currentFOV = referenceFOV;

        cameraArray = FindObjectsOfType<CameraBase>();
        binah = GameManager.Instance.Binah;
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
        cameraReference.fieldOfView = currentFOV;
    }





    //Camera Panic Button
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            float nearestDistance = Mathf.Infinity;
            CameraBase nearestCamera = null;


            foreach (CameraBase camera in cameraArray)
            {
                if (camera.alreadyUsed)
                {
                    float currentDistance = Vector3.Distance(binah.transform.position, camera.transform.position);

                    if (currentDistance < nearestDistance)
                    {
                        nearestDistance = currentDistance;
                        nearestCamera = camera;
                    }
                }

                if (nearestCamera != null && nearestCamera != this)
                {
                    CurrentCamera = nearestCamera;
                }
            }
        }
    }
}
