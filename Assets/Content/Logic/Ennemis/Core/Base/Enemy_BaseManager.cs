using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

[RequireComponent(typeof(Enemy_AudioDetection))]
[RequireComponent(typeof(Enemy_VisualDetection))]
public abstract class Enemy_BaseManager : MonoBehaviour, IWarnable
{
    #region State Selection
    public Enemy_BaseState CurrentState;

    public enum possibleInitialState
    {
        Enemy_Idle,
        Enemy_Patrol,
        Enemy_GettingCloser
    }


    [Header("    STATES SETTINGS")]
    [Space (5)]
    public possibleInitialState initialState;

    #endregion


        [Space(10)]


    #region State Settings

    [Header("   Neutral State")]
    [Space(7)]

    public bool useIdle;
    public bool usePatrol;



    [HideInInspector]
    public Enemy_IdleState IdleState;

    [HideInInspector]
    public Enemy_PatrolState PatrolState;


    [HideInInspector]
    public List<Enemy_NeutralState> NeutralStates = new List<Enemy_NeutralState>();



    [Header("   Binah detected")]
    [Space(7)]

    public bool useNeutralization;
    public bool useWarning;



    [HideInInspector]
    public Enemy_NeutralizationState NeutralizationState; 
    

    [HideInInspector]
    public Enemy_Struggling StrugglingState;


    [HideInInspector]
    public Enemy_WarningState WarningState;



    [HideInInspector]
    public List<Enemy_AttackingState> AttackingStates = new List<Enemy_AttackingState>();




    [Header("   Binah Lost or Enemy Warned")]
    [Space(7)]

    public bool useLost;
    public bool useWarned;



    [HideInInspector]
    public Enemy_LostState LostState;

    [HideInInspector]
    public Enemy_WarnedState WarnedState;



    [HideInInspector]
    public List<Enemy_SearchingState> SearchingStates = new List<Enemy_SearchingState>();

    #endregion


    [Space(20)]



    [Header ("    ENEMY SETTINGS")]
    [Space(15)]


    [SerializeField]
    private float _detectionProgression = 0f;

    public float DetectionProgression
    {
        get { return _detectionProgression; }

        set { _detectionProgression = Mathf.Clamp(value, 0, 100); 
              ManageStateWithDetection(); }
    }


    [Header ("Detection Settings")]
    [Space (5)]

    [SerializeField]
    private float detectionMultiplier;

    [SerializeField]
    private float detectionLoseRate;

    [SerializeField, Tooltip("Threshold to transition from Neutral to Attacking")]
    private float detectionThreshold;

    [SerializeField, Range(0f, 100f), Tooltip("Threshold in percentage of the Highest Detection Progression to transition from Attacking to Searching")]
    private float searchThresholdPercent;



    [Space(10)]



    [Header ("    Movements Settings")]
    [Space(5)]

    public float WalkMoveSpeed;
    public float RunMoveSpeed;

    [HideInInspector]
    public bool ArrivedOnWarning = false;



    [Space(10)]



    [Header("    Attacking Settings")]
    [Space(5)]

    public float AttackRange;



    [Space(10)]



    [Header("    Warining Settings")]
    [Space(5)]

    public float WarningRadius;

    [SerializeField, Tooltip ("The duration in wich the AI stay locked in the Warning State")]
    private float warningDuration;

    public float WarnedDetectionFill;



    [Space(20)]



    [Header ("    Patrol Settings")]
    [Space (5)]

    [SerializeField, Tooltip ("The radius in wich the AI can detect a Patrol Route")]
    private float patrolDetectionRadius;


    [SerializeField, Tooltip ("The root selected by the IA, can be forced by selecting a scene reference.")]
    private PatrolRoute chosedPatrolRoute;







    // REFERENCES 

    [HideInInspector]
    public GameObject Binah;

    [HideInInspector]
    public UtilityAI_Manager BinahManager;

    [HideInInspector]
    public NavMeshAgent Agent;

