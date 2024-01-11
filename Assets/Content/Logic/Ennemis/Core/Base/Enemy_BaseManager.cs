using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy_AudioDetection))]
[RequireComponent(typeof(Enemy_VisualDetection))]
public abstract class Enemy_BaseManager : MonoBehaviour, IWarnable
{
    #region State Selection

    [Header("Initial State")]
    public Enemy_BaseState currentState;

    public enum possibleInitialState
    {
        Enemy_Idle,
        Enemy_Patrol,
        Enemy_GettingCloser
    }



    public possibleInitialState initialState;

    #endregion


        [Space(20)]


    #region State Settings

    [Header("   Neutral State")]
    [Space(7)]

    public bool useIdle;
    public bool usePatrol;
    public bool useGettingCloser;



    [HideInInspector]
    public Enemy_IdleState IdleState;

    [HideInInspector]
    public Enemy_PatrolState PatrolState;

    [HideInInspector]
    public Enemy_GettingCloserState GettingCloserState;



    [HideInInspector]
    public List<Enemy_NeutralState> NeutralStates = new List<Enemy_NeutralState>();



    [Header("   Binah detected")]
    [Space(7)]

    public bool useNeutralization;
    public bool useImmobilization;
    public bool useWarning;



    [HideInInspector]
    public Enemy_NeutralizationState NeutralizationState;

    [HideInInspector]
    public Enemy_ImmobilizationState ImmobilizationState;

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



    [Header ("  ENEMY SETTINGS")]


    [SerializeField]
    private float _detectionProgression = 0f;

    public float DetectionProgression
    {
        get { return _detectionProgression; }

        set { _detectionProgression = Mathf.Clamp(value, 0, 100); 
              ManageStateWithDetection(); }
    }

    [SerializeField]
    private float detectionMultiplier;

    [SerializeField]
    private float detectionLoseRate;




    //The highest detection progression since the ennemis started loosing interest
    //[SerializeField]
    private float highestDetectionProgression;





    [SerializeField]
    private float warningDuration;



















    [Space(15)]


    public float WalkMoveSpeed;

    public float RunMoveSpeed;




    public GameObject[] PatrolPoints;





    public float AttackRange;




    public float WarningRadius;

    [HideInInspector]
    public bool IsLoosingInterest = false;



    public float WarnedDetectionFill;


    [HideInInspector]
    public bool ArrivedOnWarning = false;



    private bool isWarning = false;




    [HideInInspector]
    public Vector3 LastKnownPosition = Vector3.zero;






    // REFERENCES 

    [HideInInspector]
    public GameObject Binah;

    [HideInInspector]
    public UtilityAI_Manager BinahManager;

    [HideInInspector]
    public NavMeshAgent Agent;



    [HideInInspector]
    public Vector3 InitialPosition;




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
        switch (initialState)
        {
            case possibleInitialState.Enemy_GettingCloser:
                if (!useGettingCloser)
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
        if (useGettingCloser)
        {
            NeutralStates.Add(GettingCloserState = new Enemy_GettingCloserState());
            GettingCloserState.BaseManager = this;

            //Set Initial State
            if (initialState == possibleInitialState.Enemy_GettingCloser)
            {
                SwitchState(GettingCloserState);
            }
        }
        #endregion

        #region Attack State
        if (useImmobilization)
        {
            AttackingStates.Add(ImmobilizationState = new Enemy_ImmobilizationState());
            ImmobilizationState.BaseManager = this;
        }
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



        if (currentState != null)
        {
            currentState.AwakeState();
        }
    }




    protected void BaseStart()
    {
        StartCoroutine(NotSeeingBinah());


        if (currentState == null)
        {
            return;
        }

        currentState.StartState();
    }

    protected void BaseUpdate()
    {
        if (currentState == null)
        {
            return;
        }

        currentState.UpdateState();
    }

    protected void BaseFixedUpdate()
    {
        if (currentState == null)
        {
            return;
        }

        currentState.FixedUpdateState();
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



    




    [Space(20)]

    [SerializeField, Tooltip ("Threshold to transition from Neutral to Attacking")]
    private float detectionThreshold;

    [SerializeField, Range (0f, 100f), Tooltip("Threshold in percentage of the Highest Detection Progression to transition from Attacking to Searching")]
    private float searchThresholdPercent;





    //According to Detection
    private void ManageStateWithDetection()
    {
        switch (currentState)
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
        if (currentState == WarnedState)
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
        if (newState != currentState)
        {
            if (currentState != null)
            {
                currentState.ExitState();
            }
        }

        //Set New State
        currentState = newState;

        //Notify New State
        currentState.EnterState();
    }
}
