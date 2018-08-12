using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill2 : Skills
{
	public override void Excute()
	{
		if(isRunning)
		{
			return;
		}
		Bullet bul = PhotonNetwork.Instantiate("Diana_Bullet2_parent", transform.position, Quaternion.identity,0).GetComponent<Bullet>();
		bul.Init(PlayerManager.instance.myPnum);
		StartCoroutine(Waiting());
	}

	bool isRunning = false;
	IEnumerator Waiting()
	{
		isRunning = true;
		yield return new WaitForSeconds(1 / delay);
		isRunning = false;
	}
}
