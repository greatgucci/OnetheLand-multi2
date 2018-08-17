using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill2_default : Skills  
{



	public override void Excute()
	{

		if (isRunning)
		{
			return;
		}
		PhotonView view = GetComponent<PhotonView> ();
		Diana_Bullet2_default_Range d_b_d_t;
		d_b_d_t= PhotonNetwork.Instantiate("Diana_Skill2_Range", transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet2_default_Range>();
		d_b_d_t.Init_Diana_Bullet2_default_Range(PlayerManager.instance.myPnum,view.viewID);

		StartCoroutine(Waiting());
	}

	bool isRunning = false;
	IEnumerator Waiting()
	{
		isRunning = true;
		yield return new WaitForSeconds(1 / delay);
		isRunning = false;
	}

}
