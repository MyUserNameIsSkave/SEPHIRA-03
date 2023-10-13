using UnityEngine;


public class LadderCollision : MonoBehaviour
{
    private LadderLogic logicScript;


    private void Awake()
    {
        logicScript = transform.parent.GetComponent<LadderLogic>();
    }


    private void OnTriggerEnter(Collider other)
    {
        logicScript.TriggerEnter(other);



    }

    private void OnTriggerExit(Collider other)
    {
        logicScript.TriggerExit(other);
    }

}
