using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Skill_1 : Skills {

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
        Re_Iris_Bullet_1[] bul = new Re_Iris_Bullet_1[3];

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 3; i++)
        {
            bul[i] = PhotonNetwork.Instantiate
                ("Re_Iris_Bullet_1", transform.position, Quaternion.identity, 0).
                GetComponent<Re_Iris_Bullet_1>();
            bul[i].Init_Iris_BulletLeft(GameManager.instance.myPnum, i);

            yield return new WaitForSeconds(0.1f);
        }
    }
}
