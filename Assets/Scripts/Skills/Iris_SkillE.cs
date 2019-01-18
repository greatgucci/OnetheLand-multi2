using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_SkillE : Skills {

    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }

        Iris_BulletE iris_BulletE;
        iris_BulletE = PhotonNetwork.Instantiate("Iris_BulletE", transform.position, Quaternion.identity, 0).GetComponent<Iris_BulletE>();
        iris_BulletE.Init_Iris_BulletE(GameManager.instance.myPnum, GameManager.instance.Local.aimVector);

        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }
}
