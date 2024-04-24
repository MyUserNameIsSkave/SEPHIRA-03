using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointCollision : MonoBehaviour
{
    [SerializeField] 
    private CameraBase chekpointCamera;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Binah"))
        {
            return;
        }

        GetComponent<Collider>().enabled = false;
        StartCoroutine(CheckForCamera());
    }




    IEnumerator CheckForCamera()
    {
        while (true)
        {
            if (chekpointCamera.alreadyUsed)
            {
                GameManager.Instance.currentCheckpoint = this;
                yield break;
            }

            yield return 0;
        }
    }



    public void Respawn()
    {
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);

        GameManager.Instance.CameraController.CurrentCamera = chekpointCamera;
        GameManager.Instance.Binah.transform.position = transform.position;
        GameManager.Instance.BinahManager.SendBinahToLocation(transform.position);


        print("SHOUMD RESPAWN PLAYER AT " + this);

    }
}
