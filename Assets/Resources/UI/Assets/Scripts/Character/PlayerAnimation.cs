using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public abstract class PlayerAnimation : Photon.PunBehaviour
{

    private SkeletonAnimation myAnim;
    
    // Use this for initialization
    void Start () {
        myAnim = this.GetComponent<SkeletonAnimation>();
    }
    
    [PunRPC]
    protected void ChangeAnim_RPC(byte aniName, bool _isloop)
    {
        myAnim.loop = _isloop;
        myAnim.AnimationName = IntToAnimationName(aniName);
    }
    [PunRPC]
    protected void AddAnimationLayer_RPC(byte anime,bool b)
    {
        myAnim.state.SetAnimation(1, IntToAnimationName(anime), b);
    }
    [PunRPC]
    protected void SetAnimationLayerEmpty_RPC()
    {
        myAnim.state.SetEmptyAnimation(1, 0f);
    }

    /// <summary>
    /// 로컬에서만 호출할것!
    /// </summary>
    public void ChangeAnim(byte Name, bool isloop)
    {
        if(photonView.isMine)
        {
            photonView.RPC("ChangeAnim_RPC", PhotonTargets.All, Name, isloop);
        }
    }
    /// <summary>
    /// 로컬에서만 호출할것!
    /// </summary>
    public void AddAnimationLayer(byte Name, bool b)
    {
        if (photonView.isMine)
            photonView.RPC("AddAnimationLayer_RPC", PhotonTargets.All, Name, b);
    }
    public void SetAnimationLayerEmpty()
    {
        if(photonView.isMine)
        {
            photonView.RPC("SetAnimationLayerEmpty_RPC", PhotonTargets.All);
        }
    }
    protected abstract string IntToAnimationName(byte name);
}
