using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_default : Bullet 
{
    bool damaged =false;
	public void Init_Diana_Bullet2_default(int _shooterNum, Vector3 vector)
	{
		photonView.RPC ("Init_Diana_Bullet2_default_RPC", PhotonTargets.All,_shooterNum, vector);
	}
	[PunRPC]
	private void Init_Diana_Bullet2_default_RPC(int _shooterNum, Vector3 vector)
	{
        damaged = false;
        damage = 100;
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
		DVector = vector;
		FavoriteFunction.RotateBullet (gameObject);
		Invoke ("DestroyToServer",0.2f);
	}
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
		if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
		{
			Debug.Log("Graze!");              
			PlayerManager.instance.Local.CurrentSkillGage += 1f;
		}
	}


}
