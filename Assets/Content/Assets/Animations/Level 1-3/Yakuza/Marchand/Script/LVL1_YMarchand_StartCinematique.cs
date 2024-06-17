using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1_Scene1_YMarchand : MonoBehaviour, IEventTriggerable
{

    public GameObject Yakuza; // L'objet cible contenant l'Animator

    [SerializeField]
    public string booleanStart; // Le nom de la bool�enne � activer


    private Animator animatorY;


    void Start()
    {
        // R�cup�re l'Animator du GameObject cible
        animatorY = Yakuza.GetComponent<Animator>();
    }



    public void TriggerEvent()
    {
        // Active la bool�enne dans l'Animator
        Debug.Log("La bool�enne " + booleanStart + " a �t� activ�e.");
        animatorY.SetBool(booleanStart, true);
    }
}
