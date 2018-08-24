using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet_HolyLand :Bullet
{
	GameObject skillObject;
	public void Init_Diana_HolyLand(int _shooterNum, int domicil_num)
	{
		photonView.RPC ("Init_Diana_HolyLand_RPC",PhotonTargets.All,_shooterNum, domicil_num);
	}
	[PunRPC]
	private void Init_Diana_HolyLand_RPC(int _shooterNum, int domicil_num)
	{
		skillObject = PhotonView.Find (domicil_num).gameObject;
		Invoke ("DestroyToServer",7f);
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
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
				Heresy_Sign ();
				SetActiveToServer(true);
				skillObject.transform.parent.GetComponent<DianaControl> ().idannakin.Impact ();
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
	}public void SetActiveToServer(bool ing)
	{
		photonView.RPC ("SetActiveToServer_RPC", PhotonTargets.All, ing);
	}
	[PunRPC]
	void SetActiveToServer_RPC(bool ing)
	{
		skillObject.transform.parent.GetComponent<DianaControl>().idannakin.gameObject.SetActive (ing);
	}
}