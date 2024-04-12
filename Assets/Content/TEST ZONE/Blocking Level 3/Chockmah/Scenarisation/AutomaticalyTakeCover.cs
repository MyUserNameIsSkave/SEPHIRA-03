using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class AutomaticalyTakeCover : MonoBehaviour
{
    [Header("   GENERAL")]
    [Space(10)]

    [SerializeField]
    private CameraBase associatedEventCamera;


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
