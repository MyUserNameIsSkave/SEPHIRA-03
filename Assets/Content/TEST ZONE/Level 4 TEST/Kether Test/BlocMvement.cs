using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocMvement : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        GetComponent<Animator>().speed = 0;
    }


}
