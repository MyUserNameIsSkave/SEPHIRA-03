using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL1_Scene1_YMarchand : MonoBehaviour, IEventTriggerable
{

    public GameObject Yakuza; // L'objet cible contenant l'Animator

    [SerializeField]
    public string booleanStart; // Le nom de la booléenne à activer


    private Animator animatorY;


    void Start()
    {
        // Récupère l'Animator du GameObject cible
        animatorY = Yakuza.GetComponent<Animator>();
    }



    public void TriggerEvent()
    {
        // Active la booléenne dans l'Animator
        Debug.Log("La booléenne " + booleanStart + " a été activée.");
        animatorY.SetBool(booleanStart, true);
    }
}
