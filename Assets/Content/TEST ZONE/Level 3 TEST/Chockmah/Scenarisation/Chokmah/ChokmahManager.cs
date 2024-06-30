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
    private CapsuleCollider chokmahCollision;

    public bool ShouldBeDestroyed;
    
    public ChokmahPath TargetPath;


    private float delayTime = 2f; 



    public void TriggerEvent()
    {
        

        if (chokmahReference != null)
        {
            Destroy(chokmahReference);
            chokmahReference = null;
            
            
        }

        if (chokmahReference == null)
        {
            chokmahReference = Instantiate(Chokmah, TargetPath.gameObject.transform.GetChild(0).transform.position, TargetPath.gameObject.transform.GetChild(0).transform.rotation);
            chokmahReference.GetComponent<ChokmahLogic>().patrolRoute = TargetPath;
        }
        if (TargetPath.movementSpeed != 0)
        {
            chokmahReference.GetComponent<NavMeshAgent>().speed = TargetPath.movementSpeed;

            if (TargetPath.movementSpeed > 5)
            {
                chokmahReference.transform.GetChild(0).GetComponent<Animator>().SetTrigger("Run");
                StartCoroutine(SetAnimatorBoolAfterDelay(chokmahReference.transform.GetChild(0).GetComponent<Animator>(), "PunchThenDie", delayTime));
            }
        }


        TargetPath = null;
    }
    private IEnumerator SetAnimatorBoolAfterDelay(Animator animator, string boolParameterName, float delayTime)
    {
        if (chokmahReference != null)
        {
            chokmahCollision = Chokmah.GetComponent<CapsuleCollider>();
            yield return new WaitForSeconds(delayTime);
            animator.SetBool(boolParameterName, true);
            yield return new WaitForSeconds(0.7f);
            Debug.Log("Rotate and freeze"); 
            chokmahReference.transform.Rotate(0, -90, 0);
            chokmahReference.transform.rotation = chokmahReference.transform.rotation;
            chokmahReference.transform.position = chokmahReference.transform.position; // Lock la position
            chokmahCollision.enabled = false;
            DestroyImmediate(chokmahCollision, true);
            chokmahReference = null;
        }
    }


}
