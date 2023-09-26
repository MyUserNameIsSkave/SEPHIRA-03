using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractiveObject;

public class IO_Door : InteractiveObject
{
    // ----- VARIABLES -----

    private int multiplier = 1;




    [Space(15f)]
    [Header("     OBJECT ACTION")]
    [Space(7f)]




    [SerializeField]
    private bool inBaseState = true;


    private Consideration baseConsideration;

    [SerializeField]
    private Consideration alternativeConsideration;





    // ---------- LOGIC ----------

    private void Start()
    {
        baseConsideration = Consideration;
    }



    // ----- INTERFACE METHODS -----




    public override void Interaction()
    {
        transform.position = transform.position + transform.right * (transform.localScale.x - transform.localScale.x * 0.2f) * multiplier;
        
        multiplier = multiplier * -1;


        

        inBaseState = !inBaseState;




        //RECUPERER LA CONSIDERATION DE BASE ET L4ALTERNATIVE
        //Change the used consideration
        if (inBaseState)
        {
            Consideration = baseConsideration;
        }
        else
        {
            Consideration = alternativeConsideration;
        }

        

        ModifyStateVariables();
    }

    
}