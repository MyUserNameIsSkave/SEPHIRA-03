using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// The AI do what it needs to Execute an Action
/// </summary>
public class UtilityAI_DoingAction : UtilityAI_BaseState
{
    // ----- WORKING VARIABLES -----

    private AI_Interaction action;

    //For the custom behavior in case of Box Collider
    private BoxCollider actionCollider;




    // ----- INHERITED METHODS -----

    public override void EnterState()
    {
        action = UtilityAI_Manager.ActionToDo;
        ActionChosed(action);
    }

    public override void ExitState()
    {

    }




    public override void CustomUdpateState()
    {

    }

    public override void FixedUpdateState()
    {
        ExecuteActionWhenPossible();
    }

    public override void UpdateState()
    {

    }




    // ----- CUSTOM METHODES -----



    //Check Condition to Execute Action
    private void ExecuteActionWhenPossible()
    {
        if (action == null)
        {
            Debug.LogWarning("Action null");
            return;
        }

        if (CheckForRange())
        {
            ExecuteAction();
            action = null;
        }
    }




    /// <summary>
    /// Action Initialization Method
    /// </summary>
    public void ActionChosed(AI_Interaction _action)
    {
        action = _action;
        actionCollider = action.gameObject.GetComponent<BoxCollider>();


        if (CheckForRange())
        {
            ExecuteAction();
            ResetVariables();
            return;
        }



        MoveToAction(action.transform.position);
    }




    /// <summary>
    /// Check if In Range for Action
    /// </summary>
    private bool CheckForRange()
    {
        Vector3 targetPosition;

        if (actionCollider != null)
        {
            targetPosition = Physics.ClosestPoint(UtilityAI_Manager.Object.transform.position, actionCollider, actionCollider.transform.position, actionCollider.transform.rotation);
        }
        else
        {
            targetPosition = action.transform.position;
        }

        return Vector3.Distance(targetPosition, UtilityAI_Manager.Object.transform.position) <= action.ActionRange ? true : false;
    }




    /// <summary>
    /// Move to the Center of the Action OR to the Closest Point for Action with Box Collider
    /// </summary>
    private void MoveToAction(Vector3 target)
    {
        if (actionCollider != null)
        {
            Vector3 closestPoint = Physics.ClosestPoint(UtilityAI_Manager.Object.transform.position, actionCollider, actionCollider.transform.position, actionCollider.transform.rotation);
            target = closestPoint;
        }
        
        
        UtilityAI_Manager.Agent.SetDestination(target);
        
    }




    /// <summary>
    /// Execute Action 
    /// </summary>
    private void ExecuteAction()
    {
        action.Interaction();
        action.TriggerEvent();
        UtilityAI_Manager.Agent.SetDestination(UtilityAI_Manager.Object.transform.position);       //Stop AI Movement
        ResetVariables();
    }




    /// <summary>
    /// Reset the Variables to prepare for the Next Action
    /// </summary>
    public void ResetVariables()
    {
        UtilityAI_Manager.ActionToDo = null;
        actionCollider = null;

        //Change State
        UtilityAI_Manager.SwitchState(UtilityAI_Manager.IdleState);
    }

}
