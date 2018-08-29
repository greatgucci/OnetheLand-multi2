using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill2_HolyLand : Skills {
    
    Diana_Bullet_HolyLand holyland;
    Vector3 position ;
    public override void Excute ()
	{
        position = PlayerManager.instance.Local.aimPosition;
        AudioController.instance.PlayEffectSound(Character.DIANA, 3);
        holyland = PhotonNetwork.Instantiate("Diana_HolyLand", position, Quaternion.identity, 0).GetComponent<Diana_Bullet_HolyLand>();
        holyland.Init_Diana_HolyLand(PlayerManager.instance.myPnum);
    }
}
