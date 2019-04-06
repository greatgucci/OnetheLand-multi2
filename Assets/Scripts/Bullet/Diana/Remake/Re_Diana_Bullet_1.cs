using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Diana_Bullet_1 : Bullet
{
    DianaControl dCon;
    public void Init_Diana_Bullet_1(int _shooterNum)
    {
        photonView.RPC("Init_Diana_Bullet_1_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_Diana_Bullet_1_RPC(int _shooterNum)
    {
        dCon = GameManager.instance.Local.GetComponentInParent<DianaControl>();
        DVector = FavoriteFunction.VectorCalc(gameObject, _shooterNum == 1 ? 2 : 1);
        Invoke("DestroyToServer", 5f);
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
        switch(dCon.attack_Level)
        {
            case 1:
                speed = 18.5f;
                break;
            case 2:
                speed = 20f;
                break;
            default:
                speed = 17.5f;
                break;
        }

        damage = 20;
        knockback = 20;

        if (_shooterNum == 1)
        {
            transform.Rotate(0f, 0f, 0f);
        }
        else
        {
            transform.Rotate(0f, 180f, 0f);
        }

        DVector = new Vector3(1f, 0f, 0f);
        rgbd.velocity = DVector * speed;
    }
}
