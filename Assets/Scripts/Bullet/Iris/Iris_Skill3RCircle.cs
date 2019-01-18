using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill3RCircle : Bullet {

    GameObject warningSquare;
    GameObject irisSkill3Animation;
    GameObject commuObject;
    GameObject iris_Skill3REffect1;
    GameObject iris_Skill3REffect2;
    int tempViewID;
    int bulNum;
    Vector3 tempAimVector;

    public void Init_Iris_Skill3RCircle(int _shooterNum, int communicatingObject, int num, Vector3 aimVector)
    {
        photonView.RPC("Init_Iris_Iris_Skill3RCircle", PhotonTargets.All, _shooterNum, communicatingObject, num, aimVector);
    }

    [PunRPC]
    protected void Init_Iris_Iris_Skill3RCircle(int _shooterNum, int communicatingObject, int num, Vector3 aimVector)
    {
        tempAimVector = aimVector;
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
        iris_Skill3REffect1 = transform.Find("CFX2_PowerAura Yellow").gameObject;
        iris_Skill3REffect2 = transform.Find("CFX2_ShinyItem").gameObject;
        irisSkill3Animation = Resources.Load("IrisSkill3Animation") as GameObject;

        iris_Skill3REffect2.SetActive(false);

        if (GameManager.instance.Local.playerNum == oNum)//피격자 입장에서 판정
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

        warningSquare = FavoriteFunction.WarningSquare(transform.position + new Vector3(0f, -0.3f, 0f), 1f, 0.5f);
        warningSquare.transform.localScale = new Vector3(30f, 0.5f, 1f);

        DVector = commuObject.transform.position - transform.position;
        DVector.Normalize();

        rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
        warningSquare.transform.Rotate(Vector3.forward, rotatingAngle - rotatingAngle_Temp);
        rotatingAngle_Temp = rotatingAngle;

        /*
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
        */
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

        DVector = FavoriteFunction.VectorRotationWithDegree(tempAimVector, rotatingAngle);
        
        rgbd.velocity = DVector * speed;

        yield return new WaitForSeconds(0.2f);

        rgbd.velocity = DVector * 0f;

        yield return new WaitForSeconds(0.3f);
        //iris_Skill3REffect1.SetActive(false);

        //iris_Skill3REffect2.SetActive(true);
        Iris_Bullet3R bul;

        bul = PhotonNetwork.Instantiate("Iris_Skill3RLine", transform.position + new Vector3(0f, -0.3f, 0f), Quaternion.identity, 0).GetComponent<Iris_Bullet3R>();
        bul.Init_Iris_Bullet3R(GameManager.instance.myPnum, tempViewID);
        yield return new WaitForSeconds(1.4f);

        //iris_Skill3REffect2.SetActive(false);

    }

    IEnumerator DestroyCircle()
    {
        GameObject irisSkill3Animation_Temp = transform.Find("IrisSkill3Animation").gameObject;

        yield return new WaitForSeconds(2.6f);

        DestroyToServer();
    }
}
