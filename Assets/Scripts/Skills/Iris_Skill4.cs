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
        Bullet bul = PhotonNetwork.Instantiate
            ("Iris_Skill4Rotation", transform.position, Quaternion.identity, 0).GetComponent<Bullet>();
        bul.Init(PlayerManager.instance.myPnum);

        PhotonView view;
        view = bul.gameObject.GetComponent<PhotonView>();

        Bullet_Plural bul_P;

        bul_P = PhotonNetwork.Instantiate
            ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Bullet_Plural>();
        bul_P.Init(PlayerManager.instance.myPnum, 1, view.viewID);

        bul_P = PhotonNetwork.Instantiate
                    ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Bullet_Plural>();
        bul_P.Init(PlayerManager.instance.myPnum, 2, view.viewID);

        bul_P = PhotonNetwork.Instantiate
                    ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Bullet_Plural>();
        bul_P.Init(PlayerManager.instance.myPnum, 3, view.viewID);

        bul_P = PhotonNetwork.Instantiate
                    ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Bullet_Plural>();
        bul_P.Init(PlayerManager.instance.myPnum, 4, view.viewID);

        bul_P = PhotonNetwork.Instantiate
                    ("Iris_Skill4Circle", transform.position, Quaternion.identity, 0).GetComponent<Bullet_Plural>();
        bul_P.Init(PlayerManager.instance.myPnum, 5, view.viewID);


        yield return new WaitForSeconds(0.2f);
    }
}
