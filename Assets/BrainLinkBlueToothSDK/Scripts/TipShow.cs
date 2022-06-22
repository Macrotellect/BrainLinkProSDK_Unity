using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class TipShow : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj_textTip;
    void Start()
    {
#if UNITY_ANDROID
        obj_textTip.SetActive(true);
 #else
        obj_textTip.SetActive(false);
#endif
        
    }

    
}
