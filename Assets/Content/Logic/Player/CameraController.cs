using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip ("Serializable only for debugging purpose")]
    public CameraBase currentCamera;

        [Space(20)]

    [SerializeField, Range(0f, 1f)]
    private float HorizontalThreshold = 0.2f;

    [SerializeField, Range(0f, 1f)]
    private float VerticalThreshold = 0.2f;

        [Space(20)]

    [SerializeField]
    private AnimationCurve SensivityProgresion;

        [Space(20)]

    [SerializeField]
    private float Sensivity;




    private void Update()
    {

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            currentCamera.RotateCamera(GetCameraInput());
        }
    }

    private void LateUpdate()
    {
        transform.position = currentCamera.CameraPoint.transform.position;
        transform.rotation = currentCamera.CameraPoint.transform.rotation;
    }




    public Vector2 GetCameraInput()
    {
        //Mouse Position Relative to Border
        float WidthPositionPercentage = Mathf.Clamp((((Input.mousePosition.x - Screen.width / 2) / Screen.width) * 2), -1, 1);
        float HeightPositionhPercentage = Mathf.Clamp((((Input.mousePosition.y - Screen.height / 2) / Screen.height) * 2), -1, 1);

        float HorizontalInput = 0f;
        float VerticalInput = 0f;
        

        //Mouse Position to Input
        if (Mathf.Abs(WidthPositionPercentage) >= 1 - HorizontalThreshold)
        {
            float PositionInBorder = Mathf.InverseLerp(1 - HorizontalThreshold, 1, Mathf.Abs(WidthPositionPercentage));
            HorizontalInput = (SensivityProgresion.Evaluate(PositionInBorder) * Mathf.Sign(WidthPositionPercentage)) * Sensivity * Time.deltaTime * 10;
        }


        if (Mathf.Abs(HeightPositionhPercentage) >= 1 - VerticalThreshold)
        {
            float PositionInBorder = Mathf.InverseLerp(1 - VerticalThreshold, 1, Mathf.Abs(HeightPositionhPercentage));
            VerticalInput = (SensivityProgresion.Evaluate(PositionInBorder) * Mathf.Sign(HeightPositionhPercentage)) * Sensivity * Time.deltaTime * 10;
        }


        return new Vector2(VerticalInput, HorizontalInput);
    }
}
