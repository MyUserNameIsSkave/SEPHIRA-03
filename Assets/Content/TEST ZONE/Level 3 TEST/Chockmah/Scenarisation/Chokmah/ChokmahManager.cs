using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.iOS;

public class ChokmahManager : MonoBehaviour, IEventTriggerable
{

    [SerializeField]
    private GameObject Chokmah;

    private GameObject chokmahReference;

    public bool ShouldBeDestroyed;
    
    public ChokmahPath TargetPath;






    public void TriggerEvent()
    {
        

        if (chokmahReference != null)
        {
            Destroy(chokmahReference);
            chokmahReference = null;
        }

        chokmahReference = Instantiate(Chokmah, TargetPath.gameObject.transform.GetChild(0).transform.position, TargetPath.gameObject.transform.GetChild(0).transform.rotation);
        chokmahReference.GetComponent<ChokmahLogic>().patrolRoute = TargetPath;

        if (TargetPath.movementSpeed != 0)
        {
            chokmahReference.GetComponent<NavMeshAgent>().speed = TargetPath.movementSpeed;
        }


        TargetPath = null;
    }


}
