using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AII_UnknownZone : AI_Interaction

{
    // ----- VARIABLES -----


    [SerializeField]
    private float discoveryTime;



    // ----- INTERFACE METHODS -----

    public override void Interaction()
    {
        //Useless
    }








    // ----- CUSTOM METHODES -----



    private UnknownZone_Collision collisionScript;

    private void Start()
    {
        collisionScript = transform.GetChild(0).GetComponent<UnknownZone_Collision>();
        collisionScript.discoveryTime = discoveryTime;
    }











    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Binah"))
        {
            StartCoroutine(Discovering());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Binah"))
        {
            StopAllCoroutines();
        }
    }



    private IEnumerator Discovering()
    {
        yield return new WaitForSeconds(discoveryTime);

        Discovered();
    }







    public void Discovered()
    {
        ModifyStateVariables();
        UtilityAI_Manager.DoingActionState.ResetVariables();

        Destroy(gameObject);
    }

}
