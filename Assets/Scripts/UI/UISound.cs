using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour {

    public GameObject[] uiSoundOb;
    GameObject[] uiSound_Temp = new GameObject[10];

    public void PlayChoose()
    {
        uiSound_Temp[0] = Instantiate(uiSoundOb[0]);
        Destroy(uiSound_Temp[0], 2f);
    }
    
    public void PlayDecision()
    {
        uiSound_Temp[1] = Instantiate(uiSoundOb[1]);
        Destroy(uiSound_Temp[1], 2f);
    }
}
