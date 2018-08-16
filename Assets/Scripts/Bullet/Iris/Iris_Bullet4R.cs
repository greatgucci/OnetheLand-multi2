using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet4R : Bullet {

    int bulNum;

    float rotatingAngle;

    public void Init_Iris_Bullet4R(int _shooterNum, int num)
    {
        photonView.RPC("Init_Iris_Bullet4R_RPC", PhotonTargets.All, _shooterNum, num);
    }

    [PunRPC]
    protected void Init_Iris_Bullet4R_RPC(int _shooterNum, int num)
    {
        DVector = PlayerManager.instance.GetPlayerByNum(_shooterNum).aimVector;
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

    protected override void Move(int _shooterNum)
    {
        StartCoroutine(Move_IrisBullet4R());
    }

    IEnumerator Move_IrisBullet4R()
    {
        speed = 3.75f;
        damage = 100;
        float timer = 0f;

        while (true)
        {
            if (timer >= 2f)
            {
                break;
            }

            DVector = PlayerManager.instance.GetPlayerByNum(shooterNum).aimPosition - transform.position;

            rgbd.velocity = DVector * speed;

            speed += Time.deltaTime * 2.5f;
            timer += Time.deltaTime;
            yield return null;
        }

        DestroyToServer();
    }

}
