using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAnimationUI : MonoBehaviour, IEventTriggerable
{
    private Material ui_material;

    [SerializeField]
    private Texture2D[] ui_sprites;

    [SerializeField]
    private float frame_duration;

    private void Awake()
    {
        ui_material = GetComponent<Image>().material;
        StartCoroutine(Animation());


    }

    IEnumerator Animation()
    {
        int i = 0;

        while (true)
        {
            if (i == ui_sprites.Length)
            {
                i = 0;
            }

            yield return new WaitForSeconds(frame_duration);
            ui_material.SetTexture("_base_texture", ui_sprites[i]);

            i++;
        }
    }

    public void TriggerEvent()
    {
        Destroy(transform.parent.gameObject);
    }
}
