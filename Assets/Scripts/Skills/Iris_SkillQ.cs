using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_SkillQ : Skills {

    GameObject iris_BulletQTrigger;

    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }

        iris_BulletQTrigger = Instantiate(Resources.Load("Iris_BulletQTrigger") as GameObject, new Vector2(-9f, 0f), Quaternion.identity);
        iris_BulletQTrigger.GetComponent<Iris_BulletQTrigger>().IrisQMove();

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
