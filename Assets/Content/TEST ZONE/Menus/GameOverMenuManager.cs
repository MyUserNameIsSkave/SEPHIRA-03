using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuManager : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    public void OnBecameVisible()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
    public void Restart()
    {
        //FAUDRA GERER LE CHECKPOINT
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Update is called once per frame
    public void QuitToMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
