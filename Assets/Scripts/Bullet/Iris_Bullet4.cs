using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet4 : Bullet {

    float rotatingAngle;

    protected override void Move(int _shooterNum)
    {
        StartCoroutine(DestroyIrisSkill4());

        DVector = commuObject.transform.position - transform.position;
        DVector.Normalize();

        rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
        transform.Rotate(Vector3.forward, rotatingAngle);
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (isTirggerTime == true)
        {
            if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
            {
                return;
            }

            if (collision.tag == "Player" + oNum)
            {
                PlayerManager.instance.Local.CurrentHp -= damage;
            }
            if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
            {
                PlayerManager.instance.Local.CurrentSkillGage += 1f;
            }
        }
    }

    IEnumerator DestroyIrisSkill4()
    {
        yield return new WaitForSeconds(0.2f);

        DestroyToServer();
    }
}
