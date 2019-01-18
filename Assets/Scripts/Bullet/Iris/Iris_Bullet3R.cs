using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet3R : Bullet {

    GameObject commuObject;

    public void Init_Iris_Bullet3R(int _shooterNum, int communicatingObject)
    {
        AudioController.instance.PlayEffectSound(Character.IRIS, 6);
        photonView.RPC("Init_Iris_Bullet3R_RPC", PhotonTargets.All, _shooterNum, communicatingObject);
    }

    [PunRPC]
    protected void Init_Iris_Bullet3R_RPC(int _shooterNum, int communicatingObject)
    {
        commuObject = PhotonView.Find(communicatingObject).gameObject;
        Invoke("DestroyToServer", 10f);
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

    protected override void Move(int _shooterNum)
    {
        damage = 10;
        StartCoroutine(MoveIrisSkillLine());
    }
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (isTirggerTime == true)
        {
            if (GameManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
            {
                return;
            }

            if (collision.tag == "Player" + oNum)
            {
                GameManager.instance.Local.CurrentDamage -= (short)damage;

            }
            if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
            {
                GameManager.instance.Local.CurrentSkillGage += (short)1f;
            }
        }
    }

    IEnumerator MoveIrisSkillLine()
    {
        float rotatingAngle;
        float rotatingAngle_Temp = 0f;
        float timer = 0f;

        while (true)
        {

            if (timer > 1.2f)
            {
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

        transform.localScale = new Vector3(60f, 0.375f, 1f);

        yield return new WaitForSeconds(0.1f);

        transform.localScale = new Vector3(60f, 0.1f, 1f);

        yield return new WaitForSeconds(0.05f);

        DestroyToServer();
    }
}
