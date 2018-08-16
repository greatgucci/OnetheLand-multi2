using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill3RCircle : Bullet {

    GameObject warningSquare;
    GameObject irisSkill3Animation;
    GameObject commuObject;
    int tempViewID;
    int bulNum;

    public void Init_Iris_Skill3RCircle(int _shooterNum, int communicatingObject, int num)
    {
        photonView.RPC("Init_Iris_Iris_Skill3RCircle", PhotonTargets.All, _shooterNum, communicatingObject, num);
    }

    [PunRPC]
    protected void Init_Iris_Iris_Skill3RCircle(int _shooterNum, int communicatingObject, int num)
    {
        bulNum = num;
        tempViewID = communicatingObject;
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
        irisSkill3Animation = Resources.Load("IrisSkill3Animation") as GameObject;

        if (PlayerManager.instance.Local.playerNum == oNum)//피격자 입장에서 판정
        {
            StartCoroutine(MoveWarning());
        }

        StartCoroutine(MoveCircle());
        StartCoroutine(DestroyCircle());
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {

    }

    IEnumerator MoveWarning()
    {
        float rotatingAngle;
        float rotatingAngle_Temp = 0f;

        float timer = 0f;

        yield return new WaitForSeconds(0.2f);

        warningSquare = FavoriteFunction.WarningSquare(transform.position, 1f, 10f);
        warningSquare.transform.localScale = new Vector3(30f, 0.75f, 1f);

        while (true)
        {
            if (warningSquare == null)
            {
                break;
            }

            if (timer > 0.3f)
            {
                Destroy(warningSquare);

                break;
            }

            DVector = commuObject.transform.position - transform.position;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            warningSquare.transform.Rotate(Vector3.forward, rotatingAngle - rotatingAngle_Temp);
            rotatingAngle_Temp = rotatingAngle;

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator MoveCircle()
    {
        float rotatingAngle = 0f;

        if (bulNum == 0)
        {
            rotatingAngle = 80f;
        }
        else if (bulNum == 1)
        {
            rotatingAngle = -80f;
        }

        DVector = FavoriteFunction.PIVectorCal(rotatingAngle);
        rgbd.velocity = DVector * speed;

        yield return new WaitForSeconds(0.2f);

        rgbd.velocity = DVector * 0f;

        yield return new WaitForSeconds(0.3f);

        Iris_Bullet3R bul;

        bul = PhotonNetwork.Instantiate("Iris_Skill3RLine", transform.position, Quaternion.identity, 0).GetComponent<Iris_Bullet3R>();
        bul.Init_Iris_Bullet3R(PlayerManager.instance.myPnum, tempViewID);
    }

    IEnumerator DestroyCircle()
    {

        GameObject irisSkill3Animation_Temp = transform.Find("IrisSkill3Animation").gameObject;

        yield return new WaitForSeconds(2.6f);

        DestroyToServer();
    }
}
