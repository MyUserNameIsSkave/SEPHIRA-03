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

    public AnimationCurve modificationCurve;


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


    //[SerializeField]
    //private float refreshRate = 0.2f;

    [SerializeField]
    private GameObject[] objectToCheck;

    [Space(7)]

    [SerializeField]
    private float sphereRadius = 10f; // Rayon de la sphère de collision

    [SerializeField]
    private LayerMask NonObstructingLayers;

    [SerializeField]
    private LayerMask DynamicLightLayer;







    private Collider[] colliders;
    private List<Collider> nonObstructedColliders = new List<Collider>();








    //private void Start()
    //{
    //    StartCoroutine(CheckLightLevelLoop());
    //}



    //IEnumerator CheckLightLevelLoop()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(refreshRate);
    //        GetNearDynamicLights();


    //        // Faire en sorte de verifier pour chacunes des Targets

    //        //foreach (GameObject item in objectToCheck)
    //        //{
    //        //    GetNearDynamicLights();
    //        //}

    //    }
    //}







    /// <summary>
    /// Get all dynamic light in Range. The first step in the Light Intensity Evalutaion.
    /// </summary>
    public float GetNearDynamicLights(GameObject target)
    {
        //Reset the value to make the Light intensity additives
        rawLightIntensity = 0;
        NormalizeLightIntensity();

        //Reset the Non Obstructerd Light List
        nonObstructedColliders.Clear();




        //Fill the Lights List with Lights within a radius
        colliders = Physics.OverlapSphere(target.transform.position, sphereRadius, DynamicLightLayer);

        //Fill the Non Obstructerd Light List with Lights in the Light
        RaycastHit hit;
        foreach (Collider collider in colliders)
        {
            //Check Obstruction
            if (!Physics.Raycast(transform.position, (collider.transform.position - target.transform.position).normalized, out hit, Vector3.Distance(collider.transform.position, target.transform.position), ~NonObstructingLayers))
            {
                //Debug.Log("Non Obstructed");
                nonObstructedColliders.Add(collider);

            }
            else
            {
                //Debug.Log("Obstrué");
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

        return AdaptedLightIntensity;
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
        NormalizeLightIntensity();
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
            Debug.Log("Out of SpotLight Radius");
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
        AdaptedLightIntensity = modificationCurve.Evaluate(normalizedLightIntensity);         
    }






    void OnDrawGizmosSelected()
    {
        // Afficher la sphère de collision dans l'éditeur Unity
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}