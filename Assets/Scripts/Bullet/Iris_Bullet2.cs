using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet2 : Bullet_Plural {

    Vector3 oPosition_Temp;

    public int irisBullet2Num_Temp;

    float rotatingAngle = -(3.14f / 9f);

    protected override void Move(int _shooterNum)
    {
        irisBullet2Num_Temp = bulNum;

        Debug.Log("Move : " + irisBullet2Num_Temp);

        speed = 8f;

        rotatingAngle += (irisBullet2Num_Temp * (3.14f / 18f));

        DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
        rotatingAngle += DVector.y > 0 ? -Vector3.AngleBetween(Vector3.right, DVector) : Vector3.AngleBetween(Vector3.right, DVector);

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
