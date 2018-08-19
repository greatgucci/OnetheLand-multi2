using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet4_Cross : Bullet
{
	public void Init_Diana_Bullet4_Cross(int _shooterNum, Vector3 dVector)
	{
		photonView.RPC("Init_Diana_Bullet4_Cross_RPC", PhotonTargets.All, _shooterNum, dVector);
	}
	[PunRPC]
	protected void Init_Diana_Bullet4_Cross_RPC(int _shooterNum, Vector3 dVector)
	{
		SetTag(type.Range_Attack);
		DVector = dVector;
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		FavoriteFunction.RotateBullet(gameObject);
	}
	protected override void OnTriggerStay2D (Collider2D collision)
	{
		base.OnTriggerStay2D (collision);
	} 
}
