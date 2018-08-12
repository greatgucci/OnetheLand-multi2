using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet_Plural : Bullet {

    protected int bulNum;
    public void Init(int _shooterNum, int num)
    {
        photonView.RPC("Init_RPC_BulNum", PhotonTargets.All, _shooterNum, num);
    }


    public void Init(int _shooterNum, int num, int communicatingObject)
    {
        photonView.RPC("Init_RPC", PhotonTargets.All, _shooterNum, num, communicatingObject);
    }

    [PunRPC]
    protected void Init_RPC_BulNum(int _shooterNum, int num)
    {
        bulNum = num;
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

    [PunRPC]
    protected void Init_RPC(int _shooterNum, int num, int communicatingObject)
    {
        commuObject = PhotonView.Find(communicatingObject).gameObject;
        bulNum = num;
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
