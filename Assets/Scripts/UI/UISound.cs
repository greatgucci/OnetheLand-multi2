using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour {

    public GameObject[] uiSoundOb;
    GameObject[] uiSound_Temp = new GameObject[10];

    public void PlayChoose()
    {
        uiSound_Temp[0] = Instantiate(uiSoundOb[0]);
        Destroy(uiSound_Temp[0], 3f);
    }
    
    public void PlayDecision()
    {
        uiSound_Temp[1] = Instantiate(uiSoundOb[1]);
        Destroy(uiSound_Temp[1], 3f);
    }

    public void PlayCharacterChoose()
    {
        uiSound_Temp[2] = Instantiate(uiSoundOb[2]);
        Destroy(uiSound_Temp[2], 3f);
    }

    public void PlayItem()
    {
        uiSound_Temp[3] = Instantiate(uiSoundOb[3]);
        Destroy(uiSound_Temp[3], 3f);
    }

    public void PlayGameStart()
    {
        uiSound_Temp[4] = Instantiate(uiSoundOb[4]);
        Destroy(uiSound_Temp[4], 3f);
    }

    public void PlayBGM1()
    {
        uiSound_Temp[7] = Instantiate(uiSoundOb[7]);
        Destroy(uiSound_Temp[7], 134f);
        Invoke("PlayBGM2", 134f);
    }

    public void PlayBGM2()
    {
        uiSound_Temp[8] = Instantiate(uiSoundOb[8]);
        Destroy(uiSound_Temp[8], 76f);
        Invoke("PlayBGM2", 76f);
    }

    private void Awake()
    {
        PlayBGM1();
    }
}
