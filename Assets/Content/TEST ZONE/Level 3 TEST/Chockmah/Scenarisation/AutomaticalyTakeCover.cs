using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutomaticalyTakeCover : MonoBehaviour
{
    [Header("   GENERAL")]
    [Space(10)]

    [SerializeField]
    private bool singleUse = false;

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

    private bool usedAlready = false;


    //WORKING VARIABLES


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

        
        if (!usedAlready || !singleUse)
        {
            TakeCover();
            StartCoroutine(TriggerChockmah());
            usedAlready = true;
        }

    }

    private bool chokmahTriggered = false;



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
        if (chokmahTriggered)
        {
            yield break;
        }

        while (associatedEventCamera != GameManager.Instance.CameraController.CurrentCamera)
        {
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(eventDelay);


        chokmahManager.TargetPath = targetPath;
        IEventTriggerable eventInterface = chokmahManager.GetComponent<IEventTriggerable>();
        eventInterface.TriggerEvent();

        chokmahTriggered = true;

        yield return 0;
    }

}
