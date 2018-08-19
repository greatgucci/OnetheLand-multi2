using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet3_HolyWater : Bullet {

	float scale = 4f;
    float sustaniate_tiem = 4f;
	public void Init_Diana_Bullet3_HolyWater(int _shooterNum, Vector3 direction)
	{
		photonView.RPC ("Init_Diana_Bullet3_HolyWater_RPC", PhotonTargets.All,_shooterNum,direction);
	}
	[PunRPC]
	private void Init_Diana_Bullet3_HolyWater_RPC(int _shooterNum, Vector3 direction)
	{
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
		DVector = direction;
        Invoke("DestroyToServer", sustaniate_tiem);
		FavoriteFunction.RotateBullet(gameObject);
		transform.localScale *= scale;

	}
	protected override void OnTriggerStay2D (Collider2D collision)
	{
		if (isTirggerTime == true)
		{
			Bullet bul;
			bul = collision.gameObject.GetComponent<Bullet> ();
			if ((collision.tag == "Bullet")&&(bul.shooterNum != shooterNum))
			{
				bul.DVector = (collision.transform.position - transform.position).normalized;
				bul.rgbd.velocity=bul.DVector*bul.rgbd.velocity.magnitude;
				FavoriteFunction.RotateBullet(collision.gameObject);
				bul.shooterNum = shooterNum;
			}
		}
	}

}
