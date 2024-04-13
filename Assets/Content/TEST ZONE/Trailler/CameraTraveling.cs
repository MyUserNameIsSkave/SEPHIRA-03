using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTraveling : MonoBehaviour
{
    [SerializeField]
    private Transform startPoint, endPoint, cameraPoint;

    [SerializeField]
    private AnimationCurve movementCurve;

    [SerializeField]
    private float movementSpeed;


    private void Start()
    {
        StartCoroutine(Traveling());
    }


    IEnumerator Traveling()
    {

        while (true)
        {
            cameraPoint.position = Vector3.Lerp(startPoint.position, endPoint.position, movementCurve.Evaluate(Time.time));
            print(Time.time);
            yield return new WaitForNextFrameUnit();
        }
    }
}
