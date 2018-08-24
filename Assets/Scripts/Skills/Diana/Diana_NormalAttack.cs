using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_NormalAttack : Skills {

	public override void Excute ()
	{
		if (isRunning)
			return;
		Vector3 DVector;
		PhotonView view = GetComponent<PhotonView> ();
		Diana_NormalBullet dan_at;
		DVector = PlayerManager.instance.Local.aimVector.normalized;
		dan_at = PhotonNetwork.Instantiate ("Diana_NormalBullet", transform.position, Quaternion.identity, 0).GetComponent<Diana_NormalBullet> ();
		dan_at.Init_Diana_NormalBullet (PlayerManager.instance.myPnum, view.viewID,DVector, true);
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
