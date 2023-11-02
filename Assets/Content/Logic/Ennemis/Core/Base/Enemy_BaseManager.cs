using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


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

    protected Enemy_Idle IdleState;
    protected Enemy_Patrol PatrolState;
    protected Enemy_GettingCloser GettingCloserState;

    protected List<Enemy_NeutralState> NeutralStates = new List<Enemy_NeutralState>();



    [Header("   Binah in Sight State")]
    public bool useChasing;
    public bool useWarning;

    protected Enemy_Chasing ChasingState;
    protected Enemy_Warning WarningState;

    protected List<Enemy_InSight> InSightStates = new List<Enemy_InSight>();




    [Header("   Binah In Range State")]
    public bool useNeutralization;
    public bool useImmobilization;

    protected Enemy_Neutralization NeutralizationState;
    protected Enemy_Immobilization ImmobilizationState;

    protected List<Enemy_InRangeState> InRangeStates = new List<Enemy_InRangeState>();




    [Header("   Binah Lost State")]
    public bool useSearch;

    protected Enemy_Search SearchState;

    protected List<Enemy_LostState> LostStates = new List<Enemy_LostState>();

    #endregion

    [Space (20)]
    [Header ("Debug")]

    [SerializeField]
    protected bool displayCurrentState = true;




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




    protected void BaseAwake()
    {
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
       if (useChasing)
       {
            InSightStates.Add(ChasingState = new Enemy_Chasing());
            ChasingState.BaseManager = this;
       }
       if (useWarning)
       {
            InSightStates.Add(WarningState = new Enemy_Warning());
            WarningState.BaseManager = this;
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
        #endregion

        #region Lost State List
        if (useSearch)
        {
            LostStates.Add(SearchState = new Enemy_Search());
            SearchState.BaseManager = this;
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




    public abstract void HaveBeenWarned();

    public abstract void IsWarning();
}
