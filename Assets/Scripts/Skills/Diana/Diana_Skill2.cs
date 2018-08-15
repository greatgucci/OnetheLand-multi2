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

		Diana_Bullet2_data diana_bullet2_data;
			
		diana_bullet2_data = PhotonNetwork.Instantiate("Diana_Bullet2_parent", transform.position, Quaternion.identity,0).GetComponent<Diana_Bullet2_data>();

		diana_bullet2_data.GetComponent<Diana_Bullet2_data>().distance = 4f;
		diana_bullet2_data.GetComponent<Diana_Bullet2_data>().explosion_scale = 4f;
		diana_bullet2_data.Init_Diana_Bullet2_parent(PlayerManager.instance.myPnum, transform.position);
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
