using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Skill_2 : Skills
{
    public override void Excute()
    {
        if (isRunning)
            return;

        StartCoroutine(Shoot_IrisSkill_2());
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

    IEnumerator Shoot_IrisSkill_2()
    {
        Re_Iris_Bullet_2[] bul = new Re_Iris_Bullet_2[3];

        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < 3; i++)
        {
            bul[i] = PhotonNetwork.Instantiate
                ("Re_Iris_Bullet_2", transform.position, Quaternion.identity, 0).
                GetComponent<Re_Iris_Bullet_2>();
            bul[i].Init_Iris_Bullet_2(GameManager.instance.myPnum, i);
        }
    }
}
