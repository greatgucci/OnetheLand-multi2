using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet1 : Bullet {
	public int direction;
	public int position;
	float timer = 0;
	protected override void Move(int _shooterNum)
	{
		DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
		FavoriteFunction.RotateBullet(gameObject);
		speed = 6f;
		rgbd.velocity = DVector * speed;
		StartCoroutine (shooterBullet ());
	}
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
		{
			return;
		}

		if (collision.tag == "Player" + oNum)
		{
			PlayerManager.instance.Local.CurrentHp -= damage;
			timer = 8;
		}
	}
	IEnumerator shooterBullet()
	{
		PhotonView view;
		Bullet bul;
		Vector3 position;
		view = GetComponent<PhotonView>();
		while (true) {
			if (timer > 7) {
				break;
			}
			position = transform.position;
			bul = PhotonNetwork.Instantiate("Diana_Bullet1_instance",position, Quaternion.identity,0).GetComponent<Bullet>();
			direction = 1;
			this.position = 1;
			bul.Init(shooterNum,view.viewID);
			bul = PhotonNetwork.Instantiate("Diana_Bullet1_instance",position, Quaternion.identity,0).GetComponent<Bullet>();
			direction = -1;
			this.position = 1;
			bul.Init(shooterNum,view.viewID);
			bul = PhotonNetwork.Instantiate("Diana_Bullet1_instance",position, Quaternion.identity,0).GetComponent<Bullet>();
			direction = 1;
			this.position = 0;
			bul.Init(shooterNum,view.viewID);
			bul = PhotonNetwork.Instantiate("Diana_Bullet1_instance",position, Quaternion.identity,0).GetComponent<Bullet>();
			direction = -1;
			this.position = 0;
			bul.Init(shooterNum,view.viewID);
			yield return new WaitForSeconds (0.5f);
		}
		DestroyToServer();
	}
}

