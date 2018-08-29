using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet_HolyLand :Bullet
{
	GameObject skillObject;
	public void Init_Diana_HolyLand(int _shooterNum, int domicil_num)
	{
		photonView.RPC ("Init_Diana_HolyLand_RPC",PhotonTargets.All,_shooterNum, domicil_num);
	}
	[PunRPC]
	private void Init_Diana_HolyLand_RPC(int _shooterNum, int domicil_num)
	{
		skillObject = PhotonView.Find (domicil_num).gameObject;
		Invoke ("DestroyToServer",7f);
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
        tag = "HolyLand" + shooterNum;

    }
	protected override void OnTriggerStay2D (Collider2D collision)
	{
	}
}