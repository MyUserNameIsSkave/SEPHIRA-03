using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnPlay : MonoBehaviour
{
    private MeshRenderer _renderer;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.enabled = true;
    }


}
