using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;



    [Tooltip("The time between to update of the enemies view, dont take into account the systeme time requiere to update each enemies")]
    public float timeBeteenEnemyUpdate;

    //The duration the system took to update all the enemies
    public float UpdateTime;

    public AnimationCurve LightMultiplierCurve;



    private DetectionTest[] detectionScripts;




    private GameObject binah;


    private void Awake()
    {
        Instance = this;
        detectionScripts = FindObjectsOfType<DetectionTest>();

        StartCoroutine(UpdateEnemyDetection());

    }


    IEnumerator UpdateEnemyDetection()
    {
        //Get the reference of Binah from the GameManager
        binah = GameManager.Instance.Binah;




        //Infinit Loop
        while (true)
        {
            float startTime = Time.time;


            yield return new WaitForSeconds(timeBeteenEnemyUpdate);


            // Update Each Ennemis
            foreach (DetectionTest toUpdate in detectionScripts)
            {
                //Prevent error with potential change in the pull of Enemies to Update
                if (!toUpdate)
                {
                    //Changes in the pull of Enemies to Update
                    detectionScripts = FindObjectsOfType<DetectionTest>();
                    continue;
                }

                //Check Distance
                if (Vector3.Distance(toUpdate.transform.position, binah.transform.position) > toUpdate.MaxViewDistance)
                {
                    //To far
                    continue;
                }


                yield return new WaitForNextFrameUnit();
                toUpdate.GetVisionScore();
            }


            UpdateTime = Time.time - startTime;
        }

    }
}


