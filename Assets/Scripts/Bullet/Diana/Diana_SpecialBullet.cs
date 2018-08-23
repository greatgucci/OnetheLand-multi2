using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_SpecialBullet : Bullet{
	public void Init_Diana_SpecialBullet(int _shooterNum, int type, Vector3 dVector)
	{
		photonView.RPC ("Init_Diana_SpecialBullet_RPC",PhotonTargets.All,_shooterNum, type, dVector);
	}
	[PunRPC]
	private void Init_Diana_SpecialBullet_RPC(int _shooterNum, int type, Vector3 dVector)
	{
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
		speed = 15f;
		damage = 50;
		float angle = dVector.y>0 ? Vector3.Angle (Vector3.right, dVector)*Mathf.Deg2Rad : -Vector3.Angle (Vector3.right, dVector)*Mathf.Deg2Rad;
		angle += type * 4*Mathf.Deg2Rad;
		DVector = new Vector3(Mathf.Cos(angle),Mathf.Sin(angle),0f);
		FavoriteFunction.RotateBullet (gameObject);
		rgbd.velocity = DVector * speed;
		StartCoroutine(Scale_setting());

	}
    IEnumerator Scale_setting()
    {
		while (true) {
			transform.localScale *= 0.75f;
			speed *= 0.9f;
			rgbd.velocity = DVector * speed;
			if(transform.localScale.magnitude<0.3)
			{
				break;
			}
			yield return new WaitForSeconds(0.07f);
		}
		if (PlayerManager.instance.myPnum == shooterNum) {
			DestroyToServer();
		}
    }
}
