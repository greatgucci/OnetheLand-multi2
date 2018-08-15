using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill3 : Skills
{
	public override void Excute()
	{
		if(isRunning)
		{
			return;
		}
		Diana_Bullet3 diana_bullet3;
		diana_bullet3=PhotonNetwork.Instantiate("Diana_Bullet3", transform.position, Quaternion.identity,0).GetComponent<Diana_Bullet3>();

		diana_bullet3.Init_Diana_Bullet3(PlayerManager.instance.myPnum);
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
