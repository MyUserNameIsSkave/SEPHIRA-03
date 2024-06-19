using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DialogueSkipAnimation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(TeleportBinah());
    }


    IEnumerator TeleportBinah()
    {
        GameManager.Instance.Binah.GetComponent<NavMeshAgent>().enabled = false;
        GameManager.Instance.Binah.transform.position = transform.GetChild(0).position;

        yield return new WaitForEndOfFrame();

        GameManager.Instance.Binah.GetComponent<NavMeshAgent>().enabled = true;
    }
}
