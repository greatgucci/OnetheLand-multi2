using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet1Bomb : Bullet {
    protected override void Move(int _shooterNum)
    {
        transform.localScale *= 2;
        StartCoroutine(DestroyBomb());
    }

    IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(0.2f);

        DestroyToServer();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
        {
            return;
        }

        if (collision.tag == "Player" + oNum)
        {
            PlayerManager.instance.Local.CurrentHp -= damage;
            DestroyToServer();
        }
    }
}
