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
        GameObject parent = transform.parent.gameObject;
        Enemy_BaseManager EnemyManager = parent.GetComponent<Enemy_BaseManager>();

        //Custom Fail Conditions
        #region
        switch (EnemyManager.CurrentState)
        {
            //Fail if in Attacking State
            case Enemy_AttackingState:

                //Exeption if in Warning State
                if (EnemyManager.CurrentState != EnemyManager.WarningState)
                {
                    MakeInteractionFail();
                    return;
                }

                break;
        }
        #endregion


        //Deactivate Enemy
        parent.GetComponent<NavMeshAgent>().enabled = false;
        parent.GetComponent<Enemy_AudioDetection>().enabled = false;
        parent.GetComponent<Enemy_VisualDetection>().enabled = false;
        parent.GetComponent<CapsuleCollider>().enabled = false;

        EnemyManager.enabled = false;

        parent.transform.Translate(new Vector3(0, -0.8f, 0));
        parent.transform.Rotate(new Vector3(0, 0, -90));
        parent.transform.Translate(new Vector3(0, 0.7f, 0));


        //Succeed result
        Destroy(gameObject);
    }

    public override void TriggerEvent()
    {

    }
}
