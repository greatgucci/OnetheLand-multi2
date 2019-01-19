using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_BulletLeft : Bullet {

    Vector3 oPosition_Temp;

    int bulNum;

    float rotatingAngle;
    float rotating_Temp = 0f;

    public void Init_Iris_BulletLeft(int _shooterNum, int num, Vector3 aimDVector)
    {
        photonView.RPC("Init_Iris_BulletLeft_RPC", PhotonTargets.All, _shooterNum, num, aimDVector);
    }

    [PunRPC]
    protected void Init_Iris_BulletLeft_RPC(int _shooterNum, int num, Vector3 aimDVector)
    {
        DVector = FavoriteFunction.VectorCalc(gameObject, _shooterNum == 1 ? 2 : 1);
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
        damage = 10;
        speed = 6f;


        if (bulNum == 0)
        {
            rotating_Temp = (2 * Mathf.PI * 0);
        }
        else if(bulNum == 1)
        {
            rotating_Temp = (2 * Mathf.PI * (Random.Range(-7f, 7f) / 360f));
        }
        else if (bulNum == 2)
        {
            rotating_Temp = (2 * Mathf.PI * (Random.Range(-7f, 7f) / 360f));
        }
        else if (bulNum == 3)
        {
            rotating_Temp = (2 * Mathf.PI * (Random.Range(-7f, 7f) / 360f));
        }
        else if (bulNum == 4)
        {
            rotating_Temp = (2 * Mathf.PI * (Random.Range(-7f, 7f) / 360f));
        }

        rotatingAngle = DVector.y > 0 ? Vector3.AngleBetween(Vector3.right, DVector) : -Vector3.AngleBetween(Vector3.right, DVector);
        rotatingAngle += rotating_Temp;

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

        DVector = dVector_Temp;

        StartCoroutine(Accel_IrisBulletLeft());
    }

    IEnumerator Accel_IrisBulletLeft()
    {
        speed = 6f;

        while (true)
        {
            if (speed >= 10f)
                break;

            speed += 3 * Time.deltaTime;
            rgbd.velocity = DVector * speed;

            yield return null;
        }
    }
}
