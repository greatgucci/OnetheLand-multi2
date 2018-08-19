using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill4_default : Skills
{
	int[] angle = {-6,-3,0,3,6};
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
        Vector3 destination;
		for (int i = 0; i < 6; i++) {
			Diana_Bullet4_default d_b_d = PhotonNetwork.Instantiate
				("Diana_Bullet4_default", transform.position, Quaternion.identity,0).
				GetComponent<Diana_Bullet4_default>();
            destination = PlayerManager.instance.Local.aimPosition;
			d_b_d.Init_Diana_Bullet4_default(PlayerManager.instance.myPnum, destination);
			yield return new WaitForSeconds (0.2f);
		}
		yield return new WaitForSeconds (0.3f);
		for (int i = 0; i < 5; i++) {
			Diana_Bullet4_default d_b_d = PhotonNetwork.Instantiate
				("Diana_Bullet4_default", transform.position, Quaternion.identity,0).
				GetComponent<Diana_Bullet4_default>();
            destination = PlayerManager.instance.Local.aimPosition;
            d_b_d.Init_Diana_Bullet4_default(PlayerManager.instance.myPnum,angle[i],destination);
		}
		yield return null;
	}
}
