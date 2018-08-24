using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet_Thunder_Create : Bullet {

	public void Diana_Thunder_Create(int _shooterNum, Vector3 dVector)
	{
		photonView.RPC ("Diana_Thunder_Create_RPC",PhotonTargets.All,_shooterNum, dVector);
	}
	[PunRPC]
	void Diana_Thunder_Create_RPC(int _shooterNum, Vector3 dVector)
	{
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
		DVector = dVector.normalized;
		speed = 8f;
		FavoriteFunction.RotateBullet (gameObject);
		rgbd.velocity = DVector * speed;
		StartCoroutine (Throw_Thunder ());
	}
	IEnumerator Throw_Thunder()
	{
		float i = 0f;
		float length = 0;
		Vector3 start_position = transform.position;
		while (true) 
		{
			length += (transform.position - start_position).magnitude;
			if (length >= 0.3f * i) {
				//범위 보여줌
				yield return new WaitForSeconds(0.1f);
				if (PlayerManager.instance.myPnum == shooterNum) {

                    AudioController.instance.PlayEffectSound(Character.DIANA, 2);
                    Diana_Bullet1_Thunder thunder;
					thunder = PhotonNetwork.Instantiate ("Diana_Thunder", transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet1_Thunder>();
					thunder.Diana_Thunder (shooterNum, 1);
				}
				i+=1f;
				if(i>4)
					break;
			}
			start_position = transform.position;
			yield return null;
		}
		if (PlayerManager.instance.myPnum == shooterNum) {
			DestroyToServer ();
		}
	}
	protected override void OnTriggerStay2D (Collider2D collision)
	{
	}
}
