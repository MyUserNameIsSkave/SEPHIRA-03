using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL05_BinahSavedCinematique : MonoBehaviour, IEventTriggerable
{
    public GameObject YakuzaBack; // L'objet cible contenant l'Animator
    public GameObject YakuzaFront; // L'objet cible contenant l'Animator
    public GameObject Binah; // L'objet cible contenant l'Animator

    [SerializeField]
    public string booleanBinahSaved; // Le nom de la booléenne à activer

    private Animator animatorB;
    private Animator animatorYF;
    private Animator animatorYB;
    private Rigidbody binahBody;

    void Start()
    {
        // Récupère l'Animator du GameObject cible
        animatorB = Binah.GetComponent<Animator>();
        animatorYB = YakuzaBack.GetComponent<Animator>();
        animatorYF = YakuzaFront.GetComponent<Animator>();
        binahBody = Binah.GetComponent<Rigidbody>();
    }



    public void TriggerEvent()
    {
        // Active la booléenne dans l'Animator
        animatorB.SetBool(booleanBinahSaved, true);
        Invoke("BinahSavedYC", 6.4f);
        Invoke("EnableGravity", 17.4f);
    }

   void BinahSavedYC()
    {
        animatorYB.SetBool(booleanBinahSaved, true);
        animatorYF.SetBool(booleanBinahSaved, true);
    }
   void EnableGravity()
    {
        binahBody.useGravity = true;
    }
}
