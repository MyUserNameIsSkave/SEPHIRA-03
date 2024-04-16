using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
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
    private ChokmahManager chokmahManager;

    [SerializeField]
    private ChokmahPath targetPath;




    //WORKING VARIABLES

    private bool AlreadyTriggered;
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
            StartCoroutine(TriggerChockmah());
        }

    }





    private void TakeCover()
    {
        if (CoverPoint == null)
        {
            return;
        }

        //Crouch
        binah.Crouching(true);

        //Move To
        binah.SendBinahToLocation(CoverPoint.position);
    }





    IEnumerator TriggerChockmah()
    {
        while (associatedEventCamera != GameManager.Instance.CameraController.CurrentCamera)
        {
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(eventDelay);


        chokmahManager.TargetPath = targetPath;
        IEventTriggerable eventInterface = chokmahManager.GetComponent<IEventTriggerable>();
        eventInterface.TriggerEvent();

        yield return 0;
    }

}
