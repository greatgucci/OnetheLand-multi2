using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_default : Bullet 
{
	bool damaged=false;
	protected override void Move(int _shooterNum)
	{
		StartCoroutine(MoveDianaSkillLine());
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

			DVector = commuObject.transform.position - transform.position;
			DVector.Normalize();

			rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
			transform.Rotate(Vector3.forward, rotatingAngle - rotatingAngle_Temp);
			rotatingAngle_Temp = rotatingAngle;

			timer += Time.deltaTime;
			yield return null;
		}
		DestroyToServer();
	}

}
