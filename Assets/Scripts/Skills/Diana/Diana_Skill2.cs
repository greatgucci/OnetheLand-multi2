using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill2 : Skills
{
	Diana_Bullet2_data diana_bullet2_data;
	float explosion_scale = 4f;
	float distance =4f;
	int type=4;
	int player;
	public override void Excute()
	{
		if(isRunning)
		{
			return;
		}
		if (PlayerManager.instance.myPnum == 1) 
			player = 1;
		else
			player =-1;
			
		diana_bullet2_data = PhotonNetwork.Instantiate("Diana_Bullet2_parent", transform.position, Quaternion.identity,0).GetComponent<Diana_Bullet2_data>();
		diana_bullet2_data.Init_Diana_Bullet2(PlayerManager.instance.myPnum, explosion_scale, 7f/*bullet_speed*/, transform.position, type, new Vector3(player,0,0), distance);
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
