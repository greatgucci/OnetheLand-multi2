using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill2_HolyLand : Skills {

	PhotonView view;
	public override void Excute ()
	{
		view = GetComponent<PhotonView> ();
		StartCoroutine (Casting_Pray ());
	}
	IEnumerator Casting_Pray()
	{
		float time=0;
        bool ing = true;
        Vector3 startposition= transform.position;
		if(!transform.parent.GetComponent<DianaControl>().pray.GetComponent<Diana_Skill4_Pray>().praying)
        {
            while (time < 0.2f)
            {
                if ((startposition - transform.position).magnitude > 0f)
                {
                    ing = false;
                    break;
                }
                time += Time.deltaTime;
                //기도 모션
                yield return null;
			}
        }
        if (ing)
        {

            AudioController.instance.PlayEffectSound(Character.DIANA, 3);
            Diana_Bullet_HolyLand holyland;
            holyland = PhotonNetwork.Instantiate("Diana_HolyLand",
                PlayerManager.instance.Opponent.transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet_HolyLand>();
            holyland.Init_Diana_HolyLand(PlayerManager.instance.myPnum, view.viewID);
        }
	}
}
