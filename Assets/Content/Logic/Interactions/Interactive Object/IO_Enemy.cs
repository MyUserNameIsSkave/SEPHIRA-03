using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IO_Enemy : InteractiveObject
{
    // ----- INTERFACE METHODS -----



    public override void Interaction()
    {
        //Disable behavior
        transform.GetChild(1).gameObject.SetActive(false);
        gameObject.GetComponent<NavMeshAgent>().enabled = false;

        gameObject.GetComponent<Enemy_Patrol>().enabled = false;
        gameObject.GetComponent<Enemy_Patrol>().StopAllCoroutines();

        gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;

        transform.Rotate(new Vector3(0, 0, -90));
        transform.Translate(new Vector3(0.5f, 0.5f, -0));
        transform.GetChild(0).gameObject.SetActive(false);

        transform.GetChild(3).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

        ModifyStateVariables();
    }

}
