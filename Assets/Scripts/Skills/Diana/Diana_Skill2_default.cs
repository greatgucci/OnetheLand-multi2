using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill2_default : Skills  
{

	GameObject dianaSkill2Circle;
	GameObject dianaSkill2Target;

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
		dianaSkill2Target = PhotonNetwork.Instantiate("Diana_Skill2_Target", oPostion, Quaternion.identity, 0);
		Bullet bul = dianaSkill2Target.GetComponent<Bullet>();
		view = dianaSkill2Target.GetComponent<PhotonView>();
		bul.Init(PlayerManager.instance.myPnum, view.viewID);

		yield return new WaitForSeconds(1f);

		yield return new WaitForSeconds(0.5f);
		bul = PhotonNetwork.Instantiate("Diana_Skill2Line", transform.position, Quaternion.identity, 0).GetComponent<Bullet>();
		bul.Init(PlayerManager.instance.myPnum, view.viewID);
	}
}
