using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_default_Target : Bullet 
{

	public void Init_Diana_Bullet2_default_Target(int _shooterNum)
	{
		photonView.RPC ("Init_Diana_Bullet2_default_Target_RPC", PhotonTargets.All,_shooterNum);
	}
	[PunRPC]
	private void Init_Diana_Bullet2_default_Target_RPC(int _shooterNum)
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
		StartCoroutine(DianaSKill2Targeting());
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
	}

	IEnumerator DianaSKill2Targeting()
	{
		float timer = 0;
		while(true)
		{
			timer += Time.deltaTime;
			if (timer > 1f) {
				yield return new WaitForSeconds (0.6f);
				break;
			} else {
				transform.position = PlayerManager.instance.GetPlayerByNum (oNum).transform.position;
				yield return null;
			}
		}
		DestroyToServer ();
	}

}

