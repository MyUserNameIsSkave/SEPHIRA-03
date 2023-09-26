using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Attack : MonoBehaviour
{
    [SerializeField]
    private EnnemisAnimation EnnemiAnimation;




    EnnemisBehavior EnnemisBehavior;



    NavMeshAgent EnnemiAgent;

    NavMeshAgent BinahAgent;
    float BaseSpeed;


    public float StruggleDuration;



    private void Awake()
    {
        EnnemisBehavior = transform.parent.GetComponent<EnnemisBehavior>();

        EnnemiAgent = transform.parent.GetComponent<NavMeshAgent>();

        BinahAgent = GameObject.FindGameObjectWithTag("Binah").GetComponent<NavMeshAgent>();
        BaseSpeed = BinahAgent.speed;
    }




    private void OnTriggerEnter(Collider other)
    {
        //BinahDetected = true;

        if (other.CompareTag("Binah") && EnnemisBehavior.IsChasing)
        {
            BinahAgent.speed = 0;
            EnnemiAgent.speed = 0;



            //Lancer phase de résistance
            EnnemisBehavior.IsStruggling = true;



            
            Invoke("GameOver", StruggleDuration);
        }
    }



    void GameOver()
    {
        if (!EnnemisBehavior.IsStruggling)
        {
            return;
        }



        GameObject.FindGameObjectWithTag("Binah").GetComponent<BinahAnimation>().OnPunched();
        EnnemiAnimation.OnPunch();

        Invoke("Reload", 2.2f);
    }



    void Reload()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

}
