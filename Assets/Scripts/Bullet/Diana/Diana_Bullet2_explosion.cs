using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_explosion : Bullet {
	bool damaged=false;
	protected override void Move(int _shooterNum)
	{
		transform.localScale=transform.localScale*commuObject.GetComponent<Diana_Bullet2_data> ().explosion_scale;
		Invoke ("DestroyToServer", 0.3f);
	}
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
		if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
		{
			return;
		}

		if (collision.tag == "Player" + oNum&&!damaged)//무적이 있을시 그대로 없을시 1회만 적용을 넣어야 됨
		{
			PlayerManager.instance.Local.CurrentHp -= damage;
			damaged = true;
		}
	}
}