using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill4Circle : Bullet_Plural {

    float rotatingAngle = 1.57f;
    Vector3 dVector_Temp;
    GameObject warningSquare;

    protected override void Move(int _shooterNum)
    {
        speed = 5f;

        /*
        transform.parent = commuObject.transform;

        rotatingAngle += ((Mathf.PI * 0.4f) * (bulNum - 1));

        dVector_Temp.x = Mathf.Cos(rotatingAngle);
        dVector_Temp.y = Mathf.Sin(rotatingAngle);

        dVector_Temp.Normalize();
        DVector = dVector_Temp;

        StartCoroutine(MoveIrisSkill4Circle());

        rgbd.velocity = DVector * speed;
        */

        transform.parent = commuObject.transform;
        rotatingAngle += ((Mathf.PI * 0.4f) * (bulNum - 1));
        transform.Rotate(0, 0, Mathf.Rad2Deg * rotatingAngle);
        StartCoroutine(MoveIrisSkill4Circle());
    }

    IEnumerator MoveIrisSkill4Circle()
    {
        float angularSpeed = 20f;
        float radius = 0f;

        float timer = 0.4f;
        float timer_Temp = 0f;

        while(true)
        {
            if (timer_Temp >= timer)
            {
                break;
            }

            radius += speed * Time.deltaTime;
            transform.Rotate(0, 0, angularSpeed * Mathf.Rad2Deg * Time.deltaTime);
            transform.Translate(speed * Time.deltaTime, radius * Mathf.Tan(angularSpeed * Time.deltaTime), 0);

            timer_Temp += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(WarningAttack_IrisSkill4());
    }

    IEnumerator WarningAttack_IrisSkill4()
    {
        Bullet bul;

        if (PlayerManager.instance.Local.playerNum == oNum)//피격자 입장에서 판정
        {
            warningSquare = FavoriteFunction.WarningSquare(transform.position, 1f, 1f);
            warningSquare.transform.localScale = new Vector3(30f, 0.25f, 1f);

            DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            warningSquare.transform.Rotate(Vector3.forward, rotatingAngle);
        }

        bul = PhotonNetwork.Instantiate("TargetStatic", PlayerManager.instance.GetPlayerByNum(oNum).transform.position, Quaternion.identity, 0).GetComponent<Bullet>();
        bul.Init(shooterNum);

        PhotonView view = bul.gameObject.GetComponent<PhotonView>();

        yield return new WaitForSeconds(0.5f + (bulNum * 0.1f));

        if (PlayerManager.instance.Local.playerNum == oNum)//피격자 입장에서 판정
            Destroy(warningSquare);

        if (PlayerManager.instance.GetPlayerByNum(shooterNum) == PlayerManager.instance.Local)
        {
            bul = PhotonNetwork.Instantiate
            ("Iris_Skill4Line", transform.position, Quaternion.identity, 0).GetComponent<Bullet>();
            bul.Init(shooterNum, view.viewID);

            DestroyToServer();
        }
    }
}
