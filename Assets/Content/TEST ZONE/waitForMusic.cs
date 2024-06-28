using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class waitForMusic : MonoBehaviour, IEventTriggerable
{
    private bool dialogueFinished;
    public int NextScene;


    public void TriggerEvent()
    {
        dialogueFinished = true;
    }


    private void Start()
    {
        StartCoroutine(LevelTransition());
    }


    IEnumerator LevelTransition()
    {
        yield return new WaitForSeconds(63);

        while (true)
        {
            print("CanSwitch");

            if (dialogueFinished)
            {
                yield return new WaitForSeconds(1);
                print("switch level");
                SceneManager.LoadSceneAsync(NextScene);
                StopAllCoroutines();
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
