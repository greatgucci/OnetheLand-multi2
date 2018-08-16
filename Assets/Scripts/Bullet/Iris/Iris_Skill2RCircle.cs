using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill2RCircle : Bullet
{
    int bulNum;
    GameObject warningSquare;


    public void Init_Iris_Skill2RCircle(int _shooterNum, int num)
    {
        photonView.RPC("Init_Iris_Skill2RCircle_RPC", PhotonTargets.All, _shooterNum, num);
    }

    [PunRPC]
    protected void Init_Iris_Skill2RCircle_RPC(int _shooterNum, int num)
    {
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
        StartCoroutine(WarningAttack_IrisSKill2R());
    }

    IEnumerator WarningAttack_IrisSKill2R()
    {
        float rotatingAngle;

        TargetStatic targetStatic;

        if (PlayerManager.instance.Local.playerNum == oNum)//피격자 입장에서 판정
        {
            warningSquare = FavoriteFunction.WarningSquare(transform.position, 1f, 1f);
            warningSquare.transform.localScale = new Vector3(30f, 0f, 1f);

            StartCoroutine(WarningSquareIncrease());

            DVector = PlayerManager.instance.GetPlayerByNum(shooterNum).aimVector;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            warningSquare.transform.Rotate(Vector3.forward, rotatingAngle);
        }

        targetStatic = PhotonNetwork.Instantiate("TargetStatic", PlayerManager.instance.GetPlayerByNum(shooterNum).aimPosition, Quaternion.identity, 0).GetComponent<TargetStatic>();
        targetStatic.Init_TargetStatic(shooterNum);

        PhotonView view = targetStatic.gameObject.GetComponent<PhotonView>();

        yield return new WaitForSeconds(0.2f);

        if (PlayerManager.instance.GetPlayerByNum(shooterNum) == PlayerManager.instance.Local)
        {
            Iris_Bullet2R irisBullet2R;

            irisBullet2R = PhotonNetwork.Instantiate
            ("Iris_Skill2RLine", transform.position, Quaternion.identity, 0).GetComponent<Iris_Bullet2R>();
            irisBullet2R.Init_Iris_Bullet2R(shooterNum, view.viewID);
        }
        yield return new WaitForSeconds(0.4f);
        DestroyToServer();

    }

    IEnumerator WarningSquareIncrease()
    {
        float timer = 0f;

        while (true)
        {
            if (timer >= 0.2f)
            {
                break;
            }

            warningSquare.transform.localScale += new Vector3(0f, timer * (0.25f / 0.2f), 0f);

            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(warningSquare);
    }
}
