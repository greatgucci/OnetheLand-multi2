using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_data : Bullet {

	Vector3 start_position_output;
	public void Init_Diana_Bullet2(int _shooterNum, float explosion_scale, float Speed, Vector3 start_position_input, int type, Vector3 dVector, float distance)
	{
		photonView.RPC ("Init_Diana_Bullet2_RPC", PhotonTargets.All, _shooterNum, explosion_scale, Speed, start_position_input, type, dVector, distance);
	}
	[PunRPC]
	private void Init_Diana_Bullet2_RPC(int _shooterNum,float explosion_scale, float Speed, Vector3 start_position_input, int type,Vector3 dVector, float distance)
	{
		float angle;
		shooterNum=_shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		start_position_output = start_position_input;
		if (type == 1)
			angle = Random.Range (-60f, -20f) * Mathf.Deg2Rad;
		else if (type == 2)
			angle = Random.Range (-20f, 20f) * Mathf.Deg2Rad;
		else if (type == 3)
			angle = Random.Range (20f, 60f) * Mathf.Deg2Rad;
		else
			angle = 0f;
		DVector = new Vector3(dVector.x*Mathf.Cos(angle),dVector.y*Mathf.Sin(angle),0);
		FavoriteFunction.RotateBullet(gameObject);
		rgbd.velocity = DVector * Speed;
		StartCoroutine (collision (distance));
	}
	private IEnumerator collision(float distance)
	{
		float distance_current;
		while (true) {
			distance_current = (transform.position-start_position_output).magnitude;
			if(distance_current>distance)
			{
				collision_after ();
				break;
			}
			yield return null;
		}
		DestroyToServer();
	}
	public virtual void collision_after ()
	{
	}
}
