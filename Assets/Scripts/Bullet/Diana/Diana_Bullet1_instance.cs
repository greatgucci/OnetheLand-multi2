using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet1_instance : Bullet {

    GameObject commuObject;
	Vector3 oPosition;
	float angle;
	public void Init_Diana_Bullet1_default(int _shooterNum,int communicatingObject)
	{
		photonView.RPC ("Init_Diana_Bullet1_default_RPC", PhotonTargets.All,_shooterNum,communicatingObject);
	}
	[PunRPC]
	private void Init_Diana_Bullet1_default_RPC(int _shooterNum, int communicatingObject)
	{
		SetTag (type.bullet);
		shooterNum = _shooterNum;
		commuObject = PhotonView.Find(communicatingObject).gameObject;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		angle= commuObject.GetComponent<Diana_Bullet1>().direction==1 ? 75f* Mathf.Deg2Rad : -75f* Mathf.Deg2Rad;
		DVector = commuObject.GetComponent<Diana_Bullet1>().DVector + new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0f);
		DVector = DVector.normalized;
		transform.Translate (DVector*commuObject.GetComponent<Diana_Bullet1>().direction*commuObject.GetComponent<Diana_Bullet1>().position);
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity = DVector * speed;
	}
}

