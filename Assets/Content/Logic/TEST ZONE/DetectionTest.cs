using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectionTest : MonoBehaviour
{
    [SerializeField]
    private bool showViewAngle;

    [SerializeField]
    private float ViewVisualizationLength;


    [SerializeField]
    private float horizontalViewAngle, verticalViewAngle;


    [SerializeField]
    private GameObject[] sightTarget;

    [SerializeField]
    private LayerMask layerToIgnore;



    private void OnDrawGizmosSelected()
    {
        if (!showViewAngle)
        {
            return;
        }


        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0, horizontalViewAngle, 0) * transform.forward * ViewVisualizationLength, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -horizontalViewAngle, 0) * transform.forward * ViewVisualizationLength, Color.yellow);

        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0 , 0, verticalViewAngle) * transform.forward * ViewVisualizationLength, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0 , 0, -verticalViewAngle) * transform.forward * ViewVisualizationLength, Color.yellow);
    }




    private void FixedUpdate()
    {
        foreach (GameObject target in sightTarget)
        {
            Vector3 targetPosition = target.transform.position;
            float targetDistance = Vector3.Distance(targetPosition, transform.position);


            //Angle 
            float horizontalAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, (targetPosition - transform.position).normalized, transform.up));
            float verticalAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, (targetPosition - transform.position).normalized, transform.right));


            if (horizontalViewAngle < horizontalAngle && verticalViewAngle < verticalAngle)
            {
                Debug.DrawLine(transform.position, targetPosition, Color.grey);
                continue;
            }

            RaycastHit hit;
            if (!Physics.Raycast(transform.position, targetPosition - transform.position, out hit, targetDistance, ~layerToIgnore))
            {
                IncreaseDetection();


                Debug.DrawLine(transform.position, targetPosition, Color.red);
            }
            else
            {
                Debug.DrawLine(transform.position, hit.point, Color.white);
            }

        }
    }


    private void IncreaseDetection()
    {

    }
}
