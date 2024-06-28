using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class RecapLevel : MonoBehaviour
{
    public int NextScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(3);

        while (true)
        {
            print("TA");

            if (Input.anyKey)
            {
                print("TO");
                SceneManager.LoadSceneAsync(NextScene);
            }
            yield return new WaitForEndOfFrame();
        }
    }



}
