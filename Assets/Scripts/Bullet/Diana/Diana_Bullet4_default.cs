using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet4_default : Bullet {


	Vector3 oPosition;
	public void Init_Diana_Bullet4_default(int _shooterNum)
	{
		photonView.RPC ("Init_Diana_Bullet4_default_RPC", PhotonTargets.All,_shooterNum);
	}
	[PunRPC]
	private void Init_Diana_Bullet4_default_RPC(int _shooterNum)
	{
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		oPosition = PlayerManager.instance.GetPlayerByNum(oNum).transform.position;

		DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity = DVector * speed;
	}
}
