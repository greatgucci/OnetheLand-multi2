using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill3_default : Skills
{
	public override void Excute()
	{
		if(isRunning)
		{
			return;
		}
		Diana_Bullet3_default diana_bullet3;
		Vector3 vector3 = PlayerManager.instance.Local.aimPosition;
		diana_bullet3=PhotonNetwork.Instantiate("Diana_Bullet3_default", transform.position, Quaternion.identity,0).GetComponent<Diana_Bullet3_default>();
		diana_bullet3.Init_Diana_Bullet3_default(PlayerManager.instance.myPnum,vector3);
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
