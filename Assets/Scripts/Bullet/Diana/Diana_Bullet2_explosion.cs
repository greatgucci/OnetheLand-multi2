using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_explosion : Bullet {
	bool damaged=false;
	protected override void OnTriggerStay2D(Collider2D collision)
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
		if ((collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)&&!damaged)
		{
			Debug.Log("Graze!");
			PlayerManager.instance.Local.CurrentSkillGage += 1f;
		}
	}
	public void Init_Diana_Bullet2_explosion(int _shooterNum, float explosion_scale)
	{
		photonView.RPC ("Init_Diana_Bullet2_explosion_RPC", PhotonTargets.All,_shooterNum, explosion_scale);
	}
	[PunRPC]
	private void Init_Diana_Bullet2_explosion_RPC(int _shooterNum, float explosion_scale)
	{
		SetTag (type.Range_Attack);
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		transform.localScale=transform.localScale*explosion_scale;
		Invoke ("DestroyToServer", 0.3f);
	}
}