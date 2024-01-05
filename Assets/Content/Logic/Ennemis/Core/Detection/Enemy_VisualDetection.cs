using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_VisualDetection : Enemy_BaseDetection
{
    #region Variables


    [Header("   VALUES")]
    [Space(15)]



    //[SerializeField, Tooltip("Variable used for debugging purpose.")]
    private float detectionRate;

    [SerializeField, Tooltip("Variable used for debugging purpose. The DetectionRate adapted to the second.")]
    private float detectionPerSecond;





    [Space(25)]





    [Header("   VIEW SETTINGS")]
    [Space(15)]


    [Tooltip("The view distance of the enemy")]
    public float MaxViewDistance;

    [SerializeField, Tooltip("The opening of the enemy vision")]
    private float horizontalViewAngle, verticalViewAngle;



    [Space(15)]



    [SerializeField, Tooltip("NE PAS TOUCHER ! Voir avec Enzo.")]
    private LayerMask layerToIgnore;



    [Space(15)]



    [SerializeField, Tooltip("Debugging tool that show the view angle and the view distance of the enemy")]
    private bool showViewAngle;

    [SerializeField, Tooltip("Debugging tool that show the line from the enemy to Binah target points.")]
    private bool showViewLines;




    [Space(25)]





    [Header("  SENSIVITY SETTINGS")]
    [Space(15)]



    [SerializeField, Range(0.1f, 10f), Tooltip("A Multiplicator applied After the Light and Distance logic.")]
    float generalSensitivity = 1f;

    [SerializeField, Range(0.1f, 5f)]
    private float maxDetectionRatePerSecond;



    [Space(15)]



    [SerializeField, Range(0f, 1f), Tooltip("The Lower the Value the Lower the Impact of Distance")]
    float distanceSensivity = 1f;

    [SerializeField, Tooltip("the curve must go from high to low, 0 is when the distance is the lowest and 1 is when distance is the highest.")]
    AnimationCurve distanceCurve;



    [Space(15)]



    [SerializeField, Range(0f, 1f), Tooltip("The Lower the Value the Lower the Impact of Light")]
    float lightSensivity = 1f;





    public Dictionary<GameObject, float> targets = new Dictionary<GameObject, float>();
    private float maxTargetPoints = 0;

    #endregion



    [SerializeField, Tooltip ("A GameObject attached to the head bone.")]
    private GameObject head;

    private Enemy_BaseManager enemyManager;

    private void Awake()
    {
        enemyManager = GetComponent<Enemy_BaseManager>();
    }




    #region 

    // Contient du code temporaire
    void Update()
    {
        LockVerticalRotation();
    }



    private void Start()
    {
        targets = GameManager.Instance.Binah.GetComponent<InfiltrationManager>().Targets;


        foreach (KeyValuePair<GameObject, float> target in targets)
        {
            maxTargetPoints += target.Value;
        }
    }


    private void LockVerticalRotation()
    {
        // Récupérer la rotation actuelle de l'objet
        Vector3 currentRotation = transform.rotation.eulerAngles;

        // Définir la nouvelle rotation (x=0, y=rotation actuelle sur y, z=0)
        Quaternion newRotation = Quaternion.Euler(0f, currentRotation.y, 0f);

        // Appliquer la nouvelle rotation à l'objet
        transform.rotation = newRotation;
    }

    #endregion





    public void GetVisionScore()
    {
        // Je divise puis mutlipli ? Pourquoi
        detectionRate = Mathf.Clamp(CheckVision() * EnemyManager.Instance.UpdateTime / maxTargetPoints * generalSensitivity, 0, maxDetectionRatePerSecond * EnemyManager.Instance.UpdateTime);
        detectionPerSecond = detectionRate / EnemyManager.Instance.UpdateTime;

        enemyManager.SeeingSomething(detectionRate);
    }






    private float CheckVision()
    {
        float rawVisionScore = 0;


        foreach (KeyValuePair<GameObject, float> target in targets)
        {
            Vector3 targetPosition = target.Key.transform.position;
            float targetDistance = Vector3.Distance(targetPosition, head.transform.position);



            //Get vector ready to get their angle 
            Vector3 flattenedHorizontalPosition = Vector3.ProjectOnPlane(targetPosition, head.transform.up) + new Vector3(0, head.transform.position.y, 0);
            Vector3 flattenedVerticalPosition = head.transform.forward * Vector3.Distance(head.transform.position, flattenedHorizontalPosition) + head.transform.position + new Vector3(0, targetPosition.y - flattenedHorizontalPosition.y, 0);


            //Angle 
            float horizontalAngle = Mathf.Abs(Vector3.SignedAngle(head.transform.forward, (flattenedHorizontalPosition - head.transform.position).normalized, head.transform.right));
            float verticalAngle = Mathf.Abs(Vector3.SignedAngle(head.transform.forward, (flattenedVerticalPosition - head.transform.position).normalized, head.transform.up));



            //Check Angle
            if (horizontalViewAngle < horizontalAngle || verticalViewAngle < verticalAngle)
            {
                //Target Out of Viewing Angle
                if (showViewLines)
                {
                    Debug.DrawLine(head.transform.position, targetPosition, Color.grey, EnemyManager.Instance.UpdateTime);
                }

                continue;
            }


            RaycastHit hit;

            //Check Visibility
            if (Physics.Raycast(head.transform.position, targetPosition - head.transform.position, out hit, targetDistance, ~layerToIgnore))
            {
                //Target Not Visible
                if (showViewLines)
                {
                    Debug.DrawLine(head.transform.position, hit.point, Color.white, EnemyManager.Instance.UpdateTime);
                }
            }
            else
            {
                //Target Visible
                if (showViewLines)
                {
                    Debug.DrawLine(head.transform.position, targetPosition, Color.red, EnemyManager.Instance.UpdateTime);
                }


                //Normalized Light value
                float targetLightLevel = GameManager.Instance.Binah.GetComponent<LightEvaluator>().GetNearDynamicLights(target.Key);


                // Modifiers 
                float distanceModifier = distanceCurve.Evaluate(Mathf.Lerp(0, 1, Vector3.Distance(head.transform.position, targetPosition) / MaxViewDistance));
                float adaptedLightMultiplier = Mathf.Clamp01(targetLightLevel / lightSensivity);
                float adaptedDistanceMultiplier = Mathf.Clamp01(distanceModifier / distanceSensivity);



                //Apply the Modifiers to Detection Score 
                #region Detection Score Maths
                if (distanceSensivity == 0 || lightSensivity == 0)
                {
                    if (distanceSensivity == 0 && lightSensivity == 0)
                    {
                        rawVisionScore += target.Value;
                        continue;
                    }

                    if (distanceSensivity == 0 && lightSensivity != 0)
                    {
                        rawVisionScore += target.Value * adaptedLightMultiplier;
                        continue;
                    }

                    if (lightSensivity == 0 && distanceSensivity != 0)
                    {
                        rawVisionScore += target.Value * adaptedDistanceMultiplier;
                        continue;
                    }
                }
                else
                {
                    rawVisionScore += target.Value * adaptedLightMultiplier * adaptedDistanceMultiplier;
                }
                #endregion
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
        Debug.DrawLine(head.transform.position, head.transform.position + Quaternion.Euler(0, horizontalViewAngle, 0) * head.transform.forward * MaxViewDistance, Color.yellow);
        Debug.DrawLine(head.transform.position, head.transform.position + Quaternion.Euler(0, -horizontalViewAngle, 0) * head.transform.forward * MaxViewDistance, Color.yellow);


        //Ligne Verticales 

        // Transformer les directions locales en directions mondiales
        Vector3 lineDirectionUp = transform.TransformDirection(Quaternion.Euler(-verticalViewAngle, 0, 0) * Vector3.forward);
        Vector3 lineDirectionDown = transform.TransformDirection(Quaternion.Euler(verticalViewAngle, 0, 0) * Vector3.forward);

        // Dessiner les lignes
        Debug.DrawLine(head.transform.position, head.transform.position + lineDirectionUp * MaxViewDistance, Color.yellow);
        Debug.DrawLine(head.transform.position, head.transform.position + lineDirectionDown * MaxViewDistance, Color.yellow);
    }
}
