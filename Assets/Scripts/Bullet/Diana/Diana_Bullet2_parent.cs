using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_parent : Diana_Bullet2_data {
	Vector3 start_position;
	protected override void Move(int _shooterNum)
	{
		explosion_scale=6f;
		speed =7f;
		distance = 8f;
		start_position = transform.position;
		if(shooterNum == 1)
		{
			rgbd.velocity = new Vector2(speed, 0);
		}else
		{
			transform.localScale = new Vector3(-1, 1, 1);
			rgbd.velocity = new Vector2(-speed, 0);
		}
		StartCoroutine (collision ());
	}
	IEnumerator collision()
	{
		Bullet bul;
		PhotonView view;
		float distance;
		view = GetComponent<PhotonView> ();
		while (true) {
			distance = (transform.position-start_position).magnitude;
			if(distance>this.distance)
			{
				bul=PhotonNetwork.Instantiate ("Diana_Bullet2_explosion", transform.position, Quaternion.identity, 0).GetComponent<Bullet>();
				bul.Init(PlayerManager.instance.myPnum,view.viewID);
				bul = PhotonNetwork.Instantiate("Diana_Bullet2", transform.position, Quaternion.identity,0).GetComponent<Bullet>();
				type = 1;
				bul.Init(PlayerManager.instance.myPnum,view.viewID);
				bul = PhotonNetwork.Instantiate("Diana_Bullet2", transform.position, Quaternion.identity,0).GetComponent<Bullet>();
				type = 2;
				bul.Init(PlayerManager.instance.myPnum,view.viewID);
				bul = PhotonNetwork.Instantiate("Diana_Bullet2", transform.position, Quaternion.identity,0).GetComponent<Bullet>();
				type = 3;
				bul.Init(PlayerManager.instance.myPnum,view.viewID);
				break;
			}
			yield return null;
		}
		DestroyToServer();
	}
}