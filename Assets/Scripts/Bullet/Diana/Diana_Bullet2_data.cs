using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_data : Bullet {
	public GameObject commuObject;
	public float explosion_scale;
	public Vector3 start_position_input;
	public int type_bullet;
	public float distance;
	Vector3 start_position_output;
	public void Init_Diana_Bullet2(int _shooterNum, int domicile_gameObject)
	{
		photonView.RPC ("Init_Diana_Bullet2_RPC", PhotonTargets.All, _shooterNum, domicile_gameObject);
	}
	public void Init_Diana_Bullet2_parent(int _shooterNum, Vector3 position)
	{
		photonView.RPC ("Init_Diana_Bullet2_parent_RPC", PhotonTargets.All, _shooterNum, position);
	}
	[PunRPC]
	protected void Init_Diana_Bullet2_RPC(int _shooterNum, int domicile_gameObject)
	{
		SetTag (type.bullet);
		float angle;
		int type_C;
		Vector3 dVector;
		speed = 7f;
		commuObject = PhotonView.Find (domicile_gameObject).gameObject;
		dVector = commuObject.GetComponent<Bullet> ().DVector.normalized;
		type_C = commuObject.GetComponent<Diana_Bullet2_data> ().type_bullet;
		shooterNum=_shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		if (dVector.y == 0) {
			dVector.y = 1;
		}
		start_position_output = commuObject.GetComponent<Diana_Bullet2_data>().start_position_input;
		if (type_C == 1)
			angle = Random.Range (-60f, -20f) * Mathf.Deg2Rad;
		else if (type_C == 2)
			angle = Random.Range (-20f, 20f) * Mathf.Deg2Rad;
		else
			angle = Random.Range (20f, 60f) * Mathf.Deg2Rad;
		DVector += new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0);
		FavoriteFunction.RotateBullet(gameObject);
		rgbd.velocity = new Vector2(DVector.x,DVector.y) * speed;
		StartCoroutine (collision (_shooterNum, DVector));
	}
	[PunRPC]
	protected void Init_Diana_Bullet2_parent_RPC(int _shooterNum, Vector3 position)
	{

		distance = 4f;
		explosion_scale = 4f;
		speed = 7f;
		shooterNum=_shooterNum;
		DVector = PlayerManager.instance.Local.aimVector.normalized;
		start_position_output = position;
		FavoriteFunction.RotateBullet(gameObject);
		rgbd.velocity = new Vector2(DVector.x,DVector.y) * speed;
		StartCoroutine (collision (_shooterNum, DVector));
	}
	protected IEnumerator collision(int _shooterNum, Vector3 dVector)
	{
		float distance_current=0;
		SetTag (type.bullet);
		Diana_Bullet2_Warning d_b_w;
		d_b_w = PhotonNetwork.Instantiate ("Diana_Bullet2_Warning",transform.position, Quaternion.identity,0).GetComponent<Diana_Bullet2_Warning>();
		d_b_w.Init_Diana_Bullet2_Warning (_shooterNum, distance, dVector);
		while (true) {
			distance_current = (transform.position-start_position_output).magnitude;
			if(distance_current>distance)
			{
				if(PlayerManager.instance.myPnum == _shooterNum)
					collision_after ();
				break;
			}
			yield return null;
		}
		if(PlayerManager.instance.myPnum == _shooterNum)
			DestroyToServer();
	}
	public virtual void collision_after ()
	{
	}
}
