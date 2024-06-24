using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
public class ChokmahManager : MonoBehaviour, IEventTriggerable
{

    [SerializeField]
    private GameObject Chokmah;

    [HideInInspector]
    public GameObject chokmahReference;

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

            if (TargetPath.movementSpeed > 5)
            {
                chokmahReference.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Run");
            }
        }


        TargetPath = null;
    }


}
