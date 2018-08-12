using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet1_instance : Bullet {

	Vector3 oPosition;
	float angle;
	protected override void Move(int _shooterNum)
	{
		angle= commuObject.GetComponent<Diana_Bullet1>().direction==1 ? 75f* Mathf.Deg2Rad : -75f* Mathf.Deg2Rad;
		DVector = commuObject.GetComponent<Diana_Bullet1>().DVector + new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0f);
		DVector = DVector.normalized;
		transform.Translate (DVector*commuObject.GetComponent<Diana_Bullet1>().direction*commuObject.GetComponent<Diana_Bullet1>().position);
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity = DVector * speed;
	}
}

