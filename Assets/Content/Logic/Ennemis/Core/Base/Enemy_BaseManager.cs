using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Enemy_AudioDetection))]
[RequireComponent(typeof(Enemy_VisualDetection))]
public abstract class Enemy_BaseManager : MonoBehaviour, IWarnable
{
    #region State Selection

    [Header ("Initial State")]
    public Enemy_BaseState currentState;

    public enum possibleInitialState 
    {
        Enemy_Idle,
        Enemy_Patrol,
        Enemy_GettingCloser
    }

    public possibleInitialState initialState;

    #endregion


    [Space (60)]


    #region State Settings

    [Header("   Neutral State")]
    public bool useIdle;
    public bool usePatrol;
    public bool useGettingCloser;

    public Enemy_Idle IdleState;
    public Enemy_Patrol PatrolState;
    public Enemy_GettingCloser GettingCloserState;

    public List<Enemy_NeutralState> NeutralStates = new List<Enemy_NeutralState>();



    [Header("   Binah in Sight State"), Tooltip ("Thoses state might be obligatory so maybe remove the option")]
    public bool useInvestigating;
    public bool useChasing;

    public Enemy_Investigating InvestingatingState;
    public Enemy_Chasing ChasingState;

    public List<Enemy_InSightState> InSightStates = new List<Enemy_InSightState>();




    [Header("   Binah In Range State")]
    public bool useNeutralization;
    public bool useImmobilization;
    [Tooltip ("Might only work if used alone")]
    public bool useWarning;
    public bool useStruggling;

    public Enemy_Struggling StrugglingState;
    public Enemy_Neutralization NeutralizationState;
    public Enemy_Immobilization ImmobilizationState;
    public Enemy_Warning WarningState;

    public List<Enemy_InRangeState> InRangeStates = new List<Enemy_InRangeState>();




    [Header("   Binah Lost State")]
    public bool useSearch;
    public bool useWarned;

    public Enemy_Search SearchState;
    public Enemy_Warned WarnedState;


    public List<Enemy_LostState> LostStates = new List<Enemy_LostState>();

    #endregion


    [Space(20)]


    #region Ennemy Settings and Variabes

    [Header("Settings")]
    public float detectionRate = 25f;
    public float loseRate = 12.5f;

    public float WalkSpeed = 1f;
    public float RunSpeed = 2.5f;
    public float canAttackDistance = 1f;

    public float WarningRadius = 10f;




    [Header ("Variables")]
    [SerializeField]
    private float _detectionProgression = 0f;

    public float DetectionProgression
    {
        get { return _detectionProgression; }

        set { _detectionProgression = Mathf.Clamp(value, 0, 100); 
              if (_detectionProgression == 100) {currentState.DetectedBinah(); }}

    }

    public bool isLosingInterest = false;


    public Vector3 lastSeenPosition;

    public Vector3 warningPosition;



    //References
    public NavMeshAgent agent;
    public GameObject Binah;




    #endregion












    #region Debug

    [Space (20)]
    [Header ("Debug")]

    [SerializeField]
    protected bool displayCurrentState = true;

    #endregion


    private void OnValidate()
    {
        if (displayCurrentState)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    [HideInInspector]
    public UtilityAI_Manager BinahManager;

    protected void BaseAwake()
    {
        //Get AI Manager references
        BinahManager = GameManager.Instance.Binah.GetComponent<UtilityAI_Manager>();



        //Get References
        agent = GetComponent<NavMeshAgent>();
        Binah = GameObject.FindGameObjectWithTag("Binah");

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


        //Instanciate Used States and set Initial State
        #region Neutral State List (Manage initial State too)
        if (useIdle)
        {
            NeutralStates.Add(IdleState = new Enemy_Idle());
            IdleState.BaseManager = this;

            //Set Initial State
            if (initialState == possibleInitialState.Enemy_Idle)
            {
                SwitchState(IdleState);
            }
        }
        if (usePatrol)
        {
            NeutralStates.Add(PatrolState = new Enemy_Patrol());
            PatrolState.BaseManager = this;

            //Set Initial State
            if (initialState == possibleInitialState.Enemy_Patrol)
            {
                SwitchState(PatrolState);
            }
        }
        if (useGettingCloser)
        {
            NeutralStates.Add(GettingCloserState = new Enemy_GettingCloser());
            GettingCloserState.BaseManager = this;

            //Set Initial State
            if (initialState == possibleInitialState.Enemy_GettingCloser)
            {
                SwitchState(GettingCloserState);
            }
        }
        #endregion

        #region In Sight State List
        if (useInvestigating)
        {
            InSightStates.Add(InvestingatingState = new Enemy_Investigating());
            InvestingatingState.BaseManager = this;
        }
        if (useChasing)
        {
             InSightStates.Add(ChasingState = new Enemy_Chasing());
             ChasingState.BaseManager = this;
        }
        #endregion

        #region In Range State List
        if (useNeutralization)
        {
             InRangeStates.Add(NeutralizationState = new Enemy_Neutralization());
             NeutralizationState.BaseManager = this;
        }
        if (useImmobilization)
        {
             InRangeStates.Add(ImmobilizationState = new Enemy_Immobilization());
             ImmobilizationState.BaseManager = this;
        }
        if (useWarning)
        {
            InRangeStates.Add(WarningState = new Enemy_Warning());
            WarningState.BaseManager = this;
        }
        if (useStruggling)
        {
            InRangeStates.Add(StrugglingState = new Enemy_Struggling());
            StrugglingState.BaseManager = this;
        }
        #endregion

        #region Lost State List
        if (useSearch)
        {
            LostStates.Add(SearchState = new Enemy_Search());
            SearchState.BaseManager = this;
        }
        if (useWarned)
        {
            LostStates.Add(WarnedState = new Enemy_Warned());
            WarnedState.BaseManager = this;
        }
        #endregion
    }




    protected void BaseStart()
    {

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
        if (isLosingInterest)
        {
            LoseInterest();
        }




        if (currentState == null)
        {
            return;
        }
        currentState.FixedUpdateState();
    }




    /// <summary>
    /// Methode to call in order to Switch State. Reference the New State using State Manager variable.
    /// </summary>
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
        newState.EnterState();
    }







    public void GetWarned(Vector3 _warningPosition)
    {
        if (useWarned)
        {
            warningPosition = _warningPosition;
            SwitchState(WarnedState);
        }
    }


    public abstract void IsWarning();




    public void MoveAgentTo(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
    }

    public void ChangeAgentSpeed(float movementSpeed)
    {
        agent.speed = movementSpeed;
    }




    private void LoseInterest()
    {
        DetectionProgression -= loseRate * Time.deltaTime;
    }


    //IMPLEMENTER ICI DES METHODES PO
    //URCHANGER D ETAT AVEC LES CHANGEMENTS DE VARAIBLES APPROPRIES ETC.
    //PRENNANT EN COMPTE LES ETAT DISPONIBLES ET CONDITIONS
}
