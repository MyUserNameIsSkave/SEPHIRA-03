using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GateClosing : MonoBehaviour, IEventTriggerable
{
    [SerializeField]
    private Animator gateAnimator, roomAnimation;

    [SerializeField]
    private GameObject objetAMouvoir; // Nouvel objet à déplacer
    [SerializeField]
    private float distanceAX = 5f; // Distance à parcourir sur l'axe X
    [SerializeField]
    private float vitesseDeplacement = 2f; // Vitesse de déplacement de l'objet

    [SerializeField]
    private CameraBase nextCamera;

    public MonoBehaviour dialogue;


    public void TriggerEvent()
    {
        StartCoroutine(CloseGate());
        StartCoroutine(MoveWall());
    }

    IEnumerator CloseGate()
    {

        GameManager.Instance.BinahManager.SendBinahToLocation(GameManager.Instance.Binah.transform.position);

        yield return new WaitForSeconds(2f);

        roomAnimation.SetTrigger("BinahPassed");

        yield return new WaitForSeconds(9f);

        gateAnimator.SetTrigger("BinahPassed");

        yield return new WaitForSeconds(3f);

        GameManager.Instance.CameraController.CurrentCamera = nextCamera;

        IEventTriggerable eventInterface = dialogue.GetComponent<IEventTriggerable>();
        eventInterface.TriggerEvent();
    }
    IEnumerator MoveWall()
    {
        yield return new WaitForSeconds(2f);

        // Calculer la position cible sur l'axe X
        float positionCibleX = objetAMouvoir.transform.position.x + distanceAX;
        Vector3 positionCible = new Vector3(positionCibleX, objetAMouvoir.transform.position.y, objetAMouvoir.transform.position.z);

        // Déplacer l'objet vers la position cible sur l'axe X
        float distance = Vector3.Distance(objetAMouvoir.transform.position, positionCible);
        float tempsDeplacement = distance / vitesseDeplacement;

        float tempsEcoule = 0f;
        while (tempsEcoule < tempsDeplacement)
        {
            tempsEcoule += Time.deltaTime;
            float pourcentage = tempsEcoule / tempsDeplacement;

            objetAMouvoir.transform.position = Vector3.Lerp(objetAMouvoir.transform.position, positionCible, pourcentage);

            yield return null;
        }

        // Assure que l'objet est à la position cible
        objetAMouvoir.transform.position = positionCible;
    }

}
