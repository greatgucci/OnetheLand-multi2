using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Bullet_2 : Bullet
{
    Vector3 oPosition_Temp;

    int bulNum;

    public int irisBullet2Num_Temp;

    float rotatingAngle = -2f * Mathf.PI / 8f;

    public void Init_Iris_Bullet_2(int _shooterNum, int num)
    {
        photonView.RPC("Init_Iris_Bullet2_RPC", PhotonTargets.All, _shooterNum, num);
    }

    [PunRPC]
    protected void Init_Iris_Bullet2_RPC(int _shooterNum, int num)
    {
        bulNum = num;
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
        damage = 15;
        knockback = 15;

        irisBullet2Num_Temp = bulNum;

        speed = 10f;

        rotatingAngle += (irisBullet2Num_Temp * 2 * Mathf.PI / 8f);

        if (_shooterNum == 1)
        {
            transform.Rotate(0f, 0f, 0f);
        }

        else
        {
            transform.Rotate(0f, 180f, 0f);
            rotatingAngle += Mathf.PI;
        }

        Vector3 dVector_Temp;

        dVector_Temp.x = Mathf.Cos(rotatingAngle);
        dVector_Temp.y = Mathf.Sin(rotatingAngle);
        dVector_Temp.z = 0f;

        dVector_Temp.Normalize();

        rgbd.velocity = dVector_Temp * speed;
        StartCoroutine(MoveDirChange());
    }

    IEnumerator MoveDirChange()
    {
        float timer = 0f;

        while(true)
        {
            if (MapManager.Instance.CheckMapBoundary(transform) && timer <= 0f)
            {
                if (shooterNum == 1 && rgbd.velocity.y > 0)
                {
                    rotatingAngle = -Mathf.PI * 2 / 8f;
                }
                else if (shooterNum == 1 && rgbd.velocity.y < 0)
                {
                    rotatingAngle = Mathf.PI * 2 / 8f;
                }
                else if (shooterNum == 2 && rgbd.velocity.y > 0)
                {
                    rotatingAngle = Mathf.PI * 2 / 8f;
                    rotatingAngle += Mathf.PI;
                }
                else if (shooterNum == 2 && rgbd.velocity.y < 0)
                {
                    rotatingAngle = -Mathf.PI * 2 / 8f;
                    rotatingAngle += Mathf.PI;
                }

                Vector3 dVector_Temp;

                dVector_Temp.x = Mathf.Cos(rotatingAngle);
                dVector_Temp.y = Mathf.Sin(rotatingAngle);
                dVector_Temp.z = 0f;

                dVector_Temp.Normalize();

                rgbd.velocity = dVector_Temp * speed;

                timer = 0.1f;
            }

            if (timer > 0f)
                timer -= Time.deltaTime;

            yield return null;
        }
    }
}
