using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Skill_5 : Skills
{
    public override void Excute()
    {
        if (isRunning)
            return;

        StartCoroutine(Shoot_IrisSkill_5());
        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkill_5()
    {
        Re_Iris_Bullet_5_Circle bul = new Re_Iris_Bullet_5_Circle();

        yield return new WaitForSeconds(0.1f);

        Vector3 oPosition = GameManager.instance.Opponent.transform.position;
        GameObject target = PhotonNetwork.Instantiate("TargetMoving", oPosition, Quaternion.identity, 0);

        target.GetComponent<Iris_Skill3Targeting>().Init_Iris_Skill3Targeting(GameManager.instance.myPnum);

        PhotonView view;
        view = target.GetComponent<PhotonView>();

        bul = PhotonNetwork.Instantiate
        ("Re_Iris_Bullet_5_Circle", transform.position, Quaternion.identity, 0).
        GetComponent<Re_Iris_Bullet_5_Circle>();
        bul.Init_Iris_Bullet_5_Circle(GameManager.instance.myPnum, view.viewID);
    }
}
