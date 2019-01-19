using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_BulletShift : Bullet {

    public void Init_Iris_BulletShift(int _shooterNum)
    {
        photonView.RPC("Init_Iris_BulletShift_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_Iris_BulletShift_RPC(int _shooterNum)
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

    protected override void Move(int _shooterNum)
    {
        DestroyToServer();
    }

}