    [HideInInspector]
    public Vector3 InitialPosition;

    //The coroutine containing the patrol logic
    private Coroutine patrolCoroutine;

    private float highestDetectionProgression;

    [HideInInspector]
    public bool IsLoosingInterest = false;

    [HideInInspector]
    public Vector3 LastKnownPosition = Vector3.zero;

    private bool isWarning = false;





    protected void BaseAwake()
    {
        //Get References
        Binah = GameManager.Instance.Binah;
        BinahManager = Binah.GetComponent<UtilityAI_Manager>();

        Agent = GetComponent<NavMeshAgent>();


        //Get Variables
        InitialPosition = transform.position;


        //Set Movement Speed
        Agent.speed = WalkMoveSpeed;




        #region Debug 
        //Prevent Bugs
        switch (initialState)
        {
            case possibleInitialState.Enemy_Idle:
                if (!useIdle)
                {
                    Debug.LogError("Le State Initial de " + gameObject.name + " n'est pas seletionné parmis les States possibles");
                    Debug.Break();
                }
                break;
        }
        switch (initialState)
        {
            case possibleInitialState.Enemy_Patrol:
                if (!usePatrol)
                {
                    Debug.LogError("Le State Initial de " + gameObject.name + " n'est pas seletionné parmis les States possibles");
                    Debug.Break();
                }
                break;
        }
        #endregion




        #region Neutral State List (Manage initial State too)
        if (useIdle)
        {
            NeutralStates.Add(IdleState = new Enemy_IdleState());
            IdleState.BaseManager = this;

            //Set Initial State
            if (initialState == possibleInitialState.Enemy_Idle)
            {
                SwitchState(IdleState);
            }
        }
        if (usePatrol)
        {
            NeutralStates.Add(PatrolState = new Enemy_PatrolState());
            PatrolState.BaseManager = this;

            //Set Initial State
            if (initialState == possibleInitialState.Enemy_Patrol)
            {
                SwitchState(PatrolState);
            }
        }
        #endregion

        #region Attack State
        if (useNeutralization)
        {
            AttackingStates.Add(NeutralizationState = new Enemy_NeutralizationState());
            NeutralizationState.BaseManager = this;
        }
        if (useWarning)
        {
            AttackingStates.Add(WarningState = new Enemy_WarningState());
            WarningState.BaseManager = this;
        }

        StrugglingState = new Enemy_Struggling(); //Ne pas ajouter a l'array de State d'Attaque
        StrugglingState.BaseManager = this;
        #endregion

        #region Binah Lost or Enemy Warned
        if (useLost)
        {
            SearchingStates.Add(LostState = new Enemy_LostState());
            LostState.BaseManager = this;
        }
        if (useWarned)
        {
            SearchingStates.Add(WarnedState = new Enemy_WarnedState());
            WarnedState.BaseManager = this;
        }

        #endregion



        if (CurrentState != null)
        {
            CurrentState.AwakeState();
        }
    }




    protected void BaseStart()
    {
        StartCoroutine(NotSeeingBinah());


        if (CurrentState == null)
        {
            return;
        }

        CurrentState.StartState();
    }

    protected void BaseUpdate()
    {
        if (CurrentState == null)
        {
            return;
        }

        CurrentState.UpdateState();
    }

    protected void BaseFixedUpdate()
    {
        if (CurrentState == null)
        {
            return;
        }

        CurrentState.FixedUpdateState();
    }






    //Detection Management
    #region
    public void SeeingBinah(float detectionIncrement)
    {
        DetectionProgression += detectionIncrement* detectionMultiplier;
        LastKnownPosition = Binah.transform.position;
    }


