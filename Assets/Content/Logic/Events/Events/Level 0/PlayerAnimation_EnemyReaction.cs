using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAnimation_EnemyReaction : MonoBehaviour, IEventTriggerable
{
    public void TriggerEvent()
    {
        GetComponent<Animator>().SetTrigger("React");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
