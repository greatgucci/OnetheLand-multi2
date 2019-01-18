using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_SpecialAttack : Skills
{
	Vector3 dVector;
    Diana_SpecialBullet dan_at;
    public override void Excute()
    {
        if (isRunning)
            return;
		dVector = GameManager.instance.Local.aimVector.normalized;
        AudioController.instance.PlayEffectSound(Character.DIANA, 1);
        for (int type = 0; type < 5; type++)
        {
            dan_at = PhotonNetwork.Instantiate("Diana_SpecialBullet", transform.position, Quaternion.identity, 0).GetComponent<Diana_SpecialBullet>();
            dan_at.Init_Diana_SpecialBullet(GameManager.instance.myPnum, type - 2, dVector);
        }
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