using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill4 : Skills
{
    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }
        
        StartCoroutine(Waiting());
        StartCoroutine(Pray_Create_Cross());
    }
    private IEnumerator Pray_Create_Cross()
    {
        //기도 드리는 모션
        yield return new WaitForSeconds(0.2f);
        Diana_Bullet4 diana_bullet4;
		diana_bullet4 = PhotonNetwork.Instantiate("Diana_Bullet4", new Vector3 (0f,0f,0f), Quaternion.identity, 0).GetComponent<Diana_Bullet4>();
        diana_bullet4.Init_Diana_Bullet4(PlayerManager.instance.myPnum);
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }
}