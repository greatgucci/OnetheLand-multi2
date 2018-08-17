using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_default : Bullet 
{
    GameObject commuObject;
    bool damaged =false;
	public void Init_Diana_Bullet2_default(int _shooterNum)
	{
		photonView.RPC ("Init_Diana_Bullet2_default_RPC", PhotonTargets.All,_shooterNum);
	}
	[PunRPC]
	private void Init_Diana_Bullet2_default_RPC(int _shooterNum)
	{
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		StartCoroutine(MoveDianaSkillLine());
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

	IEnumerator MoveDianaSkillLine()//질문을 해보도록 함. 과연 레이저 형식이 나은지 아니면 탄환이 나은지
	{
		float timer = 0f;

		while (true)
		{

			if (timer > 0.1f)
			{
				break;
			}

			DVector = PlayerManager.instance.Local.aimVector.normalized;
			transform.Rotate(Vector3.forward, DVector);

			timer += Time.deltaTime;
			yield return null;
		}
		DestroyToServer();
	}

}
