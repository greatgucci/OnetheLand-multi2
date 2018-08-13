using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill1 : Skills {

    public override void Excute()
    {

        if (isRunning)
        {
            return;
        }

        StartCoroutine(Shoot_IrisSkill1());

        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkill1()
    {
        Iris_Bullet1 bul = PhotonNetwork.Instantiate

            ("Iris_Bullet1", transform.position, Quaternion.identity, 0).
            GetComponent<Iris_Bullet1>();
        bul.Init_Iris_Bullet1(PlayerManager.instance.myPnum);

        yield return new WaitForSeconds(0.2f);

        bul = PhotonNetwork.Instantiate

            ("Iris_Bullet1", transform.position, Quaternion.identity, 0).
            GetComponent<Iris_Bullet1>();
        bul.Init_Iris_Bullet1(PlayerManager.instance.myPnum);

        yield return new WaitForSeconds(0.2f);

        bul = PhotonNetwork.Instantiate

            ("Iris_Bullet1", transform.position, Quaternion.identity, 0).
            GetComponent<Iris_Bullet1>();
        bul.Init_Iris_Bullet1(PlayerManager.instance.myPnum);
    }
}
