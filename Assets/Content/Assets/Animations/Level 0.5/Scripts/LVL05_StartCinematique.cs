using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL05_StartCinematique : MonoBehaviour, IEventTriggerable
{
    public GameObject YakuzaBack; // L'objet cible contenant l'Animator
    public GameObject YakuzaFront; // L'objet cible contenant l'Animator
    public GameObject Binah; // L'objet cible contenant l'Animator
    public DialogueManager YBStartScript;
    public BinahScream BinahScreamScript;
    public float timeToWaitBeforeStopScreaming;

    [SerializeField]
    public string booleanStart; // Le nom de la booléenne à activer

   
    private Animator animatorB;
    private Animator animatorYF;
    private Animator animatorYB;
    

    void Start()
    {
        // Récupère l'Animator du GameObject cible
        animatorB = Binah.GetComponent<Animator>();
        animatorYB = YakuzaBack.GetComponent<Animator>();
        animatorYF = YakuzaFront.GetComponent<Animator>();
    }



    public void TriggerEvent()
    {
        // Active la booléenne dans l'Animator
        Debug.Log("La booléenne " + booleanStart + " a été activée.");
        animatorB.SetBool(booleanStart, true);
        animatorYB.SetBool(booleanStart, true);
        animatorYF.SetBool(booleanStart, true);
        Invoke("WaitBeforeStart", 9.5f);
    }

    public void WaitBeforeStart()
    {
        Debug.Log("Dialogue lancé.");
        YBStartScript.TriggerEvent();
        Invoke("WaitBeforeStopScream", timeToWaitBeforeStopScreaming);
    }
    public void WaitBeforeStopScream()
    {
        Debug.Log("Binah arrête de crier !");
        BinahScreamScript.TriggerEvent();
    }
}


