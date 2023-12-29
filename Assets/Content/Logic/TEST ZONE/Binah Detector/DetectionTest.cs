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

        //Lignes Horizontales
        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0, horizontalViewAngle, 0) * transform.forward * ViewVisualizationLength, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + Quaternion.Euler(0, -horizontalViewAngle, 0) * transform.forward * ViewVisualizationLength, Color.yellow);


        //Ligne Verticales 

        // Transformer les directions locales en directions mondiales
        Vector3 lineDirectionUp = transform.TransformDirection(Quaternion.Euler(-verticalViewAngle, 0, 0) * Vector3.forward);
        Vector3 lineDirectionDown = transform.TransformDirection(Quaternion.Euler(verticalViewAngle, 0, 0) * Vector3.forward);

        // Dessiner les lignes
        Debug.DrawLine(transform.position, transform.position + lineDirectionUp * 10f, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + lineDirectionDown * 10f, Color.yellow);
    }




    public float horizontalAngle;
    public float verticalAngle;




    void Update()
    {
        // Récupérer la rotation actuelle de l'objet
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Définir la nouvelle rotation (x=0, y=rotation actuelle sur y, z=0)
        Quaternion newRotation = Quaternion.Euler(0f, currentRotation.y, 0f);

        // Appliquer la nouvelle rotation à l'objet
        transform.rotation = newRotation;
    }


    private void FixedUpdate()
    {
        foreach (GameObject target in sightTarget)
        {
            Vector3 targetPosition = target.transform.position;
            float targetDistance = Vector3.Distance(targetPosition, transform.position);



            //Get vector ready to get their angle 
            Vector3 flattenedHorizontalPosition = Vector3.ProjectOnPlane(targetPosition, transform.up) + new Vector3 (0, transform.position.y, 0);
            Vector3 flattenedVerticalPosition = transform.forward * Vector3.Distance(transform.position, flattenedHorizontalPosition) + transform.position + new Vector3(0, targetPosition.y - flattenedHorizontalPosition.y, 0);


            //Angle 
            horizontalAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, (flattenedHorizontalPosition - transform.position).normalized, transform.right));
            verticalAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, (flattenedVerticalPosition - transform.position).normalized, transform.up));




            if (horizontalViewAngle < horizontalAngle || verticalViewAngle < verticalAngle)
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
