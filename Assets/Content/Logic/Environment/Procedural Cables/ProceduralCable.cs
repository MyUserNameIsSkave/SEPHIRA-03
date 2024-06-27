using System;
using System.Collections;
using UnityEngine;


[Serializable] 
public class CableSection
{
    public Transform start, end;

        [Space (7)]

    public float sag = 1;
    public float swayFrequency = 1;
    public float swayMultiplier = 1;

        [Space(7)]

    public float swayXMultiplier = 1;
    public float swayYMultiplier = 1f;

    [HideInInspector]
    public float swayValue;
}













[RequireComponent(typeof(LineRenderer))]
//[ExecuteAlways]
public class ProceduralCable : MonoBehaviour
{
    #region VARIABLES


    // ----- SETTINGS -----

    [Header("     GENERAL SETTINGS")]
    [Space(7)]

    [SerializeField, Tooltip("Should the cable move ?")] 
    bool StaticSettings = true;

    [SerializeField, Range (20, 500f), Tooltip("The distance at wich the cable become static")] 
    float MakeStaticDistance;

    [Space(7)]

    [SerializeField] float pointDensity = 3;
    [SerializeField] float generalSagMultiplier = 1;
    [SerializeField] float generalSwayFrequency = 0.5f;
    [SerializeField] float generalSwayMultiplier = 1f;

        [Space (7)]

    [SerializeField] private bool useCustomeFramerate = false;
    [SerializeField, Range(1, 60)] private float framerate = 24;

        [Space(10)]
        [Header("     POINTS")]

    [SerializeField] CableSection[] cableSections;


    [Space(15)]
    [Header ("     ----- DEBUG -----")]
    [Space(7)]

    [SerializeField]
    private bool showRange;
    [SerializeField, Tooltip("Shoul nonly be changed in the Prefab")] private Material baseMaterial;








    // ----- WORKING -----

    // References
    LineRenderer line;


    // Calculation
    private float frameTime;
    Vector3 sagDirection;
    int pointsInLineRenderer;
    Vector3 vectorFromStartToEnd;
    float swayMultiplier = 0.2f;





    Camera _camera;
    bool inRange = true;
    Vector3 position;
    int iterations = 0;

    #endregion








    private void OnValidate()
    {
        iterations = 0;
        _camera = Camera.main;
        position = transform.position;
        line = GetComponent<LineRenderer>();
        line.material = baseMaterial;
    }



    //On Enable de base !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    private void Start()
    {
        //StartCoroutine(Loop());
        //StartCoroutine(ShowRange());
    }













    // UPDATE WHEN NOT PLAYING


    IEnumerator ShowRange()
    {
        if (!showRange || !Application.isPlaying)
        {
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(ShowRange());

            yield break;
        }

        if (Vector3.Distance(_camera.transform.position, position) <= MakeStaticDistance)
        {
            line.material = baseMaterial;
        }
        else
        {
            line.material = null;
        }
        
        yield return new WaitForSeconds(Time.deltaTime);
        StartCoroutine(ShowRange());
    }



    private void OnDrawGizmosSelected()
    {
        OnSelectedPreview();
    }




    public void OnSelectedPreview()
    {
        if (!Application.isPlaying)
        {
            frameTime = Time.deltaTime / 3;
            Preview(cableSections);
        }
    }




    //LA COUROUTINE BOUCLE QUOI QU'IL ARRIVE POUR PERMETTRE DE CHANGER EN COURS DE JEU. NEPAS OUBLIER DE CHANGER CA
    //Custom Framerate
    IEnumerator Loop()
    {

        while (true)
        {
            if (useCustomeFramerate)
            {
                frameTime = (1000 / framerate) / 1000;
            }
            else
            {
                frameTime = Time.deltaTime;
            }



            if (Vector3.Distance(_camera.transform.position, position) <= MakeStaticDistance || MakeStaticDistance == 0f)
            {
                inRange = true;

            }
            else
            {
                inRange = false;
                frameTime = 1f;
            }



            Preview(cableSections);

            yield return new WaitForSeconds(frameTime);

        }
    }







    protected void Preview(CableSection[] cableSections)
    {
        if (Application.isPlaying && StaticSettings)
        {
            return;
        }


        line.positionCount = 0;



        //Validity
        if (cableSections.Length == 0)
        {
            Debug.LogError("No sections assigned to Cable_Procedural component attached to " + gameObject.name);
            return;
        }



        // The Direction of SAG is the direction of gravity
        sagDirection = Vector3.down;



        // Draw each section 
        foreach (CableSection section in cableSections)
        {

            DrawCableSection(section);


        }



        iterations += 1;
    }





    protected void DrawCableSection(CableSection section)
    {

        if (section.start == null || section.end == null)
        {
            return;
        }

        // What point is being calculated


        int i = 0;






        // Get direction Vector.
        Vector3 vectorFromStartToEnd = section.end.position - section.start.position;
        // Setting the Start object to look at the end will be used for making the wind be perpendicular to the cable later
        section.start.forward = vectorFromStartToEnd.normalized;
        // Get number of points in the cable using the distance from the start to end, and the point density
        int pointsCount = Mathf.FloorToInt(pointDensity * vectorFromStartToEnd.magnitude);








        if (!StaticSettings && inRange)
        {
            section.swayValue += (generalSwayFrequency * section.swayFrequency) * frameTime;
        }




        #region Clamp the wind value to stay within a cirlce's radian limits.
        if (section.swayValue > Mathf.PI * 2) 
        {
            section.swayValue = 0; 
        }

        if (section.swayValue < 0) 
        {
            section.swayValue = Mathf.PI * 2; 
        }
        #endregion



        while (i < pointsCount)
        {
            // This is the fraction of where we are in the cable and it accounts for arrays starting at zero
            float pointForCalcs = (float)i / ((pointsCount - 1) > 0 ? pointsCount - 1 : 1);
            // This is what gives the cable a curve and makes the wind move the center the most
            float effectAtPointMultiplier = Mathf.Sin(pointForCalcs * Mathf.PI);



            // Get Point Position
            Vector3 pointPosition = vectorFromStartToEnd * pointForCalcs;
            // Add Sag Vector
            Vector3 sagAtPoint = sagDirection * section.sag * generalSagMultiplier;
            // Add Sway Vector
            Vector3 swayAtPoint = generalSwayMultiplier * swayMultiplier * section.swayMultiplier * section.start.transform.TransformDirection(new Vector3(Mathf.Sin(section.swayValue) * section.swayXMultiplier, Mathf.Cos(2 * section.swayValue + Mathf.PI) * .5f * section.swayYMultiplier, 0));



            // Calculate the postion with Sag
            Vector3 currentPointsPosition = section.start.position + pointPosition + (swayAtPoint + Vector3.ClampMagnitude(sagAtPoint, section.sag * generalSagMultiplier)) * effectAtPointMultiplier;



            // Add a point to the line renderer
            line.positionCount += 1;

            // Set point
            line.SetPosition(line.positionCount - 1, currentPointsPosition);



            i++;

        }
    }
}

