using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using static UnityEngine.GraphicsBuffer;

public class LightEvaluator : MonoBehaviour
{

    [Header("    VALUE DEBUGING")]

    [SerializeField, Tooltip("Variable used for debugging purpose.")]
    private float rawLightIntensity;

    [SerializeField, Tooltip("Variable used for debugging purpose.")]
    private float normalizedLightIntensity;

    [SerializeField, Tooltip("Variable used for debugging purpose.")]
    private float adaptedLightIntensity;

    [Space(10)]

    [SerializeField, Tooltip ("When true, always update the logic to get an evalutation of the light level even when out of enemy sight.")]
    private bool alwaysUpdate;




    [Space(20)]



    [Header("    NORMALIZING SETTINGS")]

    [SerializeField, Tooltip("NE PAS TOUCHER ! Voir avec Enzo.")]
    private float maxValue;

    [SerializeField, Tooltip("NE PAS TOUCHER ! Voir avec Enzo.")]
    private float minValue;

    [SerializeField, Tooltip("the curve must go from high to low, 0 is when the distance is the lowest and 1 is when distance is the highest.")]
    AnimationCurve lightCurve;


    [Space (20)]



    [Header("    LIGHT INTENSITY SETTINGS")]

    [SerializeField, Tooltip("A multiplicator to adjust score given to a specific light source.")]
    private float bakedLightAdjustment = 1f;

    [SerializeField, Tooltip("A multiplicator to adjust score given to a specific light source.")]
    private float pointLightAdjustment = 0.5f;

    [SerializeField, Tooltip("A multiplicator to adjust score given to a specific light source.")]
    private float spotLightAdjustment = 0.5f;


        [Space(20)]


    [Header("    COLLISION SETTINGS")]


    [SerializeField, Tooltip("The radius in wich the dynamic light can affect the light intensity evalutation.")]
    private float sphereRadius = 10f; // Rayon de la sphère de collision

    [SerializeField, Tooltip ("NE PAS TOUCHER ! Voir avec Enzo.")]
    private LayerMask nonObstructingLayers;

    [SerializeField, Tooltip("NE PAS TOUCHER ! Voir avec Enzo.")]
    private LayerMask dynamicLightLayer;




    private GameObject[] objectToCheck;
    private Collider[] colliders;
    private List<Collider> nonObstructedColliders = new List<Collider>();





    #region DEBUG
    private void Start()
    {
        StartCoroutine(DebuggingUpdate());
    }



    IEnumerator DebuggingUpdate()
    {
        while (true)
        {
            if (alwaysUpdate)
            {
                GetNearDynamicLights(transform.GetChild(0).gameObject); //Récupération d'un enfant pour avoir un GameObject avec un Rederer, a changer quand on implementera le model de Binah.
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
    #endregion




    /// <summary>
    /// Get all dynamic light in Range. The first step in the Light Intensity Evalutaion.
    /// </summary>
    public float GetNearDynamicLights(GameObject target)
    {
        //Reset the value to make the Light intensity additives
        rawLightIntensity = 0;
        //NormalizeLightIntensity(target);

        //Reset the Non Obstructerd Light List
        nonObstructedColliders.Clear();




        //Fill the Lights List with Lights within a radius
        colliders = Physics.OverlapSphere(target.transform.position, sphereRadius, dynamicLightLayer);

        //Fill the Non Obstructerd Light List with Lights in the Light
        RaycastHit hit;
        foreach (Collider collider in colliders)
        {
            //Check Obstruction
            if (!Physics.Raycast(target.transform.position, (collider.transform.position - target.transform.position).normalized, out hit, Vector3.Distance(collider.transform.position, target.transform.position), ~nonObstructingLayers))
            {
                //Debug.Log("Non Obstructed");
                nonObstructedColliders.Add(collider);

            }
            else
            {
                //Debug.Log("Obstructed");
                continue;
            }

        }



        //Get the Light Informations from Dynamics Lights
        foreach (Collider collider in nonObstructedColliders)
        {
            //Check Validity
            Light lightComponent = collider.GetComponent<Light>();

            if (lightComponent == null || !lightComponent.isActiveAndEnabled)
            {
                Debug.Log("Light Component Disabled");
                continue;
            }


            //Get the Light Informations from the correct Light Type
            switch (lightComponent.type)
            {
                case LightType.Point:
                    AddPointLightIntensity(lightComponent, target);
                    break;

                case LightType.Spot:
                    AddSpotLightIntensity(lightComponent, target);
                    break;
            }
        }


        GetBakedLightIntenity(target);
        NormalizeLightIntensity();


        return adaptedLightIntensity;
    }





    /// <summary>
    /// Set the Light Intensity with Baked Informations;
    /// </summary>
    private void GetBakedLightIntenity(GameObject target)
    {
        // Renderer associé à l'objet auquel le LightProbe est attaché
        Renderer renderer = target.GetComponent<Renderer>();

        // Vérifiez si le rendu est présent sur l'objet
        if (renderer != null)
        {
            SphericalHarmonicsL2 harmonics = new SphericalHarmonicsL2();
            LightProbes.GetInterpolatedProbe(target.transform.position, renderer, out harmonics);
            rawLightIntensity += (0.2989f * harmonics[0, 0]) + (0.5870f * harmonics[1, 0]) + (0.1140f * harmonics[2, 0]) * bakedLightAdjustment;
        }
        else
        {
            Debug.LogError("Renderer non trouvé sur cet objet. Assurez-vous qu'un composant Renderer est attaché à l'objet.");
        }
    }




    /// <summary>
    /// Set the Light Intensity with Point Light Informations;
    /// </summary>
    public void AddPointLightIntensity(Light pointLight, GameObject target)
    {

        //Check Range
        if (Vector3.Distance(transform.position, pointLight.transform.position) > pointLight.range)
        {
            //Debug.Log("PointLight too Far");
            GetBakedLightIntenity(target);
            return;
        }

        rawLightIntensity += Mathf.Lerp(pointLight.intensity * pointLightAdjustment, 0, Vector3.Distance(pointLight.transform.position, target.transform.position) / pointLight.range);
    }



    /// <summary>
    /// Set the Light Intensity with Spot Light Informations;
    /// </summary>
    public void AddSpotLightIntensity(Light spotLight, GameObject target)
    {
        //Check Range
        if (Vector3.Distance(spotLight.transform.position, target.transform.position) > spotLight.range)
        {
            
            //Debug.Log("SpotLight too Far");
            GetBakedLightIntenity(target);
            return;
        }

        float angleToLight = Vector3.Angle(spotLight.transform.forward, (target.transform.position - spotLight.transform.position).normalized) * 2;
        

        //Check Angle
        if (angleToLight > spotLight.spotAngle)
        {
            //Debug.Log("Out of SpotLight Radius");
            GetBakedLightIntenity(target);
            return;
        }

        rawLightIntensity += spotLight.intensity * spotLightAdjustment;
    }





    /// <summary>
    /// Normalize the Raw Intensity Value
    /// </summary>
    private void NormalizeLightIntensity()
    {
        normalizedLightIntensity = Mathf.Clamp01(rawLightIntensity / maxValue - minValue);
        adaptedLightIntensity = lightCurve.Evaluate(normalizedLightIntensity);
    }






    void OnDrawGizmosSelected()
    {
        // Afficher la sphère de collision dans l'éditeur Unity
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}