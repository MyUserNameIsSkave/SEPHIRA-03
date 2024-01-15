using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_WarningState : Enemy_AttackingState
{
    public override void EnterState()
    {
        BaseManager.StopAgent();


        // Obtenir tous les colliders dans la sphère de détection
        Collider[] colliders = Physics.OverlapSphere(BaseManager.transform.position, BaseManager.WarningRadius, LayerMask.GetMask("Enemy"));


        foreach (Collider collider in colliders)
        {

            if (collider.gameObject.TryGetComponent(out IWarnable WarnableInterface))
            {
                WarnableInterface.GetWarned(BaseManager.transform.position);

                Debug.DrawLine(BaseManager.transform.position, collider.transform.position, Color.yellow, 5f);
            }
        }

        Debug.Log("Enemies are Warned");

        BaseManager.StartWarning();
    }



   




    public override void ExitState()
    {
        
    }





    public override void AwakeState()
    {

    }

    public override void StartState()
    {

    }
    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {

    }
}
