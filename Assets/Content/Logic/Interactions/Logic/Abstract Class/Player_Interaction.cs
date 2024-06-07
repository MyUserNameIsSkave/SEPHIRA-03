using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Interaction : MonoBehaviour, IInteractable
{
    
    public int stamCost;

    [Tooltip("The max distance with Binah to ask for Interaction")]
    public float interactionDistance = 1000;

    protected PlayerStamina staminaScript;



    private void Start()
    {
        staminaScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStamina>();
    }


    /// <summary>
    /// Return True if the Player has enougth Stam for the Action.
    /// </summary>
    /// <returns></returns>
    protected bool CheckStamina()
    {
        if (staminaScript == null)
        {
            return false;
        }



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


    public bool CheckDistance()
    {
        if (Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position) <= interactionDistance)
        {
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
        if (CheckStamina() && CheckDistance())
        {
            Interaction();
            TriggerEvent();
        }
        else
        {
            Debug.Log("Action can't be Executed");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }

}
