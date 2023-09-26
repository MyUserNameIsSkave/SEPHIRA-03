using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlotSettings: MonoBehaviour
{
    //Objet servant de Pivot
    [Tooltip ("NE PAS MODIFIER")]
    public GameObject CameraHeadSupport;







    #region FOV Settings

    [Header("FOV SETTINGS")]
    [Space(10)]

    [Range(20, 70)]
    public float MaxFOV = 60f;

    [Range(20, 70)]
    public float BaseFOV = 60f;

    [Range(20, 70)]
    public float MinFOV = 20f;
    #endregion


    [Space(25)]






    [Header("CLAMP SETTINGS")]

    #region Clamp Settings

    //Pitch Settings
    [Tooltip ("Eviter d'aller dans le négatif")]
    [Range(-25f, 90f)]
    public float UpClamp;

    [Tooltip("Eviter d'aller dans le négatif")]
    [Range(-25f, 90f)]
    public float DownClamp;

    [Space(7)]


    //Yaw Settings
    [Range(-90f, 180f)]
    public float RightClamp;

    [Range(-90f, 180f)]
    public float LeftClamp;


    [Space(15)]


    //Base Camera Rotation
    [Range(-180f, 180f)]
    public float BasePitch;

    [Range(-180f, 180f)]
    public float BaseYaw;
    #endregion


    [Space(25)]


    //Gizmos' Visual Settings
    [Header("VISUALISATION SETTINGS")]
    [Space(10)]


    #region Hide or Show

    [SerializeField]
    bool GridPatern = false;

    [SerializeField]
    [Tooltip("Active un Crosshair aligné avec la camera")]
    bool CameraDirectionCrossOn = true;

    #endregion


    #region Lines Length, Sphere Size, Arc Resolution

    [SerializeField]
    [Range(0.5f, 30f)]
    private float LimitLength = 0.5f;

    [SerializeField]
    [Range(1f, 30f)]
    private float ForwardLength = 1f;


    [SerializeField]
    [Range(0, 0.15f)]
    float SphereRadius = 0.05f;


    [Tooltip("Résolution des arc de cercles")]
    [SerializeField]
    [Range(5f, 30f)]
    private int Resolution = 10;

    #endregion






    #region Working Variables

    //CameraHeadSupport Position
    Vector3 BasePosition;

    //Forward 
    Vector3 BaseDirection;



    //Straight End Points
    Vector3 UpPoint;
    Vector3 DownPoint;
    Vector3 RightPoint;
    Vector3 LeftPoint;

    //Corner Position
    Vector3 UpRightPoint;
    Vector3 UpLeftPoint;
    Vector3 DownRightPoint;
    Vector3 DownLeftPoint;



    //Arc Trace
    Vector3 LastPoint;
    Vector3 CurrentPoint;



    //Sync Camera Rotation with Variables
    private float CurrentLookPitch;
    private float CurrentLookYaw;


    #endregion



    //------------------------------------ CODE ------------------------------------




    #region BASE METHODS (Call Other)

    //Get Variables on place in world
    private void Awake()
    {
        GetVariables();
    }

    //Make sure everything update at the same time
    private void OnValidate()
    {
        GetVariables();
    }

    //Continuously update variable when selected
    private void OnDrawGizmosSelected()
    {
        GetVariables();
    }

    //Visual Feedback on each frame
    private void OnDrawGizmos()
    {
        TraceGizmos(UpClamp + DownClamp, RightClamp + LeftClamp, Resolution);
    }

    #endregion 




    #region CUSTOM METHODS (Called)

    //Calcule les Variables et appel l'Update la Rotation de la Camera
    private void GetVariables()
    {
        //Base Position
        BasePosition = CameraHeadSupport.transform.position;



        //Get Straight End Points
        UpPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(-UpClamp, 0, 0) * Vector3.forward * LimitLength);
        DownPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, 0, 0) * Vector3.forward * LimitLength);

        RightPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(0, RightClamp, 0) * Vector3.forward * LimitLength);
        LeftPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(0, -LeftClamp, 0) * Vector3.forward * LimitLength);



        //Get Corner Points
        UpRightPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(-UpClamp, RightClamp, 0) * Vector3.forward * LimitLength);
        UpLeftPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(-UpClamp, -LeftClamp, 0) * Vector3.forward * LimitLength);

        DownRightPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, RightClamp, 0) * Vector3.forward * LimitLength);
        DownLeftPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, -LeftClamp, 0) * Vector3.forward * LimitLength);


        
        UpdateCameraPosition();
    }

    //Update la Rotation de la Camera
    private void UpdateCameraPosition()
    {
        CurrentLookPitch = Mathf.Clamp(BasePitch, -UpClamp, DownClamp);
        CurrentLookYaw = Mathf.Clamp(BaseYaw, -LeftClamp, RightClamp);
        BaseDirection = BasePosition + (Quaternion.Euler(-CurrentLookPitch, CurrentLookYaw, 0) * transform.forward).normalized * (ForwardLength * LimitLength);
        CameraHeadSupport.transform.localRotation = Quaternion.Euler(CurrentLookPitch, CurrentLookYaw, 0);
    }



    //Get and Link Curve Points
    private void TraceGizmos(float PitchAngleOpening, float YawAngleOpening, int Resolution)
    {
        #region STRAIGHT

        //Forward Line
        Gizmos.color = Color.red;                                       //Define Colore of Below Gizmos
        Gizmos.DrawLine(BasePosition, CameraHeadSupport.transform.position + CameraHeadSupport.transform.forward * (ForwardLength * LimitLength));

        Gizmos.color = Color.white;                                     //Define Colore of Below Gizmos
        //BasePosition to Corners   
        Gizmos.DrawLine(BasePosition, UpRightPoint);
        Gizmos.DrawLine(BasePosition, UpLeftPoint);
        Gizmos.DrawLine(BasePosition, DownRightPoint);
        Gizmos.DrawLine(BasePosition, DownLeftPoint);

        #endregion


        #region SPHERES

        //Corners
        Gizmos.DrawWireSphere(UpRightPoint, SphereRadius);
        Gizmos.DrawWireSphere(UpLeftPoint, SphereRadius);

        Gizmos.DrawWireSphere(DownRightPoint, SphereRadius);
        Gizmos.DrawWireSphere(DownLeftPoint, SphereRadius);


        //CameraDirection
        Gizmos.color = Color.red;                                       //Define Colore of Below Gizmos
        Gizmos.DrawWireSphere(CameraHeadSupport.transform.position + CameraHeadSupport.transform.forward * (ForwardLength * LimitLength), SphereRadius);

        #endregion


        #region CURVES

            #region EXTERNAL CURVES

        Gizmos.color = Color.white;                                     //Define Colore of Below Gizmos

        //Up Curve
        LastPoint = UpRightPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = YawAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(-UpClamp, CurrentAngle - LeftClamp, 0) * Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;

        }
        Gizmos.DrawLine(CurrentPoint, UpRightPoint);

        //DOWN Curve
        LastPoint = DownRightPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = YawAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, CurrentAngle - LeftClamp, 0) * Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;
        }
        Gizmos.DrawLine(CurrentPoint, DownRightPoint);


        //RIGHT Curve
        LastPoint = UpRightPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = PitchAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(CurrentAngle - UpClamp, RightClamp, 0) * Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;
        }
        Gizmos.DrawLine(CurrentPoint, DownRightPoint);

        //LEFT Curve
        LastPoint = UpLeftPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = PitchAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(CurrentAngle - UpClamp, -LeftClamp, 0) * Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;
        }
        Gizmos.DrawLine(CurrentPoint, DownLeftPoint);

        #endregion


            #region  CENTER CURVES

        if (GridPatern)
        {
        Gizmos.color = Color.white;                                      //Define Colore of Below Gizmos

        //YAW
        LastPoint = RightPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = YawAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(0, CurrentAngle - LeftClamp, 0) * Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;

        }
        Gizmos.DrawLine(CurrentPoint, RightPoint);



        //PITCH
        LastPoint = UpPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = PitchAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(CurrentAngle - UpClamp, 0, 0) * Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;

        }
        Gizmos.DrawLine(CurrentPoint, DownPoint);
        }

        #endregion


            #region CAMERA DIRECTION CURVES

        if (CameraDirectionCrossOn)
        {
            Gizmos.color = Color.grey;                                   //Define Colore of Below Gizmos

            //YAW Lines
            LastPoint = RightPoint;
            for (int i = 0; i < Resolution; i++)
            {
                float CurrentAngle = YawAngleOpening / Resolution * i;
                CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(Mathf.Clamp(BasePitch, -UpClamp, DownClamp), CurrentAngle - LeftClamp, 0) * Vector3.forward * LimitLength);

                //Avoid strange line
                if (i != 0)
                    Gizmos.DrawLine(LastPoint, CurrentPoint);


                LastPoint = CurrentPoint;

            }
            Vector3 LastAjustedToCamera = BasePosition + transform.TransformDirection(Quaternion.Euler(Mathf.Clamp(BasePitch, -UpClamp, DownClamp), RightClamp, 0) * Vector3.forward * LimitLength);
            Gizmos.DrawLine(CurrentPoint, LastAjustedToCamera);



            //PITCH Lines
            LastPoint = UpPoint;
            for (int i = 0; i < Resolution; i++)
            {
                float CurrentAngle = PitchAngleOpening / Resolution * i;
                CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(CurrentAngle - UpClamp, Mathf.Clamp(BaseYaw, -LeftClamp, RightClamp), 0) * Vector3.forward * LimitLength);

                //Avoid strange line
                if (i != 0)
                    Gizmos.DrawLine(LastPoint, CurrentPoint);


                LastPoint = CurrentPoint;

            }
            LastAjustedToCamera = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, Mathf.Clamp(BaseYaw, -LeftClamp, RightClamp), 0) * Vector3.forward * LimitLength);
            Gizmos.DrawLine(CurrentPoint, LastAjustedToCamera);
        }

            #endregion

        #endregion







    }

    #endregion
}
