using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill1 : Skills
{
	public override void Excute()
	{
		if(isRunning)
		{
			return;
		}
		StartCoroutine (ShooterBullet ());
		StartCoroutine(Waiting());
	}

	bool isRunning = false;
	IEnumerator Waiting()
	{
		isRunning = true;
		yield return new WaitForSeconds(1 / delay);
		isRunning = false;
	}
	IEnumerator ShooterBullet()
	{
		for (int i = 0; i < 6; i++) {
			Bullet bul = PhotonNetwork.Instantiate("Diana_Bullet1", transform.position, Quaternion.identity,0).GetComponent<Bullet>();
			bul.Init(PlayerManager.instance.myPnum);
			yield return new WaitForSeconds (0.5f);
		}
	}
}
