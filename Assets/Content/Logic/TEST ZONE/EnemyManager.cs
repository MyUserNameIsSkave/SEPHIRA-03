using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;



    private DetectionTest[] detectionScripts;

    [Tooltip("The time between to update of the enemies view, dont take into account the systeme time requiere to update each enemies")]
    public float timeBeteenEnemyUpdate;


    //Valeur servant évaluer le score sur une seconde
    public float UpdateTime;


    public AnimationCurve LightMultiplierCurve;


    private void Awake()
    {
        Instance = this;
        detectionScripts = FindObjectsOfType<DetectionTest>();
    }


    private void Start()
    {
        StartCoroutine(UpdateEnemyDetection());
    }

    IEnumerator UpdateEnemyDetection()
    {
        while (true)
        {
            float startTime = Time.time;

            yield return new WaitForSeconds(timeBeteenEnemyUpdate);


            foreach (DetectionTest toUpdate in detectionScripts)
            {
                yield return new WaitForNextFrameUnit();
                toUpdate.CheckPosition();
            }

            UpdateTime = Time.time - startTime;
        }

    }
}


