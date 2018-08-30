using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet1_Thunder : Bullet {

	public void Diana_Thunder(int _shooterNum, int type)
	{
		photonView.RPC ("Diana_Thunder_RPC",PhotonTargets.All,_shooterNum, type);
	}
	[PunRPC]
	void Diana_Thunder_RPC(int _shooterNum, int type)
	{
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
		damage = 120;
        if(type==2)
        {
            transform.localScale *= 1.5f;
            damage = 150;
        }
		StartCoroutine (Thunder (type));
	}
	IEnumerator Thunder(int type)
	{
		float timer = 0;
		while (timer < 5f/*몇 초 유지하는지*/) {
			if (timer > 0.1f) {
                if (type == 2)
                {
                    damage = 20;
                }
                else
                    damage = 15;
            }
			timer += Time.deltaTime;
			yield return null;
		}
		if (shooterNum == PlayerManager.instance.myPnum) {
			DestroyToServer ();
		}
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
			}
			if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
			{
				PlayerManager.instance.Local.CurrentSkillGage += 1f;
			}
		}
	}
}
