using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class CameraGizmo : MonoBehaviour
{
    [SerializeField]
    private CameraBase cameraVariables;

    [SerializeField, Range (0.3f, 5f)]
    float LimitLength;

    [SerializeField, Range(3f, 10f)]
    int Resolution;

    [SerializeField, Range(1f, 10f)]
    float ForwardLength;


    [SerializeField, Range(0.01f, 0.05f)]
    float SphereRadius;




    float BasePitch;
    float BaseYaw;

    GameObject cameraPoint;
    Vector3 BasePosition;


    Vector3 UpPoint;
    Vector3 DownPoint;

    Vector3 RightPoint;
    Vector3 LeftPoint;


    Vector3 UpRightPoint;
    Vector3 UpLeftPoint;

    Vector3 DownRightPoint;
    Vector3 DownLeftPoint;


    Vector3 LastPoint;
    Vector3 CurrentPoint;





    float UpClamp;
    float DownClamp;

    float RightClamp;
    float LeftClamp;


    [SerializeField]
    private bool CameraDirectionCrossOn;

    [SerializeField]

    private bool GridPatern;





    private void OnDrawGizmos()
    {
        TraceGizmos(DownClamp - UpClamp, RightClamp - LeftClamp, Resolution);
    }


    private void OnDrawGizmosSelected()
    {
        GetVariables();


    }





    private void GetVariables()
    {
        cameraPoint = cameraVariables.PivotPoint;
        BasePosition = cameraPoint.transform.position;


        BaseYaw = cameraVariables.BaseHorizontalRotation;
        BasePitch = cameraVariables.BaseVerticalRotation;


        DownClamp = cameraVariables.VerticalRange.x;
        UpClamp = cameraVariables.VerticalRange.y;

        RightClamp = cameraVariables.HorizontalRange.y;
        LeftClamp = cameraVariables.HorizontalRange.x;






        //Get Sides Points
        UpPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(UpClamp, 0, 0) * -Vector3.forward * LimitLength);
        DownPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, 0, 0) * -Vector3.forward * LimitLength);

        RightPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(0, RightClamp, 0) * -Vector3.forward * LimitLength);
        LeftPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(0, LeftClamp, 0) * -Vector3.forward * LimitLength);


        //Get Corners Points
        UpRightPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(UpClamp, RightClamp, 0) * -Vector3.forward * LimitLength);
        UpLeftPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(UpClamp, LeftClamp, 0) * -Vector3.forward * LimitLength);

        DownRightPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, RightClamp, 0) * -Vector3.forward * LimitLength);
        DownLeftPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, LeftClamp, 0) * -Vector3.forward * LimitLength);

    }




    private void TraceGizmos(float PitchAngleOpening, float YawAngleOpening, int Resolution)
    {
        if (!cameraPoint)
        {
            return;
        }



        #region STRAIGHT

        //Forward Line
        Gizmos.color = Color.red;                                       //Define Colore of Below Gizmos
        Gizmos.DrawLine(BasePosition, BasePosition + -cameraPoint.transform.forward * (ForwardLength * LimitLength));

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
        Gizmos.DrawWireSphere(BasePosition + -cameraPoint.transform.forward * (ForwardLength * LimitLength), SphereRadius);

        #endregion











        #region CURVES


        #region EXTERNAL CURVES

        Gizmos.color = Color.white;                                     //Define Colore of Below Gizmos

        //Up Curve
        LastPoint = UpRightPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = YawAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(UpClamp, CurrentAngle + LeftClamp, 0) * -Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;

        }
        Gizmos.DrawLine(CurrentPoint, UpRightPoint);

        ////DOWN Curve
        LastPoint = DownRightPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = YawAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, CurrentAngle + LeftClamp, 0) * -Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;
        }
        Gizmos.DrawLine(CurrentPoint, DownRightPoint);


        ////RIGHT Curve
        LastPoint = UpRightPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = PitchAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(CurrentAngle + UpClamp, RightClamp, 0) * -Vector3.forward * LimitLength);

            //Avoid strange line
            if (i != 0)
                Gizmos.DrawLine(LastPoint, CurrentPoint);


            LastPoint = CurrentPoint;
        }
        Gizmos.DrawLine(CurrentPoint, DownRightPoint);

        ////LEFT Curve
        LastPoint = UpLeftPoint;
        for (int i = 0; i < Resolution; i++)
        {
            float CurrentAngle = PitchAngleOpening / Resolution * i;
            CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(CurrentAngle + UpClamp, LeftClamp, 0) * -Vector3.forward * LimitLength);

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
                CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(0, CurrentAngle + LeftClamp, 0) * -Vector3.forward * LimitLength);

                //Avoid strange line
                if (i != 0 && DownClamp < 0 && 0 < UpClamp)
                    Gizmos.DrawLine(LastPoint, CurrentPoint);


                LastPoint = CurrentPoint;

            }
            if (DownClamp < 0 && 0 < UpClamp)
                Gizmos.DrawLine(CurrentPoint, RightPoint);



            //PITCH
            LastPoint = UpPoint;
            for (int i = 0; i < Resolution; i++)
            {
                float CurrentAngle = PitchAngleOpening / Resolution * i;
                CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(CurrentAngle + UpClamp, 0, 0) * -Vector3.forward * LimitLength);

                //Avoid strange line
                if (i != 0 && LeftClamp < 0 && 0 < RightClamp)
                    Gizmos.DrawLine(LastPoint, CurrentPoint);


                LastPoint = CurrentPoint;

            }
            if (LeftClamp < 0 && 0 < RightClamp)
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
                CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(Mathf.Clamp(BasePitch, DownClamp, UpClamp), CurrentAngle + LeftClamp, 0) * -Vector3.forward * LimitLength);

                //Avoid strange line
                if (i != 0)
                    Gizmos.DrawLine(LastPoint, CurrentPoint);


                LastPoint = CurrentPoint;

            }
            Vector3 LastAjustedToCamera = BasePosition + transform.TransformDirection(Quaternion.Euler(Mathf.Clamp(BasePitch, DownClamp, UpClamp), RightClamp, 0) * -Vector3.forward * LimitLength);
            Gizmos.DrawLine(CurrentPoint, LastAjustedToCamera);



            //PITCH Lines
            LastPoint = UpPoint;
            for (int i = 0; i < Resolution; i++)
            {
                float CurrentAngle = PitchAngleOpening / Resolution * i;
                CurrentPoint = BasePosition + transform.TransformDirection(Quaternion.Euler(CurrentAngle + UpClamp, Mathf.Clamp(BaseYaw, LeftClamp, RightClamp), 0) * -Vector3.forward * LimitLength);

                //Avoid strange line
                if (i != 0)
                    Gizmos.DrawLine(LastPoint, CurrentPoint);


                LastPoint = CurrentPoint;

            }
            LastAjustedToCamera = BasePosition + transform.TransformDirection(Quaternion.Euler(DownClamp, Mathf.Clamp(BaseYaw, LeftClamp, RightClamp), 0) * -Vector3.forward * LimitLength);
            Gizmos.DrawLine(CurrentPoint, LastAjustedToCamera);
        }

        #endregion

        

        #endregion


    }
}
