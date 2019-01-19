using System.Collections;
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

        oTransform = GameManager.instance.GetPlayerByNum(oNum).transform;
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

    //degree 값을 입력받으면 radian으로 변환하여 x축 기준으로 그만큼 회전시킨 벡터를 반환합니다.
    public static Vector3 PIVectorCal(float degree)
    {
        float resultAngle;
        Vector3 dVector;

        Debug.Log(degree);

        resultAngle = (2 * Mathf.PI * (degree / 360f));

        dVector.x = Mathf.Cos(resultAngle);
        dVector.y = Mathf.Sin(resultAngle);
        dVector.z = 0f;

        dVector.Normalize();

        return dVector;
    }

    //주어진 이니셜 벡터를 디그리 각도 값으로 회전시킨 벡터 반환
    public static Vector3 VectorRotationWithDegree(Vector3 initialVector, float degAngle)
    {
        Vector3 resultVector;

        float x2, y2;
        float angleWithX;
        float angleSum;

        angleWithX = Vector3.Angle(initialVector, Vector3.right);
        angleSum = angleWithX + degAngle;

        x2 = Mathf.Cos(angleSum * Mathf.Deg2Rad);
        y2 = Mathf.Sin(angleSum * Mathf.Deg2Rad);

        resultVector.x = x2;
        resultVector.y = y2;
        resultVector.z = 0;

        return resultVector;
    }
    
    //궁극기 연출



}
