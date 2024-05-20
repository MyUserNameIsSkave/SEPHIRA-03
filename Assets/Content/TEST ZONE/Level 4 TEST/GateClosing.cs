using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateClosing : MonoBehaviour
{
    [SerializeField]
    private Animator GateAnimator, RoomAnimation;

    private void OnTriggerEnter(Collider other)
    {
        print("Enter0");
        StartCoroutine(CloseGate());
    }

    IEnumerator CloseGate()
    {

        GameManager.Instance.BinahManager.SendBinahToLocation(GameManager.Instance.Binah.transform.position);

        yield return new WaitForSeconds(2f);

        RoomAnimation.SetTrigger("BinahPassed");

        yield return new WaitForSeconds(9f);

        GateAnimator.SetTrigger("BinahPassed");
    }
}
