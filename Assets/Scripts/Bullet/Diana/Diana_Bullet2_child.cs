using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_child : Diana_Bullet2_data
{
	float angle;
	int distance =2;
	Vector3 start_position;
	protected override void Move(int _shooterNum)
	{
		explosion_scale = 3f;
		speed =7f;
		start_position = transform.position;
		if (commuObject.GetComponent<Diana_Bullet2_data> ().type == 1)
			angle = Random.Range (-60f, -20f)*Mathf.Deg2Rad;
		else if(commuObject.GetComponent<Diana_Bullet2_data> ().type==2)
			angle = Random.Range (-20f, 20f)*Mathf.Deg2Rad;
		else
			angle = Random.Range (20f, 60f)*Mathf.Deg2Rad;
		DVector = new Vector3(commuObject.GetComponent<Bullet>().DVector.x*Mathf.Cos(angle),commuObject.GetComponent<Bullet>().DVector.y*Mathf.Sin(angle),0);
		FavoriteFunction.RotateBullet(gameObject);
		rgbd.velocity = DVector * speed;
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
				break;
			}
			yield return null;
		}
		DestroyToServer();
	}
}