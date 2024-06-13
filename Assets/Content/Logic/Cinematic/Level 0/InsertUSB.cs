using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InsertUSB : MonoBehaviour
{
    [SerializeField]
    private MainDialogueManager dialogue;

    [SerializeField]
    private GameObject skipUi;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogue.dialogueFinished == false)
            {
                return;
            }


            GetComponent<Animator>().SetTrigger("start");

            Destroy(skipUi);

            Invoke("NextLevel", 5f);
        }

    }


    private void NextLevel()
    {
        SceneManager.LoadSceneAsync("Level 0.5");
    }
}
