using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnknownZone_Collision : MonoBehaviour
{

    // ----- VARIABLES -----

    private AII_UnknownZone IO_script;

    public float discoveryTime;








    // ----- LOGIC -----

    void Start()
    {
        IO_script = transform.parent.GetComponent<AII_UnknownZone>();
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Discovering());
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }


    private IEnumerator Discovering()
    {
        yield return new WaitForSeconds(discoveryTime);

        IO_script.Discovered();
    }
}
