﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FavoriteFunction : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Vector3 VectorCalc(GameObject thisObject, int oNum)
    {
        Vector3 dVector_ReturnValue;
        Transform oTransform;

        oTransform = PlayerManager.instance.GetPlayerByNum(oNum).transform;
        dVector_ReturnValue = oTransform.position - thisObject.transform.position;
        dVector_ReturnValue.Normalize();

        return dVector_ReturnValue;
    }

    public static float RotateBullet(GameObject thisObject)  //총알을 돌리고 각도 반환합니당
    {
        float rotatingAngle;
        Vector3 dVector = thisObject.GetComponent<Bullet>().DVector;

        rotatingAngle = dVector.y > 0 ? Vector3.AngleBetween(dVector, Vector3.right) : -Vector3.AngleBetween(dVector, Vector3.right);
        thisObject.transform.RotateAround(Vector3.forward, rotatingAngle);

        return rotatingAngle;
    }

    public static GameObject WarningCircle(Vector3 warningPosition, float sizeOfWarning, float warningTime)
    {
        GameObject warningCircle_Temp;

        warningCircle_Temp = Instantiate(Resources.Load("WarningCircle") as GameObject, warningPosition, Quaternion.identity);
        warningCircle_Temp.transform.Find("WarningCircleRange").transform.localScale *= sizeOfWarning;
        warningCircle_Temp.GetComponent<Warning>().warningTimer = warningTime;

        return warningCircle_Temp;
    }

    public static GameObject WarningSquare(Vector3 warningPosition, float sizeOfWarning, float warningTime)
    {
        GameObject warningSquare_Temp;

        warningSquare_Temp = Instantiate(Resources.Load("WarningSquare") as GameObject, warningPosition, Quaternion.identity);
        warningSquare_Temp.GetComponent<Warning>().warningTimer = warningTime;

        return warningSquare_Temp;
    }
}
