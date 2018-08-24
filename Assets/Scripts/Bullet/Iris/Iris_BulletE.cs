using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_BulletE : Bullet {

    GameObject warningCircle;

    public void Init_Iris_BulletE(int _shooterNum, Vector3 aimDVector)
    {
        photonView.RPC("Init_Iris_BulletE_RPC", PhotonTargets.All, _shooterNum, aimDVector);
    }

    [PunRPC]
    protected void Init_Iris_BulletE_RPC(int _shooterNum, Vector3 aimDVector)
    {
        DVector = aimDVector;
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

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    protected override void Move(int _shooterNum)
    {
        if (oNum == PlayerManager.instance.myPnum)
        {
            warningCircle = FavoriteFunction.WarningCircle(transform.position, 1f, 1f);
            warningCircle.transform.parent = transform;
        }

        StartCoroutine(AccelBulletE());
    }

    IEnumerator AccelBulletE()
    {
        float timer = 0f;

        while(true)
        {
            if (timer >= 0.25f)
            {
                break;
            }

            rgbd.velocity = DVector * Mathf.Lerp(24f, 0f, timer * 4f);

            timer += Time.deltaTime;
            yield return null;
        }

        if (shooterNum == PlayerManager.instance.myPnum)
        {
            AudioController.instance.PlayEffectSound(Character.IRIS, 3);
            Iris_BulletERangeAttack iris_BulletERangeAttack
            = PhotonNetwork.Instantiate("Iris_BulletERangeAttack", transform.position, Quaternion.identity, 0)
            .GetComponent<Iris_BulletERangeAttack>();
            iris_BulletERangeAttack.Init_Iris_BulletERangeAttack(PlayerManager.instance.myPnum);
            DestroyToServer();
        }
    }
}
