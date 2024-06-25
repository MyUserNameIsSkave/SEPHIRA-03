using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteruptMovement : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour dialogue;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Binah"))
        {
            GameManager.Instance.BinahManager.SendBinahToLocation(GameManager.Instance.Binah.gameObject.transform.position);

            if (dialogue != null)
            {
                IEventTriggerable EvenTriggerInterface = dialogue.GetComponent<IEventTriggerable>();
                EvenTriggerInterface.TriggerEvent();
            }


            Destroy(gameObject);
        }
    }
}
