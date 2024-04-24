using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NextLevel1 : MonoBehaviour, IEventTriggerable
{

    [SerializeField]
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerEvent()
    {
        GameManager.Instance.ChangeScene("Level 1");
    }
}
