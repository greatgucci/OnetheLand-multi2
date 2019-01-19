using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill2R : Skills {

    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }

        StartCoroutine(Shoot_IrisSkill2R());

        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkill2R()
    {
        Iris_Skill2RCircle iris_Skill2RCircle;

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < 5; i++)
        {
            iris_Skill2RCircle = PhotonNetwork.Instantiate("Iris_Skill2RCircle", CalcCreatePosi(i), Quaternion.identity, 0).GetComponent<Iris_Skill2RCircle>();
            iris_Skill2RCircle.Init_Iris_Skill2RCircle(GameManager.instance.myPnum, i);

            yield return new WaitForSeconds(0.2f);
        }
    }

    Vector3 CalcCreatePosi(int num)
    {
        float rotatingAngle;
        Vector3 position;

        rotatingAngle = 2 * Mathf.PI / 4f;
        rotatingAngle += num * 2 * Mathf.PI / 5f;

        position.x = Mathf.Cos(rotatingAngle);
        position.y = Mathf.Sin(rotatingAngle);
        position.z = 0f;

        position.Normalize();

        position += transform.position;

        return position;
    }
}
