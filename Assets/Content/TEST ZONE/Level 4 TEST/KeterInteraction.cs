using System.Collections;
using System.Collections.Generic;
using UnityEditor.Recorder.Input;
using UnityEngine;

public class KeterInteraction : Player_Interaction
{
    [Space (20)]

    [SerializeField]
    private Animator room_animator;

    [SerializeField]
    private Animator coridor_animator;

    [SerializeField]
    private Animator[] coridor_movables;

    [SerializeField]
    private Transform binahTargetPosition;

    [SerializeField]
    private CameraBase playerTargetCamera;

    [SerializeField]
    private GameObject gate;



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
        GameManager.Instance.Binah.SetActive(false);
        GameManager.Instance.Binah.transform.position = binahTargetPosition.position;
        GameManager.Instance.BinahManager.SendBinahToLocation(binahTargetPosition.position);
        GameManager.Instance.Binah.SetActive(true);

        room_animator.SetTrigger("SecondPhaseStart");

        yield return new WaitForSeconds(0.5f);

        foreach (Animator animator in coridor_movables)
        {
            animator.SetTrigger("Start");
        }

        GameManager.Instance.CameraController.CurrentCamera = playerTargetCamera;
        Destroy(gate);

        yield return new WaitForSeconds(1f);

        coridor_animator.SetTrigger("Coridor Animation");

        yield return new WaitForSeconds(0.5f);

        GameManager.Instance.BinahManager.SendBinahToLocation(transform.position);
    }


}
