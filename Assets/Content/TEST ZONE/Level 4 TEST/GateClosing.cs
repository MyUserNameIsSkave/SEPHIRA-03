using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateClosing : MonoBehaviour
{
    [SerializeField]
    private Animator gateAnimator, roomAnimation;

    [SerializeField]
    private CameraBase nextCamera;


    private void OnTriggerEnter(Collider other)
    {
        print("Enter0");
        StartCoroutine(CloseGate());
    }

    IEnumerator CloseGate()
    {

        GameManager.Instance.BinahManager.SendBinahToLocation(GameManager.Instance.Binah.transform.position);

        yield return new WaitForSeconds(2f);

        roomAnimation.SetTrigger("BinahPassed");

        yield return new WaitForSeconds(9f);

        gateAnimator.SetTrigger("BinahPassed");

        yield return new WaitForSeconds(3f);

        GameManager.Instance.CameraController.CurrentCamera = nextCamera;

    }
}
