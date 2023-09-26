using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IO_Cover : InteractiveObject
{
    // ----- INTERFACE METHODS -----


    private GameObject player;
    private Rigidbody rb;
    private NavMeshAgent agent;

    private BoxCollider coverCollider;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
        agent = player.GetComponent<NavMeshAgent>();
        coverCollider = gameObject.GetComponent<BoxCollider>();
    }




    public override void Interaction()
    {

        UtilityAI_Manager.isHidenBehindCover = true;

        agent.speed = 4;
        player.transform.localScale = new Vector3 (1, 0.8f, 1);

        ////Disable Collision
        rb.detectCollisions = false;

        StartCoroutine(CheckForState());
    }






    /// <summary>
    /// Check if should leave cover 
    /// </summary>
    private IEnumerator CheckForState()
    {
        Vector3 closestPoint = Physics.ClosestPoint(player.transform.position, coverCollider, coverCollider.transform.position, coverCollider.transform.rotation);


        if (Vector3.Distance(closestPoint, player.transform.position) >= 1)
        {
            agent.speed = 7;
            player.transform.localScale = new Vector3(1, 1, 1);
            player.transform.localPosition = new Vector3(player.transform.localPosition.x, 1, player.transform.localPosition.z);

            //Enable Collision Back
            rb.detectCollisions = true;

            UtilityAI_Manager.isHidenBehindCover = false;
            StopAllCoroutines();

        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(CheckForState());

        }

    }
}


















