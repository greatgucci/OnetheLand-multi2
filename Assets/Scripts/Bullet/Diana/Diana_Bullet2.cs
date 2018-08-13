using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2 :Diana_Bullet2_data {
	float angle;
	float explosion_scale = 4f;
	float distance =4f;
	int type;

	public override void collision_after ()
	{
		Diana_Bullet2_data diana_bullet2_data;
		Diana_Bullet2_explosion diana_bullet2_explosion;
		diana_bullet2_explosion=PhotonNetwork.Instantiate ("Diana_Bullet2_explosion", transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet2_explosion>();
		diana_bullet2_explosion.Init_Diana_Bullet2_explosion(PlayerManager.instance.myPnum, explosion_scale);
		diana_bullet2_data = PhotonNetwork.Instantiate("Diana_Bullet2_child", transform.position, Quaternion.identity,0).GetComponent<Diana_Bullet2_data>();
		type=1;
		diana_bullet2_data.Init_Diana_Bullet2(PlayerManager.instance.myPnum, explosion_scale, speed=7f, transform.position, type, DVector, distance);
		diana_bullet2_data = PhotonNetwork.Instantiate("Diana_Bullet2_child", transform.position, Quaternion.identity,0).GetComponent<Diana_Bullet2_data>();
		type = 2;
		diana_bullet2_data.Init_Diana_Bullet2(PlayerManager.instance.myPnum, explosion_scale, speed=7f, transform.position, type, DVector, distance);
		diana_bullet2_data = PhotonNetwork.Instantiate("Diana_Bullet2_child", transform.position, Quaternion.identity,0).GetComponent<Diana_Bullet2_data>();
		type = 3;
		diana_bullet2_data.Init_Diana_Bullet2(PlayerManager.instance.myPnum, explosion_scale, speed=7f, transform.position, type, DVector, distance);
	}
}