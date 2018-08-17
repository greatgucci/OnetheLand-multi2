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
		DVector = (PlayerManager.instance.Local.aimPosition-transform.position).normalized;
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity =  DVector* speed;
	}
	public void Init_Diana_Bullet4_default(int _shooterNum, int angle)
	{
		photonView.RPC ("Init_Diana_Bullet4_default_RPC", PhotonTargets.All,_shooterNum, angle*Mathf.Deg2Rad);
	}
	[PunRPC]
	private void Init_Diana_Bullet4_default_RPC(int _shooterNum, float angle)
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
		DVector = (PlayerManager.instance.Local.aimPosition-transform.position).normalized+new Vector3(0f,Mathf.Sin(angle),0f);
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity =  DVector* speed;
	}
}
