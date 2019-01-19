using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_BulletQ : Bullet {

    public void Init_Iris_BulletQ(int _shooterNum)
    {
        photonView.RPC("Init_Iris_BulletQ_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_Iris_BulletQ_RPC(int _shooterNum)
    {
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
        damage = 20;

        DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
        FavoriteFunction.RotateBullet(gameObject);

        StartCoroutine(AccelBullet());
    }

    IEnumerator AccelBullet()
    {
        float timer = 0f;

        speed = 0f;

        yield return new WaitForSeconds(0.2f);

        while(true)
        {
            if (timer >= 0.7f)
            {
                break;
            }

            speed += timer * 3f;
            rgbd.velocity = speed * DVector;

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
