using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_SkillShift : Skills {

    GameObject iris_BulletShift;
    Vector3 teleportPosition;

    public override void Excute()
    {
        if (isRunning)
            return;

        iris_BulletShift = GameObject.Find("iris_BulletShift_" + PlayerManager.instance.myPnum);
        if (iris_BulletShift != null)
        {
            teleportPosition = iris_BulletShift.transform.position;
            iris_BulletShift.GetComponent<Iris_BulletShift>().Init_Iris_BulletShift(PlayerManager.instance.myPnum);

            Debug.Log(teleportPosition);
            iris_BulletShift = PhotonNetwork.Instantiate("Iris_BulletShift", transform.position, Quaternion.identity, 0);
            iris_BulletShift.name = "iris_BulletShift_" + PlayerManager.instance.myPnum;

            transform.parent.position = teleportPosition;
        }
        else
        {
            iris_BulletShift = PhotonNetwork.Instantiate("Iris_BulletShift", transform.position, Quaternion.identity, 0);
            iris_BulletShift.name = "iris_BulletShift_" + PlayerManager.instance.myPnum;
        }


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
