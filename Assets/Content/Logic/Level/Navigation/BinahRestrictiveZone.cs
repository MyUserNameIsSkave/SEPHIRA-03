using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinahRestrictiveZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Binah"))
        {
            GameManager.Instance.BinahManager.InRestrictiveZone = true;

            if (GameManager.Instance.BinahManager.IsAutomaticalyMovingToCamera == true)
            {
                GameManager.Instance.BinahManager.SendBinahToLocation(GameManager.Instance.Binah.transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Binah"))
        {
            GameManager.Instance.BinahManager.InRestrictiveZone = false;
        }
    }

}
