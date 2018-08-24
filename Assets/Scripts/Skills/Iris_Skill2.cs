using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill2 : Skills {

    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }

        StartCoroutine(Shoot_IrisSkill2());

        StartCoroutine(Waiting());

        AudioController.instance.PlayEffectSound(Character.IRIS, 1);
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkill2()
    {
        Iris_Bullet2[] bul = new Iris_Bullet2[5];

        for (int i = 0; i < 4; i++)
        {
            bul[i] = PhotonNetwork.Instantiate

                ("Iris_Skill2", transform.position, Quaternion.identity, 0).
                GetComponent<Iris_Bullet2>();
            bul[i].Init_Iris_Bullet2(PlayerManager.instance.myPnum, i, PlayerManager.instance.Local.aimVector);
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 4; i++)
        {
            bul[i] = PhotonNetwork.Instantiate

                ("Iris_Skill2", transform.position, Quaternion.identity, 0).
                GetComponent<Iris_Bullet2>();
            bul[i].Init_Iris_Bullet2(PlayerManager.instance.myPnum, i+1, PlayerManager.instance.Local.aimVector);
        }

    }
}
