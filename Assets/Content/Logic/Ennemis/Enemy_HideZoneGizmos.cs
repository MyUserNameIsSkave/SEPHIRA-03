using UnityEditor;
using UnityEngine;


public class Enemy_HideZoneGizmos : MonoBehaviour
{

    [SerializeField]
    bool hide = true;


#if UNITY_EDITOR

    private void OnValidate()
    {
        switch (hide)
        {
            case true:
                SceneVisibilityManager.instance.Hide(transform.GetChild(0).gameObject, true);
                break;

            case false:
                SceneVisibilityManager.instance.Show(transform.GetChild(0).gameObject, true);
                break;
        } 
    }
#endif
}
