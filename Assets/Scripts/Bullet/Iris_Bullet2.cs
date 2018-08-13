using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet2 : Bullet {

    Vector3 oPosition_Temp;

    int bulNum;

    public int irisBullet2Num_Temp;

    float rotatingAngle = -(3.14f / 9f);

    public void Init_Iris_Bullet2(int _shooterNum, int num)
    {
        photonView.RPC("Init_Iris_Bullet2_RPC", PhotonTargets.All, _shooterNum, num);
    }

    [PunRPC]
    protected void Init_Iris_Bullet2_RPC(int _shooterNum, int num)
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

    protected override void Move(int _shooterNum)
    {
        damage = 50;

        irisBullet2Num_Temp = bulNum;

        Debug.Log("Move : " + irisBullet2Num_Temp);

        speed = 8f;

        rotatingAngle += (irisBullet2Num_Temp * (3.14f / 18f));

        DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
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
