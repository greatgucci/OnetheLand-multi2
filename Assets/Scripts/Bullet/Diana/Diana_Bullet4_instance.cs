using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet4_instance : Bullet
{
    public void Init_Diana_Bullet4_Cross(int _shooterNum)
    {
        photonView.RPC("Init_Diana_Bullet4_Cross_RPC", PhotonTargets.All, _shooterNum);
    }
    [PunRPC]
    protected void Init_Diana_Bullet4_Cross_RPC(int _shooterNum)
    {
        SetTag(type.bullet);
    }
}
