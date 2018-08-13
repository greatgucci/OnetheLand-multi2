using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill4Circle : Bullet_Plural {

    float rotatingAngle = 1.57f;
    Vector3 dVector_Temp;
    GameObject warningSquare;
    GameObject commuObject;

    public void Init_Iris_Skill4Circle(int _shooterNum, int num, int communicatingObject)
    {
        photonView.RPC("Init_Iris_Skill4Circle_RPC", PhotonTargets.All, _shooterNum, num, communicatingObject);
    }

    [PunRPC]
    protected void Init_Iris_Skill4Circle_RPC(int _shooterNum, int num, int communicatingObject)
    {
        commuObject = PhotonView.Find(communicatingObject).gameObject;
        bulNum = num;
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
        TargetStatic targetStatic;
        Iris_Bullet4 irisBullet4;

        if (PlayerManager.instance.Local.playerNum == oNum)//피격자 입장에서 판정
        {
            warningSquare = FavoriteFunction.WarningSquare(transform.position, 1f, 1f);
            warningSquare.transform.localScale = new Vector3(30f, 0.25f, 1f);

            DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            warningSquare.transform.Rotate(Vector3.forward, rotatingAngle);
        }

        targetStatic = PhotonNetwork.Instantiate("TargetStatic", PlayerManager.instance.GetPlayerByNum(oNum).transform.position, Quaternion.identity, 0).GetComponent<TargetStatic>();
        targetStatic.Init(shooterNum);

        PhotonView view = targetStatic.gameObject.GetComponent<PhotonView>();

        yield return new WaitForSeconds(0.5f + (bulNum * 0.1f));

        if (PlayerManager.instance.Local.playerNum == oNum)//피격자 입장에서 판정
            Destroy(warningSquare);

        if (PlayerManager.instance.GetPlayerByNum(shooterNum) == PlayerManager.instance.Local)
        {
            irisBullet4 = PhotonNetwork.Instantiate
            ("Iris_Skill4Line", transform.position, Quaternion.identity, 0).GetComponent<Iris_Bullet4>();
            irisBullet4.Init(shooterNum, view.viewID);

            DestroyToServer();
        }
    }
}
