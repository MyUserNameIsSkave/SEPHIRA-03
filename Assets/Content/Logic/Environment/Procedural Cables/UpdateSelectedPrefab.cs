using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSelectedPrefab : MonoBehaviour
{
    [SerializeField]
    private ProceduralCable ProceduralCable;

    private void OnDrawGizmosSelected()
    {
        ProceduralCable.OnSelectedPreview();
    }
}
