using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Interaction : MonoBehaviour, IInteractable
{
    
    public int stamCost;

    protected PlayerStamina staminaScript;



    private void Awake()
    {
        staminaScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina>();
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




    public abstract void Interaction();
    public abstract void TriggerEvent();



    public void SelectedByPlayer()
    {
        if (CheckStamina())
        {
            Interaction();
            TriggerEvent();
        }
        else
        {
            Debug.Log("Action can't be Executed");
        }
    }



}
