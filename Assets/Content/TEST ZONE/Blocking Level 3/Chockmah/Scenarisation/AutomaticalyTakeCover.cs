using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutomaticalyTakeCover : MonoBehaviour
{
    [Header("   GENERAL")]
    [Space(10)]

    [SerializeField]
    private CameraBase associatedEventCamera;

    [SerializeField]
    private float eventDelay;


    [Header("   BINAH")]
    [Space (10)]

    [SerializeField]
    private Transform CoverPoint;

    [Space(20)]
    [Header("   CHOCKMAH")]
    [Space(10)]

    [SerializeField]
    private GameObject chokmahPrefab;

    [SerializeField]
    private Transform[] targetPositions;




    //WORKING VARIABLES

    private bool AlreadyTriggered;

    private GameObject chokmahReference;

    private UtilityAI_Manager binah;




    private void Awake()
    {
        binah = GameManager.Instance.BinahManager;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Binah"))
        {
            return;
        }

        if (!AlreadyTriggered)
        {
            TakeCover();
        }
    }





    private void TakeCover()
    {
        //Crouch
        binah.Crouching(true);

        //Move To
        binah.SendBinahToLocation(CoverPoint.position);
    }


    IEnumerator TriggerChockmah()
    {
        //Spawn

        //Loop Movement
        //  Wait

        //Destroy



        yield return 0;
    }

}
