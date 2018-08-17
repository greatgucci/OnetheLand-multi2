using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill1_default : Skills
{
	int[] angle = {-5,0,5};
    public override void Excute()
    {
        if(isRunning)
        {
            return;
        }
		StartCoroutine (ShooterBullet ());
        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }
	IEnumerator ShooterBullet()
	{
		for (int i = 0; i < 6; i++) {
			Diana_Bullet1_default d_b_d = PhotonNetwork.Instantiate
				("Diana_Bullet1_default", transform.position, Quaternion.identity,0).
				GetComponent<Diana_Bullet1_default>();
			d_b_d.Init_Diana_Bullet1_default(PlayerManager.instance.myPnum, angle[i%3]);
			yield return new WaitForSeconds (0.2f);
		}
	}
}
