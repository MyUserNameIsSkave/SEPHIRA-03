using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableInteraction : MonoBehaviour, IEventTriggerable
{

    [SerializeReference]
    LayerMask ActivatedLayer;

    [SerializeReference]
    Material activatedMaterial;

    [SerializeField]
    Image mouse_icon;

    public void TriggerEvent()
    {
        gameObject.layer = GetLayerFromMask(ActivatedLayer);
        //GetComponent<MeshRenderer>().material = activatedMaterial;


        mouse_icon.enabled = true;
    }



    int GetLayerFromMask(LayerMask layerMask)
    {
        // Convertissez le LayerMask en numéro de couche
        int layer = 0;
        int layerBit = 1;

        for (int i = 0; i < 32; i++)
        {
            if ((layerMask.value & layerBit) == layerBit)
            {
                layer = i;
                break;
            }

            layerBit <<= 1;
        }

        return layer;
    }

}
