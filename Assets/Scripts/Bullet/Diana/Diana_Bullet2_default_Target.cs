using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_default_Target : Bullet 
{
	protected override void Move(int _shooterNum)
	{
		StartCoroutine(DianaSKill2Targeting());
	}

	protected override void OnTriggerStay2D(Collider2D collision)
	{
	}

	IEnumerator DianaSKill2Targeting()
	{
		float timer = 0;
		while(true)
		{
			timer += Time.deltaTime;
			if (timer > 1f) {
				yield return new WaitForSeconds (0.6f);
				break;
			} else {
				transform.position = PlayerManager.instance.GetPlayerByNum (oNum).transform.position;
				yield return null;
			}
		}
		DestroyToServer ();
	}

}

