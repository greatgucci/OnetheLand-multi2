using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class PlayerAnimation : Photon.PunBehaviour
{

    private SkeletonAnimation myAnim;
    
    // Use this for initialization
    void Start () {
        myAnim = this.GetComponent<SkeletonAnimation>();
    }


    [PunRPC]
    public void ChangeAnim_RPC(bool _isloop, float _timescale, string aniName)
    {
        myAnim.loop = _isloop;
        myAnim.timeScale = _timescale;
        myAnim.AnimationName = aniName;
    }
    [PunRPC]
    public void AddAnimationLayer_RPC(int num,string anime,bool b)
    {
        myAnim.state.SetAnimation(num, anime, b);
    }

    /// <summary>
    /// 로컬에서만 호출할것!
    /// </summary>
    public void ChangeAnim(bool isloop,float timescale,string Name)
    {
        if(photonView.isMine)
        photonView.RPC("ChangeAnim_RPC",PhotonTargets.All,isloop, timescale, Name);
    }
    /// <summary>
    /// 로컬에서만 호출할것!
    /// </summary>
    public void AddAnimationLayer(int num, string anime, bool b)
    {
        if (photonView.isMine)
            photonView.RPC("AddAnimationLayer_RPC", PhotonTargets.All, num, anime, b);
    }
}
