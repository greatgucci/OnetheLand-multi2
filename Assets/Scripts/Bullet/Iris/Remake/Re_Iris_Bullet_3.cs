using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Bullet_3 : Bullet
{
    bool alreadyAttack = false;
    GameObject commuObject;

    public void Init_Iris_Bullet_3(int _shooterNum, int commuID)
    {
        photonView.RPC("Init_Iris_Bullet_3_RPC", PhotonTargets.All, _shooterNum, commuID);
    }

    [PunRPC]
    protected void Init_Iris_Bullet_3_RPC(int _shooterNum, int commuID)
    {
        Invoke("DestroyToServer", 1f);
        commuObject = PhotonView.Find(commuID).gameObject;
        shooterNum = _shooterNum;
        if (shooterNum == 1)
        {
            oNum = 2;
        }
        else if (shooterNum == 2)
        {
            oNum = 1;
        }
        Move(shooterNum);
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (isTirggerTime == true && alreadyAttack == false)
        {
            if (GameManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
            {
                return;
            }

            if (collision.tag == "Player" + oNum)
            {
                alreadyAttack = true;

                GameManager.instance.Local.CurrentDamage += (short)damage;
                GameManager.instance.GetPlayerByNum(oNum).GetKnockBack(shooterNum == 1 ? 1 : -1, 0, knockback);

            }
            if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
            {
                GameManager.instance.Local.CurrentSkillGage += (short)1f;
            }
        }
    }

    protected override void Move(int _shooterNum)
    {
        damage = 30;
        knockback = 30;
        float rotatingAngle = 0f;
        /* 상대쪽으로 발사
        DVector = commuObject.transform.position - transform.position;
        DVector.Normalize();

        rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
        transform.Rotate(Vector3.forward, rotatingAngle);
        */
        if (_shooterNum == 1)
        {
            DVector = commuObject.transform.position - transform.position;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            transform.Rotate(Vector3.forward, rotatingAngle);
        }
        else
        {
            DVector = commuObject.transform.position - transform.position;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            transform.Rotate(Vector3.forward, rotatingAngle);
        }
    }

}
