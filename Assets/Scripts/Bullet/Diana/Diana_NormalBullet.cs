using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_NormalBullet : Bullet{
	GameObject skillObject;

	public void Init_Diana_NormalBullet(int _shooterNum, int domicilnum, Vector3 dVector)
	{
		photonView.RPC ("Init_Diana_NormalBullet_RPC",PhotonTargets.All,_shooterNum, domicilnum, dVector);
	}
	[PunRPC]
	private void Init_Diana_NormalBullet_RPC(int _shooterNum, int domicilnum, Vector3 dVector)
	{
		skillObject = PhotonView.Find (domicilnum).gameObject;
		DVector = dVector;
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
		speed = 20f;
		damage = 100;
		FavoriteFunction.RotateBullet (gameObject);
		rgbd.velocity = DVector * speed;
		Debug.Log (skillObject.transform.parent.gameObject.GetComponent<DianaControl> ().skill_can);

	}
	protected override void OnTriggerStay2D (Collider2D collision)
	{
		if (isTirggerTime == true)
		{
			if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
			{
				return;
			}

			if (collision.tag == "Player" + oNum)
				//데미지 공식 - 레이저의 경우(디스트로이가 안 되는 경우) ( 20 * 초 * 데미지 )
			{
				PlayerManager.instance.Local.CurrentHp -= damage;
				Heresy_Sign ();
				DestroyToServer();
			}
			if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
			{
				PlayerManager.instance.Local.CurrentSkillGage += 1f;
			}
		}
	}
	void Heresy_Sign()
	{
		photonView.RPC ("Heresy_Sign_RPC",PhotonTargets.All);
	}
	[PunRPC]
	void Heresy_Sign_RPC()
	{
		skillObject.transform.parent.GetComponent<DianaControl> ().skill_can = true;
	}
}
