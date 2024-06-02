using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AII_StealthNeutralization : AI_Interaction
{
    public override void InteractionFailed()
    {
        Debug.Log("ActionRange Failed");
    }

    public override void InteractionSucceed()
    {

        Debug.Log("Action DONE");

        GameObject parent = transform.parent.gameObject;
        Enemy_BaseManager EnemyManager = parent.GetComponent<Enemy_BaseManager>();

        //EnemyManager.SwitchState(EnemyManager.)

        //Custom Fail Conditions
        #region
        switch (EnemyManager.CurrentState)
        {
            //Fail if in Attacking State
            case Enemy_AttackingState:

                //Exeption if in Warning State
                if (EnemyManager.CurrentState != EnemyManager.WarningState)
                {
                    Debug.Log("Neutralization Failed");
                    MakeInteractionFail();
                    return;
                }

                break;
        }
        #endregion

        print("MISSING ANIMATION - Neutralization / Neutralized");
        parent.GetComponent<Enemy_BaseManager>().Kill();
        EnemyManager.SwitchState(EnemyManager.NeutralizedState);


        //Succeed result
        Destroy(gameObject);
    }

    public override void TriggerEvent()
    {

    }
}
