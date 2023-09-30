using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    [Tooltip("Définit le slot de camera de base")]
    [SerializeField]
    GameObject BaseCameraSlot;



    [Space(30f)]



    #region Settings

    [Header("Sensivity")]
    [Space(10)]

    [SerializeField]
    [Range(0f, 10f)]
    private float MovementSensivity = 0.5f;

    [SerializeField]
    private AnimationCurve MovementStrength;

    [Space(5)]

    [Range(1f, 20f)]
    public float ZoomSensivity = 1f;

    [Space(20)]



    [Header("Trigger")]
    [Space(10)]

    [SerializeField]
    [Range(0f, 1f)]
    private float WidthBorder;

    [SerializeField]
    [Range(0f, 1f)]
    private float HeightBorder;

    #endregion





    //A définir de façon automatique plus tard









    #region Working Variables

    //References
    private CameraController PossessedCameraController;
    private GameObject PlayerCameraSocket;
    private GameObject CurrentCameraSlot;
    private Camera MyCamera;

    

    [SerializeField]
    private GameObject CameraMovementPanel;                 //Panel d'HUD servant de feedback au mouvement



    //FOV
    private float MaxFOV;
    private float BaseFOV;
    private float CurrentFOV;
    private float MinFOV;
    private float WidthPositionPercentage;
    private float HeightPositionhPercentage;
    private Quaternion previousRotation;





    //Movements
    private bool CanMoveCamera;
    private float YawInput;
    private float PitchInput;
    private float MovementSensivityAjustement;


    #endregion



    // ----------------------------------------------------------------- //



    #region BASE METHODS (Call)

    //References & Base Camera Slot
    private void Start()
    {
        MyCamera = transform.GetChild(0).GetComponent<Camera>();
  

        //Define Base Camera
        if (BaseCameraSlot != null)
        {
            ClickOnCamera(BaseCameraSlot);
        }
    }



    //Manage Movement Method
    private void Update()
    {
        //Méthode n°1: Pousser la camera en mettant le curseur au bord de l'ecran
        RotateViewPUSH();

        // Obtenir la rotation actuelle du gameObject
        Quaternion currentRotation = transform.rotation;

        // Mettre à jour la rotation précédente
        previousRotation = currentRotation;
    }


    //Align Camera to View
    private void LateUpdate()
    {
        AlignCameraWithView();
    }

    #endregion



    // ----------------------------------------------------------------- //



    #region CUSTOM METHODS (Called)

    //Change Camera (from MouseCollision.cs)
    public void ClickOnCamera(GameObject CameraSlot)
    {
        CurrentCameraSlot = CameraSlot;

        //Position & Rotation
        transform.position = CameraSlot.transform.GetChild(0).GetChild(0).GetChild(0).transform.position;
        transform.rotation = CameraSlot.transform.GetChild(0).GetChild(0).GetChild(0).transform.rotation;

        PossessedCameraController = CameraSlot.GetComponent<CameraController>();
        //////////////////////////////////PlayerCameraSocket = PossessedCameraController.PlayerCameraSocket;

 

        //FOV
        SlotSettings Settings = CameraSlot.GetComponent<SlotSettings>();
        MinFOV = Settings.MinFOV;
        BaseFOV = Settings.BaseFOV;
        MaxFOV = Settings.MaxFOV;

        MyCamera.fieldOfView = BaseFOV;


        //Set up the adaptative sensiity based on zoom level
        CurrentFOV = BaseFOV;
        MovementSensivityAjustement = BaseFOV / CurrentFOV;



    }




    // ------------------------------ CAMERA MOVEMENT ------------------------------ //


    //INPUT SYSTEM - Unlock Movement
    private void OnMoveCamera(InputValue Value)
    {
        float State = Value.Get<float>();

        //Activates & Deactivates (Variable and UI)
        if (State == 1f)
        {
            CameraMovementPanel.SetActive(true);
            CanMoveCamera = true;
        }
        else
        {
            CameraMovementPanel.SetActive(false);
            CanMoveCamera = false;
        }
    }




    //INPUT SYSTEM - Zoom
    private void OnZoom(InputValue Value)
    {
        CurrentFOV = Mathf.Clamp(MyCamera.fieldOfView - Value.Get<float>() * ZoomSensivity, MinFOV, MaxFOV);

        MyCamera.fieldOfView = CurrentFOV;
        CurrentCameraSlot.GetComponent<SlotSettings>().BaseFOV = CurrentFOV;


        //Update the adaptative sensity based on zoom level
        MovementSensivityAjustement = BaseFOV / CurrentFOV;
    }




    //Get Input and Move Camera
    private void RotateViewPUSH()
    {
        //If Movement Key not Held
        if (CanMoveCamera == false)
            return;
        

        //Mouse Position Relative to Border
        WidthPositionPercentage = Mathf.Clamp((((Input.mousePosition.x - Screen.width / 2) / Screen.width) * 2), -1, 1);
        HeightPositionhPercentage = Mathf.Clamp((((Input.mousePosition.y - Screen.height / 2) / Screen.height) * 2), -1, 1);


        //Mouse Position to Input
        if (Mathf.Abs(WidthPositionPercentage) >= 1 - WidthBorder)
        {
            float PositionInBorder = Mathf.InverseLerp(1 - WidthBorder, 1, Mathf.Abs(WidthPositionPercentage));
            YawInput = (MovementStrength.Evaluate(PositionInBorder) * Mathf.Sign(WidthPositionPercentage)) * MovementSensivity * Time.deltaTime * 10;           //*10 pour ratrapper le DeltaTime qui permet de smooth le mouvement
        }
        else
        {
            YawInput = 0f;
        }

        if (Mathf.Abs(HeightPositionhPercentage) >= 1 - HeightBorder)
        {
            float PositionInBorder = Mathf.InverseLerp(1 - HeightBorder, 1, Mathf.Abs(HeightPositionhPercentage));
            PitchInput = (MovementStrength.Evaluate(PositionInBorder) * Mathf.Sign(HeightPositionhPercentage)) * MovementSensivity * Time.deltaTime * 10;       //*10 pour ratrapper le DeltaTime qui permet de smooth le mouvement
        }
        else
        {
            PitchInput = 0f;
        }


        //Apply Input
        /////////////////////////////////////////////PossessedCameraController.RotateCamera(-PitchInput / MovementSensivityAjustement, YawInput / MovementSensivityAjustement);
    }


    //Move Mesh
    private void AlignCameraWithView()
    {
        if (PossessedCameraController != null)
        {
            transform.position = PlayerCameraSocket.transform.position;
            transform.rotation = PlayerCameraSocket.transform.rotation;
        }
    }
    #endregion
}