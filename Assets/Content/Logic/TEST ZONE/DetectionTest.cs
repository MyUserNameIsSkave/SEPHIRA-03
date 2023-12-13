using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetectionTest : MonoBehaviour
{
    [SerializeField]
    private bool showViewAngle;



    [SerializeField]
    private float sightOpening;


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

        float angleStep = sightOpening / 90;

        for (int i = 0; i <= 90; i++)
        {
            float angle = transform.eulerAngles.y - sightOpening / 2 + angleStep * i;
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            Debug.DrawLine(transform.position, transform.position + direction * 10f, Color.blue);
        }
    }




    private void FixedUpdate()
    {
        foreach (GameObject target in sightTarget)
        {
            Vector3 targetPosition = target.transform.position;
            float targetDistance = Vector3.Distance(targetPosition, transform.position);

            if (sightOpening < Vector3.Angle(transform.forward, targetPosition - transform.position))
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