    IEnumerator NotSeeingBinah()
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemyManager.Instance.timeBeteenEnemyUpdate);

            if (!IsLoosingInterest)
            {
                highestDetectionProgression = DetectionProgression;
                continue;
            }

            DetectionProgression -= detectionLoseRate * EnemyManager.Instance.timeBeteenEnemyUpdate;
        }
    }
    #endregion



    






    //According to Detection
    private void ManageStateWithDetection()
    {
        switch (CurrentState)
        {
            case Enemy_NeutralState:
                //Debug.Log("Neutral State");
                ChangeFromNeutralState();
                break;

            case Enemy_AttackingState:
                //Debug.Log("Attack State");
                ChangeFromAttackingState();

                break;

            case Enemy_SearchingState:
                //Debug.Log("Search State");
                ChangeFromSearchingState();
                break;
        }
    }

    
    private void ChangeFromNeutralState()
    {
        if (DetectionProgression < detectionThreshold)
        {
            return;
        }


        if (WarningState != null)
        {
            SwitchState(WarningState);
            return;
        }


        //Change to Attacking
        SwitchState(AttackingStates[Random.Range(0, AttackingStates.Count)]);
    }


    private void ChangeFromAttackingState()
    {
        if (isWarning)
        {
            return;
        }


        if (DetectionProgression > highestDetectionProgression / 100 * searchThresholdPercent)       
        {
            return;
        }



        if (LostState != null)// && DetectionProgression > detectionThreshold)
        {
            SwitchState(LostState);
            return;
        }

        //Change to Neutral
        SwitchState(NeutralStates[Random.Range(0, NeutralStates.Count)]);
    }


    private void ChangeFromSearchingState()
    {
        if (CurrentState == WarnedState)
        {
            Debug.Log(" Warned State");


            if (LostState == null)
            {
                Debug.Log(" State null");

                return;
            }

            if (ArrivedOnWarning)
            {
                SwitchState(LostState);
            }
            else
            {
                return;
            }
        }
        


        if (DetectionProgression == 0)              // Je peux remplacer le 0 par une variable
        {
            //Change to Neutral
            SwitchState(NeutralStates[Random.Range(0, NeutralStates.Count)]);
            return;
        }

        if (DetectionProgression > detectionThreshold && !IsLoosingInterest)
        {
            //Change to Attacking
            SwitchState(AttackingStates[Random.Range(0, AttackingStates.Count)]);
            return;
        }

    }













    public void StartWarning()
    {
        if (isWarning)
        {
            return;
        }

        StartCoroutine(IsWarning());
    }

    public IEnumerator IsWarning()
    {
        isWarning = true;


        yield return new WaitForSeconds(warningDuration);


        isWarning = false;

        AttackingStates.Remove(WarningState);
        SwitchState(NeutralStates[Random.Range(0, AttackingStates.Count)]);
        WarningState = null;
    }










    public void GetWarned(Vector3 _warningPosition)
    {
        if (useWarned)
        {
            WarnedState.WarningPosition = _warningPosition;
            SwitchState(WarnedState);
        }
    }


    public void StopAgent()
    {
        Agent.SetDestination(transform.position);
    }


    public void MoveAgent(Vector3 targetPosition)
    {
        Agent.SetDestination(targetPosition);
    }



    public virtual void SwitchState(Enemy_BaseState newState)
    {
        if (newState == null)
        {
            print("Null State");
            return;
        }

        //Notify Old State if Different
        if (newState != CurrentState)
        {
            if (CurrentState != null)
            {
                CurrentState.ExitState();
            }
        }

        //Set New State
        CurrentState = newState;

        //Notify New State
        CurrentState.EnterState();
    }





    public void StartPatrolling()
    {
        patrolCoroutine = StartCoroutine(PatrolCoroutine());
    }


    public void StopPatrolling()
    {

        if (patrolCoroutine == null || chosedPatrolRoute == null)
        {
            return;
        }


        chosedPatrolRoute.isAvaliable = true;

        StopCoroutine(patrolCoroutine);
        patrolCoroutine = null;
        chosedPatrolRoute = null;
    }




    IEnumerator PatrolCoroutine()
    {
        yield return new WaitForNextFrameUnit();

        List<PatrolRoute> partrolRoutes = new List<PatrolRoute>();
        int currentIndex = 0;


        // Search and Chose Patrol Route
        #region
        if (chosedPatrolRoute == null)
        {
            partrolRoutes.Clear();


            //Get Route in Range
            Collider[] colliders = Physics.OverlapSphere(transform.position, patrolDetectionRadius, LayerMask.GetMask("Patrol Route"));


            //Stop if no Route in Range
            if (colliders.Length == 0)
            {
                Debug.Log("SHOULD change state");

                SwitchState(IdleState);
                yield break;
            }


            //Récupère les scripts
            foreach (Collider collider in colliders)
            {
                PatrolRoute patrolRoute = collider.GetComponent<PatrolRoute>();

                //partrolRoutes.Add(patrolRoute);

                Debug.Log(patrolRoute.isAvaliable);

                if (patrolRoute.isAvaliable)
                {
                    partrolRoutes.Add(patrolRoute);
                }

            }

            //Stop if no Route Avalaible
            if (partrolRoutes.Count == 0)
            {
                Debug.Log("SHOULD change state");

                SwitchState(IdleState);
                yield break;
            }


            chosedPatrolRoute = partrolRoutes[Random.Range(0, partrolRoutes.Count)];
            chosedPatrolRoute.isAvaliable = false;
        }
        #endregion













        // Start from Closest Point
        #region

        float currentDistance = Mathf.Infinity;



        foreach (GameObject partrolPoint in chosedPatrolRoute.PatrolPoints)
        {
            NavMeshPath path = new NavMeshPath();
            Agent.CalculatePath(partrolPoint.transform.position, path);


            float distanceToTarget = 0f;


            if (path.status == NavMeshPathStatus.PathComplete)
            {
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    distanceToTarget += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                }
            }





            //float testDistance = Vector3.Distance(transform.position, partrolPoint.transform.position);

            if (distanceToTarget < currentDistance)
            {

                currentDistance = distanceToTarget;


                currentIndex = System.Array.IndexOf(chosedPatrolRoute.PatrolPoints, partrolPoint);           
            }
        }

        #endregion










        MoveAgent(chosedPatrolRoute.PatrolPoints[currentIndex].transform.position);
        yield return new WaitForSeconds(0.1f);



        while (true)
        {

            // No Patrol Route
            if (chosedPatrolRoute == null)
            {
                Debug.Log("No Patrol Route");

                //Loop on FixedUpdate
                yield return new WaitForFixedUpdate();
                continue;
            }


            // Should Not be Moving to Pass
            if (Agent.velocity.magnitude != 0)
            {
                //Loop on FixedUpdate
                yield return new WaitForFixedUpdate();
                continue; 
            }


            int previousIndex = currentIndex;

            currentIndex += 1;
            if (currentIndex >= chosedPatrolRoute.PatrolPoints.Length)  // CurrentIndex comapred to MaxIndex
            {
                currentIndex = 0;
            }


            //Delay frome the Waiting Time of the Current Point
            yield return new WaitForSeconds(chosedPatrolRoute.PatrolPoints[previousIndex].GetComponent<PatrolPoint>().WaitTime);         // Devrait pas etre current mais past


            //Move Agent
            MoveAgent(chosedPatrolRoute.PatrolPoints[currentIndex].transform.position);


            //Loop with delay top prevent the random skipping of points
            yield return new WaitForSeconds(0.1f);

        }
    }



    public void Kill()
    {
        //Deactivate Enemy
        transform.Translate(new Vector3(0, -0.8f, 0));
        transform.Rotate(new Vector3(0, 0, -90));
        transform.Translate(new Vector3(0, 0.7f, 0));

        transform.GetChild(0).GetComponent<AII_StealthNeutralization>().enabled = false;
        transform.GetChild(0).GetComponent<CapsuleCollider>().enabled = false;

        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Enemy_AudioDetection>().enabled = false;
        GetComponent<Enemy_VisualDetection>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;

        this.enabled = false;

    }


}
