using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet3_default : Bullet 
{
	Vector3 destination;
	Vector3 position_temp;
	float distance;
	float distance_temp;
	public void Init_Diana_Bullet3_default(int _shooterNum,Vector3 vector3)
	{
		photonView.RPC ("Init_Diana_Bullet3_default_RPC", PhotonTargets.All,_shooterNum, vector3);
	}
	[PunRPC]
	private void Init_Diana_Bullet3_default_RPC(int _shooterNum, Vector3 vector3)
	{
		SetTag (type.bullet);
		shooterNum = _shooterNum;
		speed = 10f;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		DVector = PlayerManager.instance.GetPlayerByNum (shooterNum).aimVector.normalized;
		destination = PlayerManager.instance.GetPlayerByNum (shooterNum).aimPosition;
		FavoriteFunction.RotateBullet (gameObject);
		rgbd.velocity = DVector * speed;
		StartCoroutine (shootbullet (vector3));
	}
	private IEnumerator shootbullet(Vector3 vector3)
	{
		position_temp = transform.position;
		distance = Mathf.Abs ((transform.position - destination).magnitude);
		if (PlayerManager.instance.myPnum != shooterNum) {
			GameObject warning=Instantiate (Resources.Load("Diana_Bullet3_default_Warning") as GameObject,vector3,Quaternion.identity);
			warning.GetComponent<Diana_Bullet3_default_Warninig> ().position=vector3;
			warning.GetComponent<Bullet> ().shooterNum = shooterNum;
			warning.GetComponent<Bullet> ().oNum = oNum;
		}
		while (true) {
			distance_temp += Mathf.Abs((transform.position - position_temp).magnitude);
			if(distance<distance_temp)
			{
				if (PlayerManager.instance.myPnum == shooterNum) {
					Diana_Bullet3_default_firezone d_b_d_w = PhotonNetwork.Instantiate 
						("Diana_Bullet3_default_firezone", destination, Quaternion.identity, 0).GetComponent<Diana_Bullet3_default_firezone> ();
					d_b_d_w.Init_Diana_Bullet3_default_firezone (PlayerManager.instance.myPnum);
					Debug.Log ("Hello");
				}
				break;
			}
			position_temp = transform.position;
			yield return null;
		}
		if (PlayerManager.instance.myPnum == shooterNum)
			DestroyToServer ();
	}

}
