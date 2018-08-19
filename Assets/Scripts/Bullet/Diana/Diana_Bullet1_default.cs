using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet1_default : Bullet {

	public void Init_Diana_Bullet1_default(int _shooterNum, int angle, Vector3 position)
	{
		photonView.RPC ("Init_Diana_Bullet1_default_RPC", PhotonTargets.All,_shooterNum, angle, position);
	}
	[PunRPC]
	private void Init_Diana_Bullet1_default_RPC(int _shooterNum, int angle, Vector3 position)
	{
		SetTag (type.bullet);
		speed =7f;
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		DVector = (position.normalized-transform.position.normalized).normalized+ new Vector3 (0f,Mathf.Sin(angle*Mathf.Deg2Rad));
		FavoriteFunction.RotateBullet(gameObject);
		rgbd.velocity = DVector.normalized * speed;
	}


}
