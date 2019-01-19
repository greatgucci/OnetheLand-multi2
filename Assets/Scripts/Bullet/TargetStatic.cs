using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetStatic : Bullet {

    public void Init_TargetStatic(int _shooterNum)
    {
        photonView.RPC("Init_TargetStatic_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_TargetStatic_RPC(int _shooterNum)
    {
        Invoke("DestroyToServer", 10f);
        shooterNum = _shooterNum;
        if (shooterNum == 1)
        {
            oNum = 2;
        }
        else if (shooterNum == 2)
        {
            oNum = 1;
        }
        Move(shooterNum);
    }
}
