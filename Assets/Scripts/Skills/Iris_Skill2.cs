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
        Bullet_Plural[] bul = new Bullet_Plural[5];

        for (int i = 0; i < 4; i++)
        {
            bul[i] = PhotonNetwork.Instantiate

                ("Iris_Skill2", transform.position, Quaternion.identity, 0).
                GetComponent<Bullet_Plural>();
            bul[i].Init(PlayerManager.instance.myPnum, i);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 4; i++)
        {
            bul[i] = PhotonNetwork.Instantiate

                ("Iris_Skill2", transform.position, Quaternion.identity, 0).
                GetComponent<Bullet_Plural>();
            bul[i].Init(PlayerManager.instance.myPnum, i+1);
        }

    }
}
