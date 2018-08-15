using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet1_default : Bullet {

	public void Init_Diana_Bullet1_default(int _shooterNum)
	{
		photonView.RPC ("Init_Diana_Bullet1_default_RPC", PhotonTargets.All,_shooterNum);
	}
	[PunRPC]
	private void Init_Diana_Bullet1_default_RPC(int _shooterNum)
	{
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
		rgbd.velocity = PlayerManager.instance.Local.aimVector.normalized * speed;
	}


}
