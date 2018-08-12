using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet3 : Bullet {

    protected override void Move(int _shooterNum)
    {
        StartCoroutine(MoveIrisSkillLine());
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
        {
            return;
        }

        if (collision.tag == "Player" + oNum)
        {
            PlayerManager.instance.Local.CurrentHp -= damage;
        }
    }

    IEnumerator MoveIrisSkillLine()
    {
        float rotatingAngle;
        float rotatingAngle_Temp = 0f;
        float timer = 0f;

        while (true)
        {

            if (timer > 1.5f)
            {
                DestroyToServer();
                break;
            }

            DVector = commuObject.transform.position - transform.position;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            transform.Rotate(Vector3.forward, rotatingAngle - rotatingAngle_Temp);
            rotatingAngle_Temp = rotatingAngle;

            timer += Time.deltaTime;
            yield return null;
        }
    }

}
