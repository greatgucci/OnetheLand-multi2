using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet4_default : Bullet {


	Vector3 oPosition;
	protected override void Move(int _shooterNum)
	{

		oPosition = PlayerManager.instance.GetPlayerByNum(oNum).transform.position;

		DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity = DVector * speed;
	}

}
