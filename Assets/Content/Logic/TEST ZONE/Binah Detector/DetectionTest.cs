using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.GraphicsBuffer;

public class DetectionTest : MonoBehaviour
{

    [SerializeField]
    private float DetectionRate;

    [SerializeField]
    private float DetectionPerSecond;



    [Space (40)]



    [Header ("   VIEW SETTINGS")]


    [SerializeField]
    private bool showViewAngle;

    [SerializeField]
    private float ViewVisualizationLength;

    [SerializeField]
    private float horizontalViewAngle, verticalViewAngle;


    [SerializeField]
    private LayerMask layerToIgnore;








    [SerializeField]
    private GameObject[] sightTarget;


    public Dictionary<GameObject, float> Targets = new Dictionary<GameObject, float>();












    // Contient du code temporaire
    void Update()
    {
        LockVerticalRotation();

        maxTargetPoints = 0;

        //TEST TEMPORAIRE
        //TEST TEMPORAIRE
        //TEST TEMPORAIRE
        Targets = GameManager.Instance.Binah.GetComponent<InfiltrationManager>().Targets;
        foreach (KeyValuePair<GameObject, float> target in Targets)
        {
            maxTargetPoints += target.Value;
        }
        //TEST TEMPORAIRE
        //TEST TEMPORAIRE
        //TEST TEMPORAIRE

    }




    //[SerializeField]
    //private float timeBetweenUpdates;



    private void LockVerticalRotation()
    {
        // Récupérer la rotation actuelle de l'objet
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Définir la nouvelle rotation (x=0, y=rotation actuelle sur y, z=0)
        Quaternion newRotation = Quaternion.Euler(0f, currentRotation.y, 0f);

        // Appliquer la nouvelle rotation à l'objet
        transform.rotation = newRotation;
    }




    private float maxTargetPoints = 0;

    private void Start()
    {
        Targets = GameManager.Instance.Binah.GetComponent<InfiltrationManager>().Targets;


        foreach (KeyValuePair<GameObject, float> target in Targets)
        {
            maxTargetPoints += target.Value;
        }

        //StartCoroutine(CheckPosition());
    }




    [Space (20)]

    [SerializeField]
    float multiplicateur;



    public void CheckPosition()
    {

        DetectionRate = CheckVision() * EnemyManager.Instance.UpdateTime / maxTargetPoints * multiplicateur;
        DetectionPerSecond = DetectionRate / EnemyManager.Instance.UpdateTime;        
    }









    private float CheckVision()
    {
        float rawVisionScore = 0;


        foreach (KeyValuePair<GameObject, float> target in Targets)
        {

            Vector3 targetPosition = target.Key.transform.position;
            float targetDistance = Vector3.Distance(targetPosition, transform.position);



            //Get vector ready to get their angle 
            Vector3 flattenedHorizontalPosition = Vector3.ProjectOnPlane(targetPosition, transform.up) + new Vector3(0, transform.position.y, 0);
            Vector3 flattenedVerticalPosition = transform.forward * Vector3.Distance(transform.position, flattenedHorizontalPosition) + transform.position + new Vector3(0, targetPosition.y - flattenedHorizontalPosition.y, 0);


            //Angle 
            float horizontalAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, (flattenedHorizontalPosition - transform.position).normalized, transform.right));
            float verticalAngle = Mathf.Abs(Vector3.SignedAngle(transform.forward, (flattenedVerticalPosition - transform.position).normalized, transform.up));



            //Check Angle
            if (horizontalViewAngle < horizontalAngle || verticalViewAngle < verticalAngle)
            {
                //Target Out of Viewing Angle
                Debug.DrawLine(transform.position, targetPosition, Color.grey, EnemyManager.Instance.UpdateTime);
                continue;
            }


            RaycastHit hit;

            //Check Visibility
            if (Physics.Raycast(transform.position, targetPosition - transform.position, out hit, targetDistance, ~layerToIgnore))
            {
                //Target Not Visible
                Debug.DrawLine(transform.position, hit.point, Color.white, EnemyManager.Instance.UpdateTime);
            }
            else
            {
                //Target Visible
                Debug.DrawLine(transform.position, targetPosition, Color.red, EnemyManager.Instance.UpdateTime);


                float targetLightLevel = GameManager.Instance.Binah.GetComponent<LightEvaluator>().GetNearDynamicLights(target.Key);
                rawVisionScore += target.Value * targetLightLevel;


                //Check the Luminosity

            }

        }

        return rawVisionScore;
    }

































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


}
