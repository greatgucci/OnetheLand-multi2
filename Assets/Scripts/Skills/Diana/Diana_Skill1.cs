using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill1 : Skills
{
	Diana_Bullet1 diana_bullet1;
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
			diana_bullet1 = PhotonNetwork.Instantiate("Diana_Bullet1",transform.position,Quaternion.identity,0).GetComponent<Diana_Bullet1>();
            Vector3 dVector = PlayerManager.instance.Local.aimVector;
            diana_bullet1.Init_Diana_Bullet1(PlayerManager.instance.myPnum, dVector);
			yield return new WaitForSeconds (0.5f);
		}
	}
}
