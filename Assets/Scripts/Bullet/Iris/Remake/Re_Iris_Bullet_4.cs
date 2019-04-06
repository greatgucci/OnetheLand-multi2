using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Bullet_4 : Bullet
{
    GameObject commuObject;
    public void Init_Iris_Bullet_4(int _shooterNum, int commuNum, float laserSize)
    {
        photonView.RPC("Init_Iris_Bullet_4_RPC", PhotonTargets.All, _shooterNum, commuNum, laserSize);
    }

    [PunRPC]
    protected void Init_Iris_Bullet_4_RPC(int _shooterNum, int commuNum, float laserSize)
    {
        Invoke("DestroyToServer", 1f);
        commuObject = PhotonView.Find(commuNum).gameObject;
        shooterNum = _shooterNum;
        transform.localScale = new Vector3(60f, laserSize * 2f, 1f);
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
        damage = 100;
        knockback = 100;
        float rotatingAngle = 0f;
        float tempAngle = 0f;

        DVector = commuObject.transform.position - transform.position;
        DVector.Normalize();

        rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
        transform.Rotate(Vector3.forward, rotatingAngle - tempAngle);
        tempAngle = rotatingAngle;
    }
}
