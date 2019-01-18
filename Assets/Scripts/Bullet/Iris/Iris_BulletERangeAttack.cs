using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_BulletERangeAttack : Bullet {

    public void Init_Iris_BulletERangeAttack(int _shooterNum)
    {
        photonView.RPC("Init_Iris_BulletERangeAttack_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_Iris_BulletERangeAttack_RPC(int _shooterNum)
    {
        Invoke("DestroyToServer", 5f);
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
        damage = 100;

        transform.localScale *= 3;
        StartCoroutine(DestroyBomb());
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
            //데미지 공식 - 레이저의 경우(디스트로이가 안 되는 경우) ( 20 * 초 * 데미지 )
            {
                GameManager.instance.Local.CurrentDamage -= (short)damage;
                GameManager.instance.GetPlayerByNum(oNum).GetStun();
                DestroyToServer();
            }
            if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
            {
                GameManager.instance.Local.CurrentSkillGage += (short)1f;
            }
        }
    }

    IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(0.25f);

        DestroyToServer();
    }
}
