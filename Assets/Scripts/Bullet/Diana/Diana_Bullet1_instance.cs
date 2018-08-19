using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet1_instance : Bullet {

    GameObject commuObject;
	float angle;
	public void Init_Diana_Bullet1_default(int _shooterNum,int direction, int position, Vector3 dVector)
	{
		photonView.RPC ("Init_Diana_Bullet1_default_RPC", PhotonTargets.All,_shooterNum,direction,position, dVector);
	}
	[PunRPC]
	private void Init_Diana_Bullet1_default_RPC(int _shooterNum, int direction, int position, Vector3 dVector)
	{
		SetTag (type.bullet);
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		angle = direction==1 ? 105f* Mathf.Deg2Rad : -105f* Mathf.Deg2Rad;
		DVector = DVector + new Vector3 (Mathf.Cos (angle), Mathf.Sin (angle), 0f);
		DVector = DVector.normalized;
		transform.Translate (DVector*direction*position);
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity = DVector * speed;
        Invoke("DestroyToServer",4f/*sustain time*/);
	}
}

