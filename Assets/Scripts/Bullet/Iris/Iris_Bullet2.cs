using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet2 : Bullet {

    Vector3 oPosition_Temp;

    int bulNum;

    public int irisBullet2Num_Temp;

    float rotatingAngle = -(3.14f / 9f);

    public void Init_Iris_Bullet2(int _shooterNum, int num, Vector3 aimDVector)
    {
        photonView.RPC("Init_Iris_Bullet2_RPC", PhotonTargets.All, _shooterNum, num, aimDVector);
    }

    [PunRPC]
    protected void Init_Iris_Bullet2_RPC(int _shooterNum, int num, Vector3 aimDVector)
    {
        DVector = aimDVector;
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
        damage = 25;

        irisBullet2Num_Temp = bulNum;

        speed = 8f;

        rotatingAngle += (irisBullet2Num_Temp * (3.14f / 18f));
        rotatingAngle += DVector.y > 0 ? Vector3.AngleBetween(Vector3.right, DVector) : -Vector3.AngleBetween(Vector3.right, DVector);

        if (_shooterNum == 1)
        {
            transform.Rotate(0f, 0f, 0f);
        }

        else
        {
            transform.Rotate(0f, 180f, 0f);
        }

        Vector3 dVector_Temp = DVector;

        dVector_Temp.x = Mathf.Cos(rotatingAngle);
        dVector_Temp.y = Mathf.Sin(rotatingAngle);

        dVector_Temp.Normalize();

        rgbd.velocity = dVector_Temp * speed;
    }
    
    
}
