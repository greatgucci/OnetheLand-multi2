using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_Warning : Bullet {
	Vector3 start_position;
	public void Init_Diana_Bullet2_Warning(int _shooterNum, float distance,Vector3 dVector)
	{
		photonView.RPC ("Init_Diana_Bullet2_Warning_RPC", PhotonTargets.All,_shooterNum, distance, dVector);
	}
	[PunRPC]
	private void Init_Diana_Bullet2_Warning_RPC(int _shooterNum, float distance, Vector3 dVector)
	{
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		start_position = transform.position;
		speed = 10000;
		DVector = dVector;
		FavoriteFunction.RotateBullet(gameObject);
		rgbd.velocity = DVector * speed;
		StartCoroutine (stay(distance));
	}
	protected override void OnTriggerStay2D (Collider2D collision)
	{
	}
	IEnumerator stay(float distance)
	{
		while (true) {
			
			yield return null;
			if ((transform.position - start_position).magnitude > distance)
			{
				rgbd.velocity = new Vector2(0,0);
				Invoke ("DestroyToServer",2f);
				break;
			}
		}
	}
}
