using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill3R : Skills {

    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }

        StartCoroutine(Shoot_IrisSkill3R());

        StartCoroutine(Waiting());

    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkill3R()
    {
        Iris_Skill3Targeting iris_Skill3Targeting;
        Iris_Skill3RCircle iris_Skill3RCircle;

        Vector3 tempPosition;
        tempPosition = transform.position;
        Vector3 oPosition;

        oPosition = GameManager.instance.Opponent.transform.position;

        iris_Skill3Targeting = PhotonNetwork.Instantiate("TargetMoving", oPosition, Quaternion.identity, 0)
        .GetComponent<Iris_Skill3Targeting>();
        iris_Skill3Targeting.Init_Iris_Skill3Targeting(GameManager.instance.myPnum);

        PhotonView view;
        view = iris_Skill3Targeting.GetComponent<PhotonView>();

        iris_Skill3RCircle = PhotonNetwork.Instantiate("Iris_Skill3RCircle", transform.position, Quaternion.identity, 0).GetComponent<Iris_Skill3RCircle>();
        iris_Skill3RCircle.Init_Iris_Skill3RCircle(GameManager.instance.myPnum, view.viewID, 0, GameManager.instance.Local.aimVector);
        iris_Skill3RCircle = PhotonNetwork.Instantiate("Iris_Skill3RCircle", transform.position, Quaternion.identity, 0).GetComponent<Iris_Skill3RCircle>();
        iris_Skill3RCircle.Init_Iris_Skill3RCircle(GameManager.instance.myPnum, view.viewID, 1, GameManager.instance.Local.aimVector);
        yield return null;
    }
}
