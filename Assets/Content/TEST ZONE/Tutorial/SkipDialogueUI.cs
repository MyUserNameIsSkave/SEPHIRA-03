using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipDialogueUI : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (GameManager.Instance.playerInputLocked)
        {
            GetComponent<Image>().enabled = true;
        }
        else
        {
            GetComponent<Image>().enabled = false;
        }
    }
}
