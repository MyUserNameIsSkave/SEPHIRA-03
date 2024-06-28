using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phase2KeterAnimation : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private Animator coridor_animator;

    [SerializeField]
    private Animator[] coridor_movables;

    public Transform keter;

    public void TriggerEvent()
    {
        StartCoroutine(Triggered());
    }

    IEnumerator Triggered()
    {
        foreach (Animator animator in coridor_movables)
        {
            animator.SetTrigger("Start");
        }

        coridor_animator.SetTrigger("Coridor Animation");

        yield return new WaitForSeconds(0.4f);

        Vector3 targetPosition = keter.position;
        targetPosition.z -= -1.5f;

        GameManager.Instance.BinahManager.SendBinahToLocation(targetPosition);
    }
}
