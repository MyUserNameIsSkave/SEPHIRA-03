using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_trigger_event : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour eventToTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Binah"))
            return;

        if (eventToTrigger is IEventTriggerable eventInterface)
        {
            eventInterface.TriggerEvent();
            Destroy(gameObject);
        }
    }
}
