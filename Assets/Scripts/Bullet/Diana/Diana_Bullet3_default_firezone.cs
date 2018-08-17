using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet3_default_firezone : Bullet 
{
	bool damaged = false;
	bool damaged_current = true;
	bool graze_current = true;
	public void Init_Diana_Bullet3_default_firezone(int _shooterNum)
	{
		photonView.RPC ("Init_Diana_Bullet3_default_firezone_RPC", PhotonTargets.All,_shooterNum);
	}
	[PunRPC]
	private void Init_Diana_Bullet3_default_firezone_RPC(int _shooterNum)
	{
		damage *= 20;
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
		FavoriteFunction.RotateBullet (gameObject);
		Invoke ("DestroyToServer",2f);
	}
	protected override void OnTriggerStay2D(Collider2D collision)
	{
		if (isTirggerTime == true)
		{
			if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
			{
				return;
			}

			if (collision.tag == "Player" + oNum && !damaged)
			{				//데미지 공식 - 레이저의 경우(디스트로이가 안 되는 경우) ( 20 * 초 * 데미지 )
				PlayerManager.instance.Local.CurrentHp -= damage;
				damaged = true;
			} else if (collision.tag == "Player" + oNum && damaged&&damaged_current) {
				PlayerManager.instance.Local.CurrentHp -= (damage*15/20);
				damaged_current = false;
				StartCoroutine (trigertime ());
			}
			if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum&&graze_current)
			{
				PlayerManager.instance.Local.CurrentSkillGage += 1f;
				graze_current = false;
				StartCoroutine (triggertime ());
			}
		}
	}
	IEnumerator trigertime()
	{
		float time=0;
		while (true) {
			time += Time.deltaTime;
			if (time >= 1f||damaged_current) {
				damaged_current = true;
				break;
			}
			yield return null;
		}
	}
	IEnumerator triggertime()
	{
		float time=0;
		while (true) {
			time += Time.deltaTime;
			if (time >= 1f||graze_current) {
				graze_current = true;
				break;
			}
			yield return null;
		}
	}
	public override void DestroyToServer ()
	{
		graze_current = true;
		damaged_current = true;
		photonView.RPC("DestroyToServer_RPC", PhotonTargets.All);
	}
}
