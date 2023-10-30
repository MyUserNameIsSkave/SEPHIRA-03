using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Enemy_BaseManager : MonoBehaviour
{
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

    protected Enemy_BaseState _currentState;


    // NEED TO FIND A WAY TO CHOSE A BASE STATE



    protected void BaseAwake()
    {
        //   Instanciate needed State Scritps

        #region Neutral State List
        if (useIdle)
        {
            NeutralStates.Add(IdleState = new Enemy_Idle());
            IdleState.BaseManager = this;
        }
        if (usePatrol)
        {
            NeutralStates.Add(PatrolState = new Enemy_Patrol());
            PatrolState.BaseManager = this;
        }
        if (useGettingCloser)
        {
            NeutralStates.Add(GettingCloserState = new Enemy_GettingCloser());
            GettingCloserState.BaseManager = this;
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

        SwitchState(_currentState);
    }




    protected void BaseUpdate()
    {
        if (_currentState == null)
        {
            return;
        }
        
        _currentState.UpdateState();
    }




    protected void BaseFixedUpdate()
    {
        if (_currentState == null)
        {
            return;
        }
        _currentState.FixedUpdateState();
    }




    /// <summary>
    /// Methode to call in order to Switch State. Reference the New State using State Manager variable.
    /// </summary>
    public void SwitchState(Enemy_BaseState newState)
    {
        if (newState == null)
        {
            print("Null State");
            return;
        }

        //Notify Old State if Different
        if (newState != _currentState)
        {
            if (_currentState != null)
            {
                _currentState.ExitState();
            }
        }

        //Set New State
        _currentState = newState;

        //Notify New State
        newState.EnterState();
    }
}
