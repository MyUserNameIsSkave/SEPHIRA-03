using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public abstract class AI_Interaction : MonoBehaviour, IInteractable
{
    // ----- SETTINGS VARIABLES -----

    [Header("     BASE SETTINGS")]
    [Space(7f)]


    [Tooltip ("The Stamina Cost of the action to the Player")]
    public int stamCost;

    [Tooltip ("The Scriptabel Object that translate State Variables into Score")]
    public Consideration Consideration;

    [Tooltip ("The distance from the center at witch the action can be executed by the AI")]
    public float ActionRange;



    [Space(15f)]
    [Header("     ACTION CONSEQUENCES")]
    [Space(7f)]

    public ModificationType modificationType;
    public enum ModificationType
    {
        None,
        Add,
        Set
    }

    public StateVariablesModification variablesModification;



    [Space (15)]
    [Header ("     FORCED ACTION")]
    [Space(7)]



    [Header("SETTINGS")]
    [Space(7)]

    [Tooltip ("Number of Request needed to Force an Action")]
    public int askedTimeToForce;

    [Tooltip("Time a Request stay before not counting anymore ")]
    public float RefusalLastingTime;

    [HideInInspector]
    public int timeRefused;


    [Space(15)]
    [Header("     FORCED ACTION CONSEQUENCES")]
    [Space(7)]

    [Tooltip ("Add this to State Variables in case of Forced Action")]
    public StateVariablesModification forcedActionVariablesModification;




    [HideInInspector]
    public Vector2 ScoreRange;
    protected UtilityAI_Manager UtilityAI_Manager;





    protected PlayerStamina staminaScript;



    private void Awake()
    {
        staminaScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina>();
    }


    //References and get Variables
    private void OnEnable()
    {
        UtilityAI_Manager = GameManager.Instance.BinahManager;

        ScoreRange = Consideration.ScoreRange;
    }



    public void SelectedByPlayer()
    {
        //Transfert Input
        UtilityAI_Manager.DoIndicatedAction(this);
    }






    /// <summary>
    /// Methode contenant toute la logique nécessaire à l'execution de l'action
    /// </summary>
    public abstract void InteractionSucceed();

    public abstract void InteractionFailed();

    public abstract void TriggerEvent();


    public void Interaction()
    {
        if (CheckStamina())
        {
            InteractionSucceed();
        }
        else
        {
            InteractionFailed();
        }
    }

    
    public void MakeInteractionFail()
    {
        InteractionFailed();
    }



    /// <summary>
    /// Return True if the Player has enought Stma for the Action.
    /// </summary>
    /// <returns></returns>
    protected bool CheckStamina()
    {
        if (stamCost <= staminaScript.CurrentStam)
        {
            staminaScript.CurrentStam -= stamCost;
            return true;
        }
        else
        {
            return false;
        }
    }







    /// <summary>
    /// Method to use in order to modify the State Variables Values
    /// </summary>
    public void ModifyStateVariables()
    {
        if (modificationType == ModificationType.None)
        {
            //None
            return;
        }

        if (modificationType == ModificationType.Add)
        {
            //Add
            UtilityAI_Manager.StateVariablesDictionnary.AddStateVariables(variablesModification.Courage, 
                                                                          variablesModification.Determination, 
                                                                          variablesModification.Curiosity, 
                                                                          variablesModification.Independence);

        }

        if (modificationType == ModificationType.Set)
        {
            //Set
            UtilityAI_Manager.StateVariablesDictionnary.SetStateVariables(variablesModification.Courage,
                                                                          variablesModification.Determination,
                                                                          variablesModification.Curiosity,
                                                                          variablesModification.Independence);
        }
    }




    public void ActionRefused(AI_Interaction action)
    {
        StartCoroutine(RefusalStacking(action));
    }



    public IEnumerator RefusalStacking(AI_Interaction action)
    {
        timeRefused = Mathf.Clamp(timeRefused + 1, 0, askedTimeToForce);

        if (timeRefused >= askedTimeToForce)
        {
            yield return new WaitForEndOfFrame();

            //Force Execute
            ForcedActionConsequences();
            UtilityAI_Manager.SwitchState(UtilityAI_Manager.IdleState);
            UtilityAI_Manager.IdleState.ScoreIndicatedAction(action);
        }


        yield return new WaitForSeconds(RefusalLastingTime);

        timeRefused = Mathf.Clamp(timeRefused - 1, 0, askedTimeToForce);
    }




    private void ForcedActionConsequences()
    {
        //Add
        UtilityAI_Manager.StateVariablesDictionnary.AddStateVariables(forcedActionVariablesModification.Courage,
                                                                      forcedActionVariablesModification.Determination,
                                                                      forcedActionVariablesModification.Curiosity,
                                                                      forcedActionVariablesModification.Independence);
    }



}





