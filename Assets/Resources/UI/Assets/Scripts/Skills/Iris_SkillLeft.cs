using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_SkillLeft : Skills {

    public override void Excute()
    {
        if (isRunning)
            return;

        StartCoroutine(Shoot_IrisSkillLeft());
        StartCoroutine(Waiting());

        Debug.Log(AudioController.instance);
        AudioController.instance.PlayEffectSound(Character.IRIS, 0);
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkillLeft()
    {
        Iris_BulletLeft[] bul = new Iris_BulletLeft[5];

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 5; i++)
        {
            bul[i] = PhotonNetwork.Instantiate
                ("Iris_BulletLeft", transform.position, Quaternion.identity, 0).
                GetComponent<Iris_BulletLeft>();
            bul[i].Init_Iris_BulletLeft(GameManager.instance.myPnum, i, GameManager.instance.Local.aimVector);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
