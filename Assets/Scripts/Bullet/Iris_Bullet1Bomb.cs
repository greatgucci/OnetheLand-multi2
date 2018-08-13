using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Bullet1Bomb : Bullet {
    protected override void Move(int _shooterNum)
    {
        damage = 50;

        transform.localScale *= 2;
        StartCoroutine(DestroyBomb());
    }

    IEnumerator DestroyBomb()
    {
        yield return new WaitForSeconds(0.2f);

        DestroyToServer();
    }

    
}
