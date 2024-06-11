using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BinahMovementTutorialManager : MonoBehaviour
{

    private void Awake()
    {
        GetComponent<Image>().enabled = false;
    }



    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.playerInputLocked)
        {
            GetComponent<Image>().enabled = false;
            return;
        }
        else
        {
            GetComponent<Image>().enabled = true;

            if (Input.GetKey(KeyCode.Space) && Input.GetMouseButtonDown(0))
            {
                Destroy(transform.parent.gameObject);
            }
        }



    }
}
