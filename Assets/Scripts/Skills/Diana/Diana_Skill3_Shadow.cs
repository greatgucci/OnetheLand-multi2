using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill3_Shadow : Skills{

	public override void Excute ()
	{
		if (isRunning)
			return;
		Diana_Skill3_Impact impact;
		PhotonView view;
		view = GetComponent<PhotonView> ();
		impact = PhotonNetwork.Instantiate ("Diana_shadow",transform.position,Quaternion.identity,0).GetComponent<Diana_Skill3_Impact>();
		impact.shadow (PlayerManager.instance.myPnum, view.viewID);
		StartCoroutine (Waiting ());
	}
	bool isRunning = false;
	IEnumerator Waiting()
	{
		isRunning = true;
		yield return new WaitForSeconds(1 / delay);
		isRunning = false;
	}

}