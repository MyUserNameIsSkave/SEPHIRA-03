using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionTest : MonoBehaviour
{
    [SerializeField]
    private float horizontalAngle, verticalAngle, distance;

    [SerializeField]
    private int horizontalPrecision, verticalPrecision;

    private float _horizontalPrecision, _verticalPrecision;

    private List<float> horizontalRotationOffsets = new List<float>();
    private List<float> verticalRotationOffsets = new List<float>();

    private void OnValidate()
    {
        _horizontalPrecision = horizontalPrecision + 0.1f;

        float currentHorizontalAngle = horizontalAngle / _horizontalPrecision;

        horizontalRotationOffsets.Clear();    

        for (int i = 0; i < _horizontalPrecision; i++)
        {
            horizontalRotationOffsets.Add(horizontalAngle / _horizontalPrecision * i);
        }




        _verticalPrecision = verticalPrecision + 0.1f;

        float currentVerticalAngle = horizontalAngle / _verticalPrecision;

        verticalRotationOffsets.Clear();

        for (int i = 0; i < _verticalPrecision; i++)
        {
            verticalRotationOffsets.Add(verticalAngle / _verticalPrecision * i);
        }


    }



    private void FixedUpdate()
    {
        DoTraces();

    }


    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        {
            return;
        }

        DoTraces(); 
    }




    private void DoTraces()
    {
        foreach (float horizontalRotationOffset in horizontalRotationOffsets)
        {
            //Center     
            Vector3 horizontalEndPosition = transform.position + Quaternion.AngleAxis(-horizontalAngle / 2 + horizontalRotationOffset, Vector3.up) * transform.forward * distance;
            Debug.DrawLine(transform.position, horizontalEndPosition, Color.red);


            foreach (float verticalRotationOffset in verticalRotationOffsets)
            {
                //Center
                Vector3 direction = (horizontalEndPosition - transform.position).normalized;
                Vector3 verticalEndPosition = transform.position + Quaternion.AngleAxis(-verticalAngle / 2 + verticalRotationOffset, Vector3.forward) * direction * distance;
                Debug.DrawLine(transform.position, verticalEndPosition, Color.red);
            }
        }
    }


}
