using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Skill_3 : Skills
{
    public override void Excute()
    {
        if (isRunning)
            return;

        StartCoroutine(Shoot_IrisSkill_3());
        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkill_3()
    {
        Re_Iris_Bullet_3_Hallucination bul;

        yield return new WaitForSeconds(0.1f);

        Vector3 oPosition;

        oPosition = GameManager.instance.Opponent.transform.position;
        GameObject target = PhotonNetwork.Instantiate("TargetStatic", transform.position + 
            (Vector3)(GameManager.instance.myPnum == 1 ? Vector2.right : Vector2.left), Quaternion.identity, 0);

        PhotonView view;
        view = target.GetComponent<PhotonView>();

        bul = PhotonNetwork.Instantiate
        ("Re_Iris_Hallu", transform.position, Quaternion.identity, 0).
        GetComponent<Re_Iris_Bullet_3_Hallucination>();
        bul.Init_Iris_Bullet_3_Hallu(GameManager.instance.myPnum, view.viewID);
        StartCoroutine(GetInvisible_Iris());
    }

    IEnumerator GetInvisible_Iris()
    {
        GameManager.instance.Local.GetComponentInParent<PlayerControl>().GetInvisible();

        yield return new WaitForSeconds(4f);

        GameManager.instance.Local.GetComponentInParent<PlayerControl>().CancleInvisible();
    }

}
