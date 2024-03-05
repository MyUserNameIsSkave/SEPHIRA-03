using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInteraction : MonoBehaviour, IEventTriggerable
{
    
    [SerializeReference]
    LayerMask ActivatedLayer;

    [SerializeReference]
    Material activatedMaterial;



    public void TriggerEvent()
    {
        Debug.Log("Layer changed to: " + LayerMask.LayerToName(gameObject.layer));
        gameObject.layer = GetLayerFromMask(ActivatedLayer);
        GetComponent<MeshRenderer>().material = activatedMaterial;
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
