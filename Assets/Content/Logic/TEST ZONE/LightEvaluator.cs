using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class LightEvaluator : MonoBehaviour
{

    [Header("    VALUE DEBUGING")]

    [SerializeField]
    private float rawLightIntensity;

    [SerializeField]
    private float normalizedLightIntensity;

    [SerializeField]
    private float AdaptedLightIntensity;



    [Space(20)]



    [Header("    NORMALIZING SETTINGS")]

    [SerializeField]
    private float maxValue;

    [SerializeField]
    private float minValue;

    [SerializeField]
    private AnimationCurve modificationCurve;


    [Space (20)]



    [Header("    LIGHT INTENSITY SETTINGS")]

    [SerializeField]
    private float bakedLightAdjustment = 1f;

    [SerializeField]
    private float pointLightAdjustment = 0.5f;

    [SerializeField]
    private float spotLightAdjustment = 0.5f;


        [Space(20)]


    [Header("    COLLISION SETTINGS")]


    [SerializeField]
    private float refreshRate = 0.2f;

    public GameObject[] objectToCheck;

    [Space(7)]

    [SerializeField]
    private float sphereRadius = 10f; // Rayon de la sphère de collision

    [SerializeField]
    private LayerMask NonObstructingLayers;

    [SerializeField]
    private LayerMask DynamicLightLayer;







    private Collider[] colliders;
    private List<Collider> nonObstructedColliders = new List<Collider>();








    private void Start()
    {
        StartCoroutine(CheckLightLevelLoop());
    }



    IEnumerator CheckLightLevelLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(refreshRate);
            GetNearDynamicLights();


            // Faire en sorte de verifier pour chacunes des Targets

            //foreach (GameObject item in objectToCheck)
            //{
            //    GetNearDynamicLights();
            //}

        }
    }







    /// <summary>
    /// Get all dynamic light in Range. The first step in the Light Intensity Evalutaion.
    /// </summary>
    void GetNearDynamicLights()
    {
        //Reset the value to make the Light intensity additives
        rawLightIntensity = 0;
        NormalizeLightIntensity();

        //Reset the Non Obstructerd Light List
        nonObstructedColliders.Clear();




        //Fill the Lights List with Lights within a radius
        colliders = Physics.OverlapSphere(transform.position, sphereRadius, DynamicLightLayer);

        //Fill the Non Obstructerd Light List with Lights in the Light
        RaycastHit hit;
        foreach (Collider collider in colliders)
        {
            //Check Obstruction
            if (!Physics.Raycast(transform.position, (collider.transform.position - transform.position).normalized, out hit, Vector3.Distance(collider.transform.position, transform.position), ~NonObstructingLayers))
            {
                Debug.Log("Non Obstructed");
                nonObstructedColliders.Add(collider);

            }
            else
            {
                Debug.Log("Obstrué");
                continue;
            }

        }


        //Get Bked Light Information if no Lights
        //if (nonObstructedColliders.Count == 0)
        //{
        //    GetBakedLightIntenity();
        //}



        //Get the Light Informations from Dynamics Lights
        foreach (Collider collider in nonObstructedColliders)
        {
            //Check Validity
            Light lightComponent = collider.GetComponent<Light>();

            if (lightComponent == null || !lightComponent.isActiveAndEnabled)
            {
                continue;
            }


            //Get the Light Informations from the correct Light Type
            switch (lightComponent.type)
            {
                case LightType.Point:
                    AddPointLightIntensity(lightComponent);
                    break;

                case LightType.Spot:
                    AddSpotLightIntensity(lightComponent);
                    break;
            }
        }

        GetBakedLightIntenity();


    }





    /// <summary>
    /// Set the Light Intensity with Baked Informations;
    /// </summary>
    private void GetBakedLightIntenity()
    {
        
        // Renderer associé à l'objet auquel le LightProbe est attaché
        Renderer renderer = GetComponent<Renderer>();

        // Vérifiez si le rendu est présent sur l'objet
        if (renderer != null)
        {
            SphericalHarmonicsL2 harmonics = new SphericalHarmonicsL2();
            LightProbes.GetInterpolatedProbe(gameObject.transform.position, renderer, out harmonics);
            rawLightIntensity += (0.2989f * harmonics[0, 0]) + (0.5870f * harmonics[1, 0]) + (0.1140f * harmonics[2, 0]) * bakedLightAdjustment;
            NormalizeLightIntensity();
        }
        else
        {
            Debug.LogError("Renderer non trouvé sur cet objet. Assurez-vous qu'un composant Renderer est attaché à l'objet.");
        }
    }




    /// <summary>
    /// Set the Light Intensity with Point Light Informations;
    /// </summary>
    public void AddPointLightIntensity(Light pointLight)
    {

        //Check Range
        if (Vector3.Distance(transform.position, pointLight.transform.position) > pointLight.range)
        {
            Debug.Log("PointLight too Far");
            GetBakedLightIntenity();
            return;
        }

        rawLightIntensity += Mathf.Lerp(pointLight.intensity * pointLightAdjustment, 0, Vector3.Distance(pointLight.transform.position, transform.position) / pointLight.range);
        NormalizeLightIntensity();
    }



    /// <summary>
    /// Set the Light Intensity with Spot Light Informations;
    /// </summary>
    public void AddSpotLightIntensity(Light spotLight)
    {
        Debug.Log("start");

        //Check Range
        if (Vector3.Distance(spotLight.transform.position, transform.position) > spotLight.range)
        {
            
            Debug.Log("SpotLight too Far");
            GetBakedLightIntenity();
            return;
        }

        float angleToLight = Vector3.Angle(spotLight.transform.forward, (transform.position - spotLight.transform.position).normalized) * 2;


        //Check Angle
        if (angleToLight > spotLight.spotAngle)
        {
            Debug.Log("Out of SpotLight Radius");
            GetBakedLightIntenity();
            return;
        }

        rawLightIntensity += spotLight.intensity * spotLightAdjustment;


        // ----- ANCIENNCE VERSION "PLUS" PRECISE (c'est faux) -----
        //float innerAngle = (spotLight.GetComponent<HDAdditionalLightData>().innerSpotPercent / 100 * spotLight.spotAngle) +0.1f;        // Le +.01f ser a éviter une division de 0 selon le réglage du SpotLight
        //rawLightIntensity += Mathf.Lerp(0, spotLight.intensity * spotLightAdjustment, Mathf.Abs(innerAngle / angleToLight));
        //NormalizeLightIntensity();      
    }





    /// <summary>
    /// Normalize the Raw Intensity Value
    /// </summary>
    private void NormalizeLightIntensity()
    {
        normalizedLightIntensity = Mathf.Clamp01(rawLightIntensity / maxValue - minValue);
        AdaptedLightIntensity = modificationCurve.Evaluate(normalizedLightIntensity);
    }






    void OnDrawGizmosSelected()
    {
        // Afficher la sphère de collision dans l'éditeur Unity
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}