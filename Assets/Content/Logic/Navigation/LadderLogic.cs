using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;


public class LadderLogic : MonoBehaviour
{
    //Settings
    public float climbingSpeed;

    //References
    private NavMeshLink link;
    NavMeshAgent agent;


    //Working Variables
    private Vector3 startPoint;
    private Vector3 endPoint;

    float baseAgentSpeed;
    bool inUse = false;








    //// ---------- LOGIC ----------



    private void Start()
    {
        //Self reference
        link = GetComponent<NavMeshLink>();

        //World position of the points
        startPoint = link.startPoint + transform.position;
        endPoint = link.endPoint + transform.position;
    }




    #region Collisions
    private void OnTriggerEnter(Collider other)
    {
        NavMeshAgent _agent = other.GetComponent<NavMeshAgent>();

        if (_agent)
        {
            agent = _agent;

            if (agent.speed != climbingSpeed)
            {
                baseAgentSpeed = agent.speed;
            }

            StartCoroutine(CheckForUse());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == agent.gameObject)
        {
            if (!inUse)
            {
                StopAllCoroutines();
            }
            return;
        }
    }
    #endregion




    IEnumerator CheckForUse()
    {
        if (agent.isOnOffMeshLink)
        {
            //Check for Start 
            StartUsing();

            //Adjust rotation to face the ladder
            agent.gameObject.transform.rotation = gameObject.transform.rotation * Quaternion.Euler(0, 180, 0);
        }
        else if (inUse)
        {
            //Check for End
            EndUsing();
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckForUse());
    }




    /// <summary>
    /// Do what needs to be done when an Agent Start using the Link
    /// </summary>
    private void StartUsing()
    {
        if (!inUse)
        {
            inUse = true;
            agent.speed = climbingSpeed;
            agent.GetComponent<Animator>().SetBool("ClimbingLader", true);


            if (Vector3.Distance(agent.transform.position, startPoint) > Vector3.Distance(agent.transform.position, endPoint))
            {
                //Going Up
                agent.GetComponent<Animator>().SetFloat("Ladder Start Position", 0f);

            }
            else
            {
                //Going Up
                agent.GetComponent<Animator>().SetFloat("Ladder Start Position", 1f);
            }
        }
    }


    /// <summary>
    /// What needs to be done when the Agent Ends using the Link
    /// </summary>
    private void EndUsing()
    {
        agent.GetComponent<Animator>().SetBool("ClimbingLader", false);
        agent.speed = baseAgentSpeed;
        inUse = false;
    }

}
