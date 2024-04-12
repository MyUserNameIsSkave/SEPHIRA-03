using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteruptMovement : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.BinahManager.SendBinahToLocation(GameManager.Instance.Binah.gameObject.transform.position);
        Destroy(gameObject);
    }
}
