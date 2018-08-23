using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill2_HolyLand : Skills {

	PhotonView view;
	public override void Excute ()
	{
		if (isRunning)
			return;
		view = GetComponent<PhotonView> ();
		StartCoroutine (Casting_Pray ());
		StartCoroutine (Waiting ());
	}
	bool isRunning = false;
	IEnumerator Waiting()
	{
		isRunning = true;
		yield return new WaitForSeconds(1 / delay);
		isRunning = false;
	}
	IEnumerator Casting_Pray()
	{
		float time=0;
		float CurrentHp;
		CurrentHp=PlayerManager.instance.Local.CurrentHp;
		if(!transform.parent.GetComponent<DianaControl>().pray.GetComponent<Diana_Skill4_Pray>().praying)
			while (time < 0.5f&&CurrentHp==PlayerManager.instance.Local.CurrentHp) {
				time += Time.deltaTime;
				//기도 모션
				yield return null;
			}	
		Diana_Bullet_HolyLand holyland;
		holyland = PhotonNetwork.Instantiate ("Diana_HolyLand",
			PlayerManager.instance.Opponent.transform.position,Quaternion.identity,0).GetComponent<Diana_Bullet_HolyLand> ();
		holyland.Init_Diana_HolyLand (PlayerManager.instance.myPnum,view.viewID);
	}
}
