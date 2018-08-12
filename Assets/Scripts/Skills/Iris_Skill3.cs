using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill3 : Skills {

    GameObject irisSkill3Circle;
    GameObject irisSkill3Target;

    Vector3 oPostion;

    public override void Excute()
    {

        if (isRunning)
        {
            return;
        }

        StartCoroutine(IrisSkill3());

        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator IrisSkill3()
    {
        Vector3 tempPosition;
        tempPosition = transform.position;
        oPostion = PlayerManager.instance.Opponent.transform.position;

        irisSkill3Target = PhotonNetwork.Instantiate("TargetMoving", oPostion, Quaternion.identity, 0);
        Bullet bul = irisSkill3Target.GetComponent<Bullet>();
        bul.Init(PlayerManager.instance.myPnum);

        irisSkill3Circle = PhotonNetwork.Instantiate("Iris_Skill3Circle", transform.position, Quaternion.identity, 0);
        bul = irisSkill3Circle.GetComponent<Bullet>();

        PhotonView view;
        view = irisSkill3Target.GetComponent<PhotonView>();

        bul.Init(PlayerManager.instance.myPnum, view.viewID);

        yield return new WaitForSeconds(0.3f);

        bul = PhotonNetwork.Instantiate("Iris_Skill3Line", tempPosition, Quaternion.identity, 0).GetComponent<Bullet>();
        bul.Init(PlayerManager.instance.myPnum, view.viewID);
    }
}
