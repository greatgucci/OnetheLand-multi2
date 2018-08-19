using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet4_instance : Bullet
{
	public void Init_Diana_Bullet4_instance(int _shooterNum, int timer,int type_s)
    {
		photonView.RPC("Init_Diana_Bullet4_instance_RPC", PhotonTargets.All, _shooterNum, timer,type_s);
    }
    [PunRPC]
	protected void Init_Diana_Bullet4_instance_RPC(int _shooterNum, int timer, int type_s)
    {
		Invoke ("DestroyToServer",5f);
        SetTag(type.bullet);
		speed =7f;
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		DVector = new Vector3 (Mathf.Cos((45+timer*30+90*type_s) * Mathf.Deg2Rad), Mathf.Sin((45+timer*30+90*type_s) * Mathf.Deg2Rad), 0f);
		FavoriteFunction.RotateBullet(gameObject);
		rgbd.velocity = DVector.normalized * speed;
    }
}
