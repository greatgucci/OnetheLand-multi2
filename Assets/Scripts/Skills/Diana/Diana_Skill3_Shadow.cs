using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill3_Shadow : Skills{

	public override void Excute ()
    {
        Diana_Skill3_Impact impact;
		PhotonView view;
		view = GetComponent<PhotonView> ();
        AudioController.instance.PlayEffectSound(Character.DIANA, 4);
        impact = PhotonNetwork.Instantiate ("Diana_shadow",transform.position,Quaternion.identity,0).GetComponent<Diana_Skill3_Impact>();
		impact.shadow (GameManager.instance.myPnum, view.viewID);
	}

}