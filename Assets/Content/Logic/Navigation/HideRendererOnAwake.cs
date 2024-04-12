using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideRendererOnAwake : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
}
