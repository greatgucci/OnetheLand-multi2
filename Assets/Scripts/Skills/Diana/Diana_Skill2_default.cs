using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill2_default : Skills  
{


	Vector3 oPostion;

	public override void Excute()
	{

		if (isRunning)
		{
			return;
		}

		StartCoroutine(DianaSkill2());

		StartCoroutine(Waiting());
	}

	bool isRunning = false;
	IEnumerator Waiting()
	{
		isRunning = true;
		yield return new WaitForSeconds(1 / delay);
		isRunning = false;
	}

	IEnumerator DianaSkill2()
	{
		PhotonView view;
		oPostion = PlayerManager.instance.Opponent.transform.position;
		Diana_Bullet2_default_Target d_b_d_t = PhotonNetwork.Instantiate("Diana_Skill2_Target", oPostion, Quaternion.identity, 0).GetComponent<Diana_Bullet2_default_Target>();
		d_b_d_t.Init_Diana_Bullet2_default_Target(PlayerManager.instance.myPnum);

		yield return new WaitForSeconds(1f);

		yield return new WaitForSeconds(0.5f);
		Diana_Bullet2_default d_b_d = PhotonNetwork.Instantiate("Diana_Skill2Line", transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet2_default>();
		d_b_d.Init_Diana_Bullet2_default(PlayerManager.instance.myPnum);
	}
}
