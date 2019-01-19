using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_SkillQ : Skills {

    GameObject iris_BulletQTrigger;

    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }

        iris_BulletQTrigger = Instantiate(Resources.Load("Iris_BulletQTrigger") as GameObject, new Vector2(-9f, 0f), Quaternion.identity);
        iris_BulletQTrigger.GetComponent<Iris_BulletQTrigger>().IrisQMove();
        AudioController.instance.PlayEffectSound(Character.IRIS, 8);
        StartCoroutine(UltDirectionCoroutine());

        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator UltDirectionCoroutine()
    {
        float timer = 0f;
        SpriteRenderer mapColor = GameObject.Find("Map1BackGround").GetComponent<SpriteRenderer>(); //이거 다른 맵에서도 적용 가능하게 바꿔야함

        mapColor.color = new Color(0.6f, 0.6f, 0.8f, 1f);

        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if (timer >= 0.5f)
            {
                break;
            }

            mapColor.color = new Color(0.6f + (timer * 0.8f), 0.6f + (timer * 0.8f), 0.8f + (timer * 0.4f), 1f);

            timer += Time.deltaTime;
            yield return null;
        }
    }
}
