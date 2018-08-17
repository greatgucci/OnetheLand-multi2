using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_default_Range : Bullet 
{
	public void Init_Diana_Bullet2_default_Range(int _shooterNum, int domicil_gameobject)
	{
		photonView.RPC ("Init_Diana_Bullet2_default_Range_RPC", PhotonTargets.All,_shooterNum , domicil_gameobject);
	}
	[PunRPC]
	private void Init_Diana_Bullet2_default_Range_RPC(int _shooterNum, int domicil_gameobject)
	{
		SetTag (type.warning);
		GameObject parent = PhotonView.Find (domicil_gameobject).gameObject;
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		transform.parent = parent.transform;
		DVector = PlayerManager.instance.GetPlayerByNum (shooterNum).aimVector.normalized;
		FavoriteFunction.RotateBullet (gameObject);
		StartCoroutine(MoveDianaSkillLine());
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
	}
	IEnumerator MoveDianaSkillLine()//질문을 해보도록 함. 과연 레이저 형식이 나은지 아니면 탄환이 나은지
	{
		float rotatingAngle;
		float rotatingAngle_Temp = 0f;
		float timer = 0f;

		while (true)
		{

			if (timer > 0.1f)
			{
				break;
			}

			timer += Time.deltaTime;
			yield return null;
		}
		if (PlayerManager.instance.myPnum == shooterNum) {
			Diana_Bullet2_default d_b_d = PhotonNetwork.Instantiate("Diana_Skill2Line", transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet2_default>();
			d_b_d.Init_Diana_Bullet2_default(PlayerManager.instance.myPnum,DVector);
			DestroyToServer();
		}
	}

}

