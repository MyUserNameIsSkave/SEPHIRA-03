using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnnemisBehavior : MonoBehaviour
{
    public AudioSource detectBinah;

    #region Waypoint Draw & Variables

    [SerializeField]
    List<Color> WaypointColor;

    [Space (15)]

    [SerializeField]
    List<Vector3> WaypointLocal;



    //Working Variables
    [HideInInspector] 
    public List<Vector3> WaypointWorld;     //Need to be Public






    [SerializeField]
    private EnnemisAnimation EnnemiAnimation;






    //Variables (Local / World)
    private void OnValidate()
    {
        Vector3 BasePosition = transform.position;

        //Iteraction
        int Interation = 0;
        WaypointWorld.Clear();

        foreach (Vector3 LocalPoint in WaypointLocal)
        {


            WaypointWorld.Add(LocalPoint + BasePosition);

            Interation++;
        }
    }



    //Draw
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        int Interation = 0;

        foreach (Vector3 WorldPoint in WaypointWorld)
        {
            Gizmos.color = Color.white;

            if (Interation <= WaypointColor.Count - 1)
            {
                Gizmos.color = WaypointColor[Interation];
            }


            Gizmos.DrawWireSphere(WorldPoint, 0.5f);
            Interation++;
        }
    }

    #endregion



    [SerializeField]
    float PatrolSpeed, ChaseSpeed;

    #region Patrol

    //Settings
    [SerializeField]
    List<float> WaypointWaiting;




    //Working Variables
    int CurrentWaypoint = 0;
    

    [SerializeField]
    NavMeshAgent Agent;




    private void Start()
    {
        //Commencer la Loop
        if (WaypointLocal.Count != 0)
        {
            StartCoroutine(Patrol(CurrentWaypoint + 1));

        }
        
        
    }


    IEnumerator Patrol(int CurrentTargetIndex)
    {
        Agent.speed = PatrolSpeed;
        Vector3 TargetPosition = WaypointWorld[CurrentTargetIndex];
        Agent.destination = TargetPosition;

        while (true)
        {
            
            if (Vector3.Distance(transform.position, TargetPosition) < 0.1f)
                break;

            yield return null;
        }



        //Wait
        yield return new WaitForSeconds(WaypointWaiting[CurrentTargetIndex]);



        #region Loop the Index

        //Loop the Patrol Path
        CurrentWaypoint++;

        if (CurrentWaypoint > WaypointWorld.Count - 1)
        {
            CurrentWaypoint = 0;
        }

        StartCoroutine(Patrol(CurrentWaypoint));

        #endregion
    }

    #endregion



    #region Chasing Binah


    // ----- Detect Binah -----

    //Time Needed for the Ennemis to Detect Binah
    [SerializeField]
    private float DetectionThreshold;

    Coroutine DetectionThresholdCoroutine;


    // ----- Lose Binah -----

    //Time Needed for the Ennemis to Give Up
    [SerializeField]
    float LoseThreshold;

    //The Time Binah has been lost
    float LoseStartTime;


    // ----- Chasing -----

    [HideInInspector]
    public bool IsChasing;







    // ----- Event Methods -----

    //Binah Enter Vision
    public void SeeBinah()
    {
        //Check Time Threshold
        if (!IsChasing)
        {
            detectBinah.Play();
        }
        
        DetectionThresholdCoroutine = StartCoroutine(CheckDetectionThreshold());
        LoseStartTime = Time.time;

    }

    //Binah Exit Vision
    public void LoseBinah()
    {
        if (DetectionThresholdCoroutine != null)
        {
            StopCoroutine(DetectionThresholdCoroutine);
        }

  
        

        if (IsChasing)
        {
            Agent.speed = ChaseSpeed;
            StartCoroutine(CheckLoseThreshold());
        }
    }

    private void TrackBinah()
    {
        IsChasing = true;
    }

    public void BackToPatrol()
    {
                if (WaypointLocal.Count != 0)
        {
            StartCoroutine(Patrol(CurrentWaypoint));

        }

    }






    // ----- Timer Coroutines -----

    //Add Detection delay
    IEnumerator CheckDetectionThreshold()
    {
        float StartTime = Time.time;

        while (true)
        {


            if (Time.time - StartTime >= DetectionThreshold)
            {
                break;
            }

            yield return null;
        }

        TrackBinah();
    }

    IEnumerator CheckLoseThreshold()
    {
        LoseStartTime = Time.time;

        while (true)
        {


            if (Time.time - LoseStartTime >= LoseThreshold)
            {
                break;
            }

            yield return null;
        }

        IsChasing = false;
        StopAllCoroutines();
        BackToPatrol();
    }





    //----- Update -----

    //Follow Binah
    private void Update()
    {
        transform.GetChild(0).GetComponent<Animator>().SetFloat("Speed", Agent.velocity.magnitude);



        if (IsChasing)
        {
            Agent.destination = GameObject.FindGameObjectWithTag("Binah").transform.position;
        }




        //Animation

        if (IsStruggling)
        {
            EnnemiAnimation.Struggling();
            GameObject.FindGameObjectWithTag("Binah").GetComponent<BinahAnimation>().Struggling();
        }


    }


    #endregion








    [HideInInspector]
    public bool IsStruggling = false;


    public int StruggleClickCount;
    int Clicks = 0;

    private void OnMouseDown()
    {
        if (!IsStruggling)
        {
            return;
        }


        Clicks++;
        print(Clicks);


        if (Clicks > StruggleClickCount)
        {
            IsStruggling = false;
            EnnemiAnimation.OnPunched();
            GameObject.FindGameObjectWithTag("Binah").GetComponent<BinahAnimation>().OnPunch();
            //Destroy(gameObject);

            GetComponent<CapsuleCollider>().enabled = false;    
            Destroy(transform.GetChild(1).gameObject);
            Destroy(transform.GetChild(2).gameObject);
            GameObject.FindGameObjectWithTag("Binah").GetComponent<NavMeshAgent>().speed = 3.5f;
            enabled = false;
        }
    }
}
