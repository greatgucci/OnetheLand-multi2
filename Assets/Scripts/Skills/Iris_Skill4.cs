using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill4 : Skills {

    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }

        StartCoroutine(Shoot_IrisSkill4());

        StartCoroutine(Waiting());

    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkill4()
    {
        Iris_Skill4Rotation bul = PhotonNetwork.Instantiate
            ("Iris_Skill4Rotation", transform.position, Quaternion.identity, 0).GetComponent<Iris_Skill4Rotation>();
        bul.Init(PlayerManager.instance.myPnum);

        PhotonView view;
        view = bul.gameObject.GetComponent<PhotonView>();

        Iris_Skill4Circle irisSkill4Circle;

        irisSkill4Circle = PhotonNetwork.Instantiate
            ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Iris_Skill4Circle>();
        irisSkill4Circle.Init(PlayerManager.instance.myPnum, 1, view.viewID);

        irisSkill4Circle = PhotonNetwork.Instantiate
                    ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Iris_Skill4Circle>();
        irisSkill4Circle.Init(PlayerManager.instance.myPnum, 2, view.viewID);

        irisSkill4Circle = PhotonNetwork.Instantiate
                    ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Iris_Skill4Circle>();
        irisSkill4Circle.Init(PlayerManager.instance.myPnum, 3, view.viewID);

        irisSkill4Circle = PhotonNetwork.Instantiate
                    ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Iris_Skill4Circle>();
        irisSkill4Circle.Init(PlayerManager.instance.myPnum, 4, view.viewID);

        irisSkill4Circle = PhotonNetwork.Instantiate
                    ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Iris_Skill4Circle>();
        irisSkill4Circle.Init(PlayerManager.instance.myPnum, 5, view.viewID);


        yield return new WaitForSeconds(0.2f);
    }
}
