using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_child : Diana_Bullet2_data
{
	float explosion_scale = 3f;
	float distance =2f;

	public override void collision_after()
	{
		Diana_Bullet2_explosion diana_bullet2_explosion;
		diana_bullet2_explosion=PhotonNetwork.Instantiate ("Diana_Bullet2_explosion", transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet2_explosion>();
		diana_bullet2_explosion.Init_Diana_Bullet2_explosion(PlayerManager.instance.myPnum, explosion_scale);
	}
}