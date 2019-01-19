using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill3Targeting : Bullet {

    public void Init_Iris_Skill3Targeting(int _shooterNum)
    {
        photonView.RPC("Init_Iris_Skill3Targeting_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_Iris_Skill3Targeting_RPC(int _shooterNum)
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
        speed = 1f;

        StartCoroutine(MoveIrisSKill3Targeting());
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    IEnumerator MoveIrisSKill3Targeting()
    {
        while(true)
        {
            DVector = FavoriteFunction.VectorCalc(gameObject, oNum);

            rgbd.velocity = DVector * speed;
            yield return null;
        }
    }

}
