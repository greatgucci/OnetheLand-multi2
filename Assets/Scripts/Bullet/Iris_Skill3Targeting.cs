using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill3Targeting : Bullet {

    protected override void Move(int _shooterNum)
    {
        speed = 1f;

        StartCoroutine(MoveIrisSKill3Targeting());
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {
        
    }

    IEnumerator MoveIrisSKill3Targeting()
    {
        while(true)
        {
            DVector = FavoriteFunction.VectorCalc(gameObject, oNum);

            rgbd.velocity = DVector * speed;
            yield return null;
        }
    }

}
