using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill4_default : Skills
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
		Vector3 position_temp = transform.position;
		for (int i = 0; i < 5; i++) {
			Bullet bul = PhotonNetwork.Instantiate("Diana_Bullet4_default", position_temp+new Vector3(0,1f*Mathf.Abs(i-3),0), Quaternion.identity,0).GetComponent<Bullet>();
			bul.Init(PlayerManager.instance.myPnum);
		}
		yield return null;
	}
}
