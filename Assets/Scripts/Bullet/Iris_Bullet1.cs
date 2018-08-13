using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet1 : Bullet {

    Vector3 oPosition_Temp;
    GameObject warning_Temp;

    public void Init_Iris_Bullet1(int _shooterNum)
    {
        photonView.RPC("Init_Iris_Bullet1_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_Iris_Bullet1_RPC(int _shooterNum)
    {
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
        oPosition_Temp = PlayerManager.instance.GetPlayerByNum(oNum).transform.position;

        if (PlayerManager.instance.Local.playerNum == oNum)
        {
            warning_Temp = FavoriteFunction.WarningCircle(oPosition_Temp, 2f, 3f);
        }

        DVector = FavoriteFunction.VectorCalc(gameObject, oNum);
        FavoriteFunction.RotateBullet(gameObject);

        StartCoroutine(Accel_IrisBullet1());
    }

    IEnumerator Accel_IrisBullet1()
    {
        speed = 1.5f;

        while (true)
        {
            if (speed >= 8.5f)
                break;

            speed += 3 * Time.deltaTime;
            rgbd.velocity = DVector * speed;

            yield return null;
        }
    }

    protected override void OnTriggerStay2D(Collider2D c)
    {

        if (PlayerManager.instance.Local.playerNum != oNum)
        {
            return;
        }

        if ((c.transform.parent == warning_Temp.transform) && (c.name == "JudCircle"))
        {
            Iris_Bullet1Bomb bul = PhotonNetwork.Instantiate

            ("RangeAttackCircle", c.transform.position, Quaternion.identity, 0).
            GetComponent<Iris_Bullet1Bomb>();
            bul.Init_Iris_Bullet1Bomb(shooterNum);
            bul = PhotonNetwork.Instantiate

            ("IrisSkill1Animation", transform.position, Quaternion.identity, 0).GetComponent<Iris_Bullet1Bomb>();
            bul.Init_Iris_Bullet1Bomb(shooterNum);
            DestroyToServer();
        }
    }

    protected override void DestroyCallBack()
    {
        Destroy(warning_Temp);
    }
    

}
