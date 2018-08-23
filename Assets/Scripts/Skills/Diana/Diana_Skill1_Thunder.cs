﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill1_Thunder : Skills
{
	public override void Excute ()
	{
		if (isRunning)
			return;
		transform.parent.gameObject.GetComponent<DianaControl> ().skill1_playing = true;
		StartCoroutine (Directing ());
	}
	bool isRunning = false;
	IEnumerator Waiting()
	{
		isRunning = true;
		yield return new WaitForSeconds(1 / delay);
		isRunning = false;
	}

	IEnumerator Directing()
	{
		Vector3 dVector;
		float rotatingAngle;
		GameObject rangeObject = Instantiate (Resources.Load ("Diana_Skill1_Directing") as GameObject, PlayerManager.instance.Local.aimPosition,Quaternion.identity);
		while (true) {
			rangeObject.transform.position = PlayerManager.instance.Local.aimPosition;
			if (Input.GetKeyUp (KeyCode.Mouse0))
				break;
			yield return null;
		}
		yield return new WaitForSeconds(0.2f);
		while (true) {
			dVector = PlayerManager.instance.Local.aimPosition - rangeObject.transform.position;
			rotatingAngle = dVector.y > 0 ? Vector3.AngleBetween(dVector, Vector3.right) : -Vector3.AngleBetween(dVector, Vector3.right);
			rangeObject.transform.rotation = Quaternion.identity;
			rangeObject.transform.RotateAround(Vector3.forward, rotatingAngle);
			if (Input.GetKeyUp (KeyCode.Mouse0))
				break;
			yield return null;
		}
		//상대한테 범위 instantiate
		Diana_Bullet_Thunder_Create thunder_create;
		thunder_create = PhotonNetwork.Instantiate ("Diana_Thunder_Creater",rangeObject.transform.position,Quaternion.identity,0).GetComponent<Diana_Bullet_Thunder_Create>();
		thunder_create.Diana_Thunder_Create (PlayerManager.instance.myPnum,dVector);
		Destroy(rangeObject);
	}
}