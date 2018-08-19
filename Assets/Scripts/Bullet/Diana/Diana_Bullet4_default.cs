using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet4_default : Bullet {

	public void Init_Diana_Bullet4_default(int _shooterNum, Vector3 destination)
	{
		photonView.RPC ("Init_Diana_Bullet4_default_RPC", PhotonTargets.All,_shooterNum, destination);
	}
	[PunRPC]
	private void Init_Diana_Bullet4_default_RPC(int _shooterNum, Vector3 destination)
	{
		SetTag (type.bullet);
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		DVector = (destination.normalized-transform.position.normalized).normalized;
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity =  DVector* speed;
	}
	public void Init_Diana_Bullet4_default(int _shooterNum, int angle, Vector3 destination)
	{
		photonView.RPC ("Init_Diana_Bullet4_default_RPC", PhotonTargets.All,_shooterNum, angle*Mathf.Deg2Rad, destination);
	}
	[PunRPC]
	private void Init_Diana_Bullet4_default_RPC(int _shooterNum, float angle, Vector3 destination)
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
		DVector = (destination.normalized-transform.position.normalized).normalized+new Vector3(0f,Mathf.Sin(angle),0f);
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity =  DVector* speed;
	}
}
