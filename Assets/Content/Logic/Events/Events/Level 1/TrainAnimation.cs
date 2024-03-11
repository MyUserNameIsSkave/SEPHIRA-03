using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAnimation : MonoBehaviour, IEventTriggerable
{

    [SerializeField]
    private float trainSpeed;

    [SerializeField]
    private float timeBeforeDestruction;


    private bool canMove = false;

    public void TriggerEvent()
    {
        canMove = true;
        Invoke("DestroyTrain", timeBeforeDestruction);
    }


    void Update()
    {
        if (!canMove)
        {
            return;
        }

        transform.Translate(-Vector3.forward * trainSpeed * Time.deltaTime);
    }


    private void DestroyTrain()
    {
        Destroy(gameObject);
    }

}
