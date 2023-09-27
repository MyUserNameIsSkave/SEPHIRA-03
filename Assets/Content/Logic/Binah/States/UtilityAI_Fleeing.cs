using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class UtilityAI_Fleeing : UtilityAI_BaseState
{
    NavMeshAgent agent;


    public override void EnterState()
    {
        agent = UtilityAI_Manager.Agent;
        CheckIfInDanger();

    }


    public override void ExitState()
    {
        UtilityAI_Manager.EnemyToFlee.Clear();        
        Debug.Log("ExitFlee");
    }




    public override void CustomUdpateState()
    {
        CheckIfInDanger();
    }

    public override void FixedUpdateState()
    {

    }

    public override void UpdateState()
    {
        ////DebugKey
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    GoThroughPossibilities();
        //}
    }





    //  ----- WORKING VARIABLES -----

    List<Vector3> allPointList = new List<Vector3>();
    List<Vector3> sortedPointList = new List<Vector3>();







    // ----- LOGIC -----


    /// <summary>
    /// Check if Fleeing is Still Relevant
    /// </summary>
    private void CheckIfInDanger()
    {




        //Get All Colliding Object
        Collider[] colliders = Physics.OverlapSphere(UtilityAI_Manager.Object.transform.position, UtilityAI_Manager.DetectEnemyToFleeRadius, LayerMask.GetMask("Enemy"));


        if (colliders.Length == 0)
        {
            UtilityAI_Manager.SwitchState(UtilityAI_Manager.IdleState);
        }

        UtilityAI_Manager.EnemyToFlee.Clear();

        foreach (Collider collider in colliders)
        {
            Vector3 enemyPosition = collider.gameObject.transform.position;
            Vector3 playerPosition = UtilityAI_Manager.Object.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(playerPosition, (enemyPosition - playerPosition).normalized, out hit))
            {
                //Direct Sight
                if (hit.collider.CompareTag("Enemy"))
                {
                    UtilityAI_Manager.EnemyToFlee.Add(collider.gameObject);
                }
                //Hiden but Too Close
                else if (Vector3.Distance(playerPosition, enemyPosition) <= UtilityAI_Manager.MinDistanceBeforeFlee)
                {
                    UtilityAI_Manager.EnemyToFlee.Add(collider.gameObject);
                }
                //No Need to Flee Anymore
                else
                {
                    UtilityAI_Manager.SwitchState(UtilityAI_Manager.IdleState);
                }
            }
        }


        //Start To Flee
        GoThroughPossibilities();
    }


    private void GoThroughPossibilities()
    {
        //Prevent Out of Range Error
        if (UtilityAI_Manager.EnemyToFlee.Count == 0)
        {
            UtilityAI_Manager.SwitchState(UtilityAI_Manager.IdleState);
            return;
        }



        //COVER

        AI_Interaction cover = SearchForCover();
        if (cover != null)
        {
                #region Debug
            if (UtilityAI_Manager.UseDebugTool && UtilityAI_Manager.DebugTool.logFlee)
            {
                Debug.Log("Can take Cover: " + cover.gameObject);
            }
                #endregion

            //Do Cover Action
            UtilityAI_Manager.ActionToDo = cover;
            UtilityAI_Manager.SwitchState(UtilityAI_Manager.DoingActionState);

            return;
        }


        //HIDING
        Vector3 hidePosition = SearchForHidingPosition();
        if (hidePosition != Vector3.zero)
        {
                #region Debug
            if (UtilityAI_Manager.UseDebugTool && UtilityAI_Manager.DebugTool.logFlee)
            {
                Debug.Log("Can Hide: " + hidePosition);
            }
                #endregion

            //Do Movement
            UtilityAI_Manager.Agent.SetDestination(hidePosition);
            return;
        }


        //FLEE
        Vector3 fleePosition = SearchForFleePosition();
        if (fleePosition != Vector3.zero)
        {
                #region Debug
            if (UtilityAI_Manager.UseDebugTool && UtilityAI_Manager.DebugTool.logFlee)
            {
                Debug.Log("Must Flee: " + fleePosition);
            }
                #endregion

            //Do Movement
            UtilityAI_Manager.Agent.SetDestination(fleePosition);

            return;

        }
    }







    //DONE

    #region COVER

    private AI_Interaction SearchForCover()
    {
        //Variables
        Vector3 playerPosition = UtilityAI_Manager.gameObject.transform.position;

        List<AI_Interaction> InteractionList = new List<AI_Interaction>();
        List<AI_Interaction> coverList = new List<AI_Interaction>();


        //Get all action in radius
        Collider[] colliders = Physics.OverlapSphere(playerPosition, UtilityAI_Manager.SearchCoverRadius, LayerMask.GetMask("InteractiveObject"));


        //Get each scripts derived from InteractiveObject to a List
        foreach (Collider collider in colliders)
        {
            AI_Interaction Interaction = collider.GetComponent<AI_Interaction>();
            if (Interaction != null)
            {
                InteractionList.Add(Interaction);
            }
        }



        //Get rid of non Cover Type Interaction
        foreach (var interactionScript in InteractionList)
        {
            if (UtilityAI_Manager.CoverConsideration == interactionScript.Consideration)
            {
                RaycastHit hit;

                bool wouldHideThePlayer = true;

                foreach (var enemy in UtilityAI_Manager.EnemyToFlee)
                {
                    if (Physics.Raycast(interactionScript.gameObject.transform.position, (enemy.transform.position - interactionScript.gameObject.transform.position).normalized, out hit))
                    {
                        if (hit.collider.CompareTag("Enemy"))
                        {
                            wouldHideThePlayer = false;
                        }
                    }
                }

                //Only Add Cover that "Hide" the AI
                if (wouldHideThePlayer)
                {
                    coverList.Add(interactionScript);
                }
            }
        }

        if (InteractionList.Count == 0)
        {
            return null;
        }

        //Get Closest Cover
        return GetClosestCover(coverList);

    }

    private AI_Interaction GetClosestCover(List<AI_Interaction> coverList)
    {
        NavMeshPath path = new NavMeshPath();
        float shortestPathLength = Mathf.Infinity;
        float currentPathLength = 0f;

        AI_Interaction finalCover = null;


        foreach (var coverScript in coverList)
        {
            if (agent.CalculatePath(coverScript.transform.position, path))
            {
                //Get Length
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    currentPathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                }

                //Compare Length
                if (currentPathLength < shortestPathLength)
                {
                    //Set new Shortest
                    shortestPathLength = currentPathLength;
                    finalCover = coverScript;
                }
            }
        }

        return finalCover;
    }

    #endregion



    //DONE

    #region HIDE

    private Vector3 SearchForHidingPosition()
    {
        //Variables
        Vector3 enemyPosition = UtilityAI_Manager.EnemyToFlee[0].transform.position;

        allPointList.Clear();
        sortedPointList.Clear();

        //Create Navigation Point List
        MakePointList();



        //Get Rid Non Hiding Position
        foreach (Vector3 point in allPointList)
        {
            bool wouldHideTheAI = true;

            foreach (var enemy in UtilityAI_Manager.EnemyToFlee)
            {
                


                RaycastHit hit;
                if (Physics.Raycast(point, (enemy.transform.position - point).normalized, out hit))
                {
                    if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("Player") || hit.collider.gameObject.CompareTag("AI_Interactable"))
                    {
                        wouldHideTheAI = false;
                    }
                }
            }

            if (wouldHideTheAI)
            {
                sortedPointList.Add(point);
            }


        }

        if (sortedPointList.Count == 0)
        {
            return Vector3.zero;
        }

        allPointList.Clear();


        //Only Keep  the Closest Point
        return GetClosestPoint();
    }

    private void MakePointList()
    {
        //Variables
        Vector3 playerPosition = UtilityAI_Manager.gameObject.transform.position;

        Vector3 rightDir = UtilityAI_Manager.Object.transform.right;
        Vector3 forwardDir = UtilityAI_Manager.Object.transform.forward;
        Vector3 stepRight = UtilityAI_Manager.SearchHideSpotRadius / UtilityAI_Manager.SearchDensity * rightDir;
        Vector3 stepForward = UtilityAI_Manager.SearchHideSpotRadius / UtilityAI_Manager.SearchDensity * forwardDir;
        Vector3 baseCorner = playerPosition + (-rightDir + forwardDir) * UtilityAI_Manager.SearchHideSpotRadius;
        float ajustedRadius = UtilityAI_Manager.SearchHideSpotRadius + UtilityAI_Manager.SearchHideSpotRadius / 10;



        for (int x = 0; x < UtilityAI_Manager.SearchDensity * 2 + 1; x++)
        {
            Vector3 point = (baseCorner + (stepRight * x));

            //Check for Each Enemy 
            foreach (var enemy in UtilityAI_Manager.EnemyToFlee)
            {

                if (Vector3.Distance(playerPosition, point) <= ajustedRadius && Vector3.Distance(enemy.transform.position, point) >= 1)
                {
                    allPointList.Add(point);
                }

                for (int y = 1; y < UtilityAI_Manager.SearchDensity * 2 + 1; y++)
                {
                    point = (baseCorner + (stepRight * x) - stepForward * y);

                    if (Vector3.Distance(playerPosition, point) <= ajustedRadius && Vector3.Distance(enemy.transform.position, point) >= 1)
                    {
                        allPointList.Add(point);
                    }
                }

            }
        }
    }

    private Vector3 GetClosestPoint()
    {
        NavMeshPath path = new NavMeshPath();
        float pathLength = Mathf.Infinity;

        Vector3 finalPosition = Vector3.zero;

        foreach (Vector3 point in sortedPointList)
        {
            if (agent.CalculatePath(point, path))
            {
                float currentPathLength = 0;

                //Add up each segments
                for (int i = 0; i < path.corners.Length - 1; i++)
                {
                    currentPathLength += Vector3.Distance(path.corners[i], path.corners[i + 1]);
                }

                if (currentPathLength < pathLength)
                {
                    pathLength = currentPathLength;
                    finalPosition = point;
                }
            }
        }
        return finalPosition;
    }

    #endregion



    //DONE

    #region FLEE

    private Vector3 SearchForFleePosition()
    {
        //Variables
        Vector3 playerPosition = UtilityAI_Manager.gameObject.transform.position;
        Vector3 averageDirection = Vector3.zero;
        Vector3 flatAverageDirection = Vector3.zero;
        Vector3 finalPosition = Vector3.zero;



        //Check if no collision


        //Get the Average Flee Direction
        foreach (var enemy in UtilityAI_Manager.EnemyToFlee)
        {
            averageDirection = (averageDirection + (playerPosition - enemy.transform.position).normalized);
        }

        //Get the Direction Ignoring the Vertical Axis
        flatAverageDirection = new Vector3(averageDirection.x, 0, averageDirection.z);



        RaycastHit hit;

        //Check for Wall Collision
        if (Physics.Raycast(playerPosition, flatAverageDirection, out hit, UtilityAI_Manager.SearchHideSpotRadius))
        {
            foreach (var enemy in UtilityAI_Manager.EnemyToFlee)
            {
                if (Vector3.Distance(hit.point, enemy.transform.position) > 5)
                {
                    finalPosition = hit.point;
                }
            }
        }

        //Check for a Point in an Open Space
        else
        {
            finalPosition = playerPosition + flatAverageDirection * UtilityAI_Manager.FleeDistance;
        }


        return finalPosition;
    }

    #endregion
}
