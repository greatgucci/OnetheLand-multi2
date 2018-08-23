using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    DIANA,
    IRIS
}

[System.Serializable]
public struct MultiSound
{
    public int Length
    {
        get { return audioClips.Length; }
    }
    public AudioClip RandomSound
    {
        get { return audioClips[Random.Range(0, audioClips.Length)]; }
    }
    public AudioClip[] audioClips; 
}
public class AudioController : MonoBehaviour {

    public static AudioController instance;
    public GameObject[] dianaEffectSound;
    public GameObject[] irisEffectSound;
    PhotonView photonView;
    AudioSource bgm;
    private void Awake()
    {
        instance = this;
        bgm = GetComponent<AudioSource>();
        photonView = GetComponent<PhotonView>();
    }

    public void PlayBGM()
    {
        bgm.Play();
    }
    
    public void PlayEffectSound(Character cha,int i)
    {
        photonView.RPC("PlayEffectSound_RPC", PhotonTargets.All, cha, i);
    }

    [PunRPC]
    private void PlayEffectSound_RPC(Character cha, int i)
    {
        switch(cha)
        {
            case Character.DIANA:
                Instantiate(dianaEffectSound[i]);
                break;
            case Character.IRIS:
                Instantiate(irisEffectSound[i]);
                break;
        }
    }
}
