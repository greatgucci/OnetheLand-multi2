using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet3_HolyWater : Bullet {

	public void Init_Diana_Bullet3_HolyWater(int _shooterNum, int domicile_gameobject)
	{
		photonView.RPC ("Init_Diana_Bullet3_HolyWater_RPC", PhotonTargets.All,_shooterNum,domicile_gameobject);
	}
	[PunRPC]
	private void Init_Diana_Bullet3_HolyWater_RPC(int _shooterNum, int domicile_gameobject)
	{
		speed =7f;
		GameObject commuobject = PhotonView.Find (domicile_gameobject).gameObject;
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		DVector = commuobject.GetComponent<Bullet> ().DVector;
		FavoriteFunction.RotateBullet(gameObject);

	}
	protected override void OnTriggerStay2D (Collider2D collision)
	{
		if (isTirggerTime == true)
		{
			if ((collision.tag != "Player" + oNum)&&(collision.tag != "Player" + shooterNum))
			{
				Bullet bul;
				bul = collision.GetComponent<Bullet> ();
				bul.DVector = (collision.transform.position - transform.position).normalized;
				bul.rgbd.velocity=bul.DVector*bul.rgbd.velocity.magnitude;
				bul.shooterNum = shooterNum;
			}
		}
	}

}
