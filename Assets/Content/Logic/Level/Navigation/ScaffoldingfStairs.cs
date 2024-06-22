using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaffoldingfStairs : MonoBehaviour
{
    


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Binah"))
        {
            GameManager.Instance.BinahManager.usingScaffoldingStairs = true;
            GameManager.Instance.Binah.GetComponent<Animator>().speed = GameManager.Instance.Binah.GetComponent<Animator>().speed / 2;
        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Binah"))
        {
            GameManager.Instance.BinahManager.usingScaffoldingStairs = false;
            GameManager.Instance.Binah.GetComponent<Animator>().speed = GameManager.Instance.Binah.GetComponent<Animator>().speed * 2;

        }
    }
}
