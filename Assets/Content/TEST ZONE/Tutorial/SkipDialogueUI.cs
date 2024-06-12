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



    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(ChangeScale());
        }
    }



    IEnumerator ChangeScale()
    {
        GetComponent<RectTransform>().localScale = Vector3.one * 0.5f;

        yield return new WaitForSeconds(0.1f);

        GetComponent<RectTransform>().localScale = Vector3.one * 0.63763f;
    }
}
