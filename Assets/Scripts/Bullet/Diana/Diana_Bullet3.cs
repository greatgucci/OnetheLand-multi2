using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet3 : Bullet {


	public void Init_Diana_Bullet3(int _shooterNum)
	{
		photonView.RPC ("Init_Diana_Bullet3_RPC", PhotonTargets.All, _shooterNum);
	}
	[PunRPC]
	void Init_Diana_Bullet3_RPC(int _shooterNum)
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
		DVector = PlayerManager.instance.Local.aimVector.normalized*3;
		FavoriteFunction.RotateBullet(gameObject);
		transform.Translate(0f,0f,0f);
		if (PlayerManager.instance.Local.playerNum == shooterNum) {
			Spinkle_HolyWater();
		}
	}
	protected override void OnTriggerStay2D (Collider2D collision)
	{
	}
	void Spinkle_HolyWater()//성수 뿌리기
	{
		PhotonView view;
		view = GetComponent<PhotonView> ();
		Diana_Bullet3_HolyWater d_b_h_w;
		d_b_h_w = PhotonNetwork.Instantiate ("Diana_Bullet3_HolyWater", transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet3_HolyWater> ();
		d_b_h_w.Init_Diana_Bullet3_HolyWater (PlayerManager.instance.myPnum,view.viewID);
	}
}
