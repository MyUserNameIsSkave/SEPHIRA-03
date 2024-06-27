using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KetherDeath : MonoBehaviour, IEventTriggerable
{
    public float vitesseDescente = 0.1f; // Vitesse de descente sur l'axe Y
    public float tempsOpacite = 2f; // Temps pour réduire l'opacité à 0
    private Vector3 echelleInitiale;

    public SkinnedMeshRenderer meshRenderer;
    private Color objectColor;
    private float timer;

    void Start()
    {
        objectColor = meshRenderer.material.color;
        echelleInitiale = transform.localScale;
    }

    public void TriggerEvent()
    {
        timer = 0f;
        StartCoroutine(ReduireOpaciteEtDescendre());
    }

    IEnumerator ReduireOpaciteEtDescendre()
    {
        while (timer < tempsOpacite)
        {
            timer += Time.deltaTime;
            float pourcentage = 1 - (timer / tempsOpacite);

            // Réduit l'opacité du mesh
            objectColor.a = pourcentage;
            meshRenderer.material.color = objectColor;

            // Réduit l'échelle de l'objet
            transform.localScale = Vector3.Lerp(echelleInitiale, Vector3.zero, 1 - pourcentage);

            yield return null;
        }

        // Assure que l'opacité est à 0 à la fin
        objectColor.a = 0;
        meshRenderer.material.color = objectColor;
    }
}
