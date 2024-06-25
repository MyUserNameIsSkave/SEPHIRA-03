using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeterInteraction : Player_Interaction
{
    [Space (20)]

    [SerializeField]
    private Animator room_animator;

    [SerializeField]
    private Transform binahTargetPosition;

    [SerializeField]
    private CameraBase playerTargetCamera;

    [SerializeField]
    private GameObject gate;

    [HideInInspector]
    public bool secondPhase;

    [SerializeField]
    private float delayBeforeTransition;

    public MonoBehaviour pushedDialogue;


    public override void Interaction()
    {
        StartCoroutine(SecondPhaseTransition());
    }


    public override void TriggerEvent()
    {
        //throw new System.NotImplementedException();
    }



    IEnumerator SecondPhaseTransition()
    {
        secondPhase = true;


        yield return new WaitForSeconds(delayBeforeTransition);


        GameManager.Instance.Binah.SetActive(false);
        GameManager.Instance.Binah.transform.position = binahTargetPosition.position;
        GameManager.Instance.BinahManager.SendBinahToLocation(binahTargetPosition.position);
        GameManager.Instance.Binah.SetActive(true);

        room_animator.SetTrigger("SecondPhaseStart");

        yield return new WaitForSeconds(1.2f);



        GameManager.Instance.CameraController.CurrentCamera = playerTargetCamera;
        Destroy(gate);

        IEventTriggerable eventInterface = pushedDialogue.GetComponent<IEventTriggerable>();
        eventInterface.TriggerEvent();
        
    }


}
