using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Insert_key : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("start");

            Invoke("NextLevel", 5f);
        }

    }


    private void NextLevel()
    {
        SceneManager.LoadScene("Level 0.5");
    }
}
