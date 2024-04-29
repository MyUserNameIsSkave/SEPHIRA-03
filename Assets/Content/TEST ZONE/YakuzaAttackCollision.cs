using GD.MinMaxSlider;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;


public class YakuzaAttackCollision : MonoBehaviour
{
    Enemy_BaseManager enemyManager;

    [SerializeField] 
    private PI_StrugglingPoint[] strugglingPoints;

    private List<PI_StrugglingPoint> strugglingPointsToActivate = new List<PI_StrugglingPoint>();

    private List<PI_StrugglingPoint> activeStrugglingPoints = new List<PI_StrugglingPoint>();


    [SerializeField, MinMaxSlider(1, 4)]
    private Vector2 strugglingDurationRange;

    [SerializeField, MinMaxSlider(1, 4)]
    private Vector2Int strugglePointsRange;



    private float strugglingDuration;
    private int numberOfStrugglePointsToActivate;


    private List<int> strugglePointsToActivateIndexes = new List<int>();


    private void Awake()
    {

        strugglingDuration = UnityEngine.Random.RandomRange(strugglingDurationRange.x, strugglingDurationRange.y);      //Ne pas toucher
        numberOfStrugglePointsToActivate = UnityEngine.Random.RandomRange(strugglePointsRange.x, strugglePointsRange.y);      //Ne pas toucher




        // loop until we have selected the desired number of objects
        while (strugglePointsToActivateIndexes.Count < numberOfStrugglePointsToActivate)
        {
            // generate a random index within the range of the array
            int randomIndex = UnityEngine.Random.RandomRange(0, strugglingPoints.Length);

            // if the index has not already been selected, add it to the list
            if (!strugglePointsToActivateIndexes.Contains(randomIndex))
            {
                strugglePointsToActivateIndexes.Add(randomIndex);
            }
        }




        enemyManager = transform.parent.GetComponent<Enemy_BaseManager>();

        foreach (PI_StrugglingPoint point in strugglingPoints)
        {
            point.gameObject.GetComponent<MeshRenderer>().enabled = false;
            point.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {




        if (enemyManager.CurrentState == enemyManager.NeutralizationState)
        {
            print("NUTRALIZE");
            enemyManager.SwitchState(enemyManager.StrugglingState);


            foreach (int index in strugglePointsToActivateIndexes)
            {
                PI_StrugglingPoint point = strugglingPoints[index];

                activeStrugglingPoints.Add(point);
                point.gameObject.GetComponent<MeshRenderer>().enabled = true;
                point.gameObject.GetComponent<SphereCollider>().enabled = true;
            }



            //foreach (PI_StrugglingPoint point in strugglingPoints)
            //{
            //    activeStrugglingPoints.Add(point);
            //    point.gameObject.GetComponent<MeshRenderer>().enabled = true;
            //    point.gameObject.GetComponent<SphereCollider>().enabled = true;
            //}


            StartCoroutine(Strugglingtimer());
        }
    }


    IEnumerator Strugglingtimer()
    {
        float startTime = Time.time;
        bool isWining = false;

        while (Time.time - startTime < strugglingDuration)
        {
            isWining = true;

            if (activeStrugglingPoints.Count == 0)
            {
                Debug.LogError("The active Struggle Point List is Empty");
            }

            foreach (PI_StrugglingPoint point in activeStrugglingPoints)
            {
                if (!point.triggered)
                {
                    isWining = false;
                }
            }

            if (isWining)
            {
                enemyManager.Kill();
                yield break;
            }

            yield return null;
        }


        if (enemyManager.CurrentState == enemyManager.StrugglingState)
        {
            enemyManager.StrugglingState.YakuzaWin();
        }
    }
}
