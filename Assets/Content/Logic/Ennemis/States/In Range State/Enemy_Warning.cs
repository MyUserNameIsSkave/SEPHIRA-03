using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Warning : Enemy_InRangeState
{
    public override void EnterState()
    {
        BaseManager.MoveAgentTo(BaseManager.transform.position);
        BaseManager.agent.speed = 0;


        // Obtenir tous les colliders dans la sphère de détection
        Collider[] colliders = Physics.OverlapSphere(BaseManager.transform.position, BaseManager.WarningRadius);


        foreach (Collider collider in colliders)
        {        

            if (collider.gameObject.TryGetComponent(out IWarnable WarnableInterface))
            {
                WarnableInterface.GetWarned(BaseManager.transform.position);

                Debug.DrawLine(BaseManager.transform.position, collider.transform.position, Color.yellow, 5f);
            }
        }

        Debug.Log("ENEMIES ARE WARNED");
    }







    public override void ExitState()
    {

    }

    public override void FixedUpdateState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }



    #region Useless
    public override void HeardSuspectNoise()
    {
        //Debug.Log(BaseManager.gameObject.name + " Heard Something");
    }

    public override void SeenSuspectThing()
    {
        //Debug.Log(BaseManager.gameObject.name + " Seen Something");

    }

    public override void DetectedBinah()
    {
        //Debug.Log(BaseManager.gameObject.name + " Detected Binah");

    }

    public override void LostBinah()
    {
        //Debug.Log(BaseManager.gameObject.name + " Lost Binah");

    }
    #endregion
}
