using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class UtilityAI_Manager : MonoBehaviour
{
    // ----- SETTINGS VARIABLES -----

    #region Settings

    [Header("     ----- GENERAL -----")]
    [Space(7)]

    public float WalkSpeed;
    public float CrouchSpeed;
    public float ScaffoldingStairsSpeed;
    public float LadderSpeed;

    [Range(1f, 4f)]
    public float outOfScreenSpeedMultiplier;

    [HideInInspector]
    public float currentSpeed;
    public float speedMultiplier;



    [Space(15)]
    [Header("     ----- AI ------")]
    [Space(17)]




    [Header("     SETTINGS")]
    [Space(7)]

    [SerializeField]
    [Tooltip("The time used for methodes that need to be checked regularly but are to heavy to by done in FixedUpdate()")]
    public float CustomUpdateFrequency = 0.5f;


    [Space(15)]
    [Header("     VARIABLES")]
    [Space(7)]

    public StateVariablesDictionnary StateVariablesDictionnary;


    #region Action
    [Space(15)]
    [Header("ACTION")]
    [Space(7)]

    public float ActionDetectionRadius;
    public float MaxPathLength;
    public float IndicatedActionMinScore;


    [Space(15)]
    [Header("INDEPENDENCE")]
    [Space(7)]

    public AnimationCurve IndependenceCurve;
    public float MinIndependenceForInitiative;
    [Tooltip ("The minimum score of a detected action to be a valid choice.")]
    public float IndependenceMinScore;

    [Space (7)]

    [Tooltip ("The Time in Seconds Before wich the AI Check if a Danger is near and if she need to Take Action. 0 mean No Danger Verification")]
    public float IdleTimeBeforeFlee;
    #endregion

    #region Flee
    [Space(15)]
    [Header("FLEE")]
    [Space(7)]

    [Header("     General")]
    [Space(5)]

    public float DetectEnemyToFleeRadius;

    [Header("     Hide")]
    [Space(5)]

    public float SearchHideSpotRadius;
    public float SearchDensity;


    [Header("     Cover")]
    [Space(5)]

    public float SearchCoverRadius;
    public Consideration CoverConsideration;

    [Header("     Flee")]
    [Space(5)]

    public float FleeDistance;

    #endregion





    [Space(5)]
    [Header("     DEBUG")]
    [Space(7)]

    public bool UseDebugTool;
    #endregion


    public Animator animator;




    // ----- WORKING VARIABLES -----

    //State Machine Variables
    public UtilityAI_BaseState currentState;
    public UtilityAI_Idle IdleState = new UtilityAI_Idle();
    public UtilityAI_Fleeing FleeState = new UtilityAI_Fleeing();
    public UtilityAI_DoingAction DoingActionState = new UtilityAI_DoingAction();
    public UtilityAI_Moving MovingState = new UtilityAI_Moving();
    public UtilityAI_Scripted ScriptedState = new UtilityAI_Scripted();

    //New State 
    public UtilityAI_Struggling StrugglingState = new UtilityAI_Struggling();
    public UtilityAI_Neutralized NeutralizedState = new UtilityAI_Neutralized();
    public UtilityAI_Immobilized ImmobilizedState = new UtilityAI_Immobilized();



    [HideInInspector]
    public GameObject Object;

    [HideInInspector]
    public NavMeshAgent Agent;

    //Interactive Object Mask
    [HideInInspector]
    public LayerMask AI_InteractionObject;

    [HideInInspector]
    public AI_Interaction ActionToDo;

    [HideInInspector]
    public UitlityAI_DebugTool DebugTool;

    [HideInInspector]
    public Vector3 IndicatedPosition;

    [HideInInspector]
    public List<GameObject> EnemyToFlee;

    [HideInInspector]
    public bool isHidenBehindCover = false;

    [HideInInspector]
    public bool usingLadder;

    [HideInInspector]
    public bool usingScaffoldingStairs;


    [HideInInspector]
    public bool isCrouched = false;

    [HideInInspector]
    public bool CanRecieveInput = true;


    [HideInInspector]
    public bool InRestrictiveZone;

    [HideInInspector]
    public bool IsAutomaticalyMovingToCamera;



    private void OnValidate()
    {
        if (DebugTool == null && UseDebugTool)
        {
            DebugTool = gameObject.AddComponent<UitlityAI_DebugTool>();
            DebugTool.UtilityAI_Manager = this;

        }
    }





    private void Awake()
    {
        //Base References
        Object = gameObject;
        Agent = GetComponent<NavMeshAgent>();
        AI_InteractionObject = LayerMask.GetMask("AI Interactable");

        //Give Reference of This to each States
        IdleState.UtilityAI_Manager = this;
        FleeState.UtilityAI_Manager = this;
        DoingActionState.UtilityAI_Manager = this;
        MovingState.UtilityAI_Manager = this;
        ScriptedState.UtilityAI_Manager = this;


        //New Sate
        StrugglingState.UtilityAI_Manager = this;
        NeutralizedState.UtilityAI_Manager = this;
        ImmobilizedState.UtilityAI_Manager = this;




        currentState = IdleState;
        SwitchState(currentState);



        //Start CustomUpdate
        StartCoroutine(CustomUpdate());
    }



    /// <summary>
    /// The method use by external script to make Binah enter Struggling State
    /// </summary>
    public void StartStruggling()
    {
        SwitchState(StrugglingState);
    }


    /// <summary>
    /// The method use by external script to make Binah her Neutralized State and the end game logic.
    /// </summary>
    public void GetNeutralized()
    {
        

    }








    private void Update()
    {
        currentState.UpdateState();
    }


    private void FixedUpdate()
    {
        currentState.FixedUpdateState();
        UpdateMoveSpeed();

        if (Agent.velocity.magnitude == 0 && currentState == MovingState)
        {
            SwitchState(IdleState);
        }

    }


    private IEnumerator CustomUpdate()
    {
        //Call Method
        currentState.CustomUdpateState();
        
        //Loop
        yield return new WaitForSeconds(CustomUpdateFrequency);
        StartCoroutine(CustomUpdate());
    }






    /// <summary>
    /// Methode to call in order to Switch State. Reference the New State using State Manager variable.
    /// </summary>
    public void SwitchState(UtilityAI_BaseState newState)
    {
        //Notify Old State if Different
        if (newState != currentState)
        {
            currentState.ExitState();
        }

        //Set New State
        currentState = newState;

        //Notify New State
        newState.EnterState();


        if (DebugTool != null && UseDebugTool)
        {
            DebugTool.DisplayCurrentState(newState);

        }

    }





    // ---------- CONTROLS ----------

    /// <summary>
    /// This method has the purpose to centralize the input management.
    /// </summary>
    public void SendBinahToLocation(Vector3 position)
    {
        if (!CanRecieveInput)
        {
            return;
        }

        if (GameManager.Instance.CameraController.CurrentCamera.binahJoinTargetPosition != null)
        {
            if (position == GameManager.Instance.CameraController.CurrentCamera.binahJoinTargetPosition.position)
            {
                IsAutomaticalyMovingToCamera = true;
            }
            else
            {
                IsAutomaticalyMovingToCamera = false;
            }
        }



        IndicatedPosition = position;
        SwitchState(MovingState);
    }


    /// <summary>
    /// This method has the purpose to centralize the input management.
    /// </summary>
    public void DoIndicatedAction(AI_Interaction action)
    {
        if (!CanRecieveInput)
        {
            return;
        }

        SwitchState(IdleState);
        IdleState.ScoreIndicatedAction(action);
    }


    //Input System
    private void OnCrouch(InputValue value)
    {
        if (!CanRecieveInput)
        {
            return;
        }

        if (value.Get<float>() == 0)
        {
            return;
        }

        Crouching(!isCrouched);
    }

    /// <summary>
    /// the Method used to make the IA Crouch / Uncrouch.
    /// </summary>
    public void Crouching(bool newCrouchingState)
    {
        isCrouched = newCrouchingState;
    }





    private void UpdateMoveSpeed()
    {
        if (usingLadder)
        {
            currentSpeed = LadderSpeed;
        }
        else if (usingScaffoldingStairs)
        {
            if (isCrouched)
            {
                currentSpeed = ScaffoldingStairsSpeed * (CrouchSpeed / WalkSpeed);
            }
            else
            {
                currentSpeed = ScaffoldingStairsSpeed;
            }
        }
        else
        {
            if (isCrouched)
            {
                currentSpeed = CrouchSpeed;
            }
            else
            {
                currentSpeed = WalkSpeed;
            }
        }
        

        Agent.speed = currentSpeed * speedMultiplier;
    }

}
