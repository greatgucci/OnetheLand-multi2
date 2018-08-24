﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill3_Shadow : Skills{

	public override void Excute ()
	{
		Diana_Skill3_Impact impact;
		PhotonView view;
		view = GetComponent<PhotonView> ();
		impact = PhotonNetwork.Instantiate ("Diana_shadow",transform.position,Quaternion.identity,0).GetComponent<Diana_Skill3_Impact>();
		impact.shadow (PlayerManager.instance.myPnum, view.viewID);
	}

}