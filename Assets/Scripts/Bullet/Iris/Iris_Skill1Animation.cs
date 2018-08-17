using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Iris_Skill1Animation : Bullet {

    Image irisBomb1;
    Image irisBomb2;
    Image irisBomb3;
    Image irisBomb4;

    public void Init_Iris_Skill1Animation(int _shooterNum)
    {
        photonView.RPC("Init_Iris_Skill1Animation_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_Iris_Skill1Animation_RPC(int _shooterNum)
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
        irisBomb1 = transform.Find("IrisBomb1Canvas").Find("IrisBomb1").GetComponent<Image>();
        irisBomb2 = transform.Find("IrisBomb1Canvas").Find("IrisBomb2").GetComponent<Image>();
        irisBomb3 = transform.Find("IrisBomb1Canvas").Find("IrisBomb3").GetComponent<Image>();
        irisBomb4 = transform.Find("IrisBomb1Canvas").Find("IrisBomb4").GetComponent<Image>();

        irisBomb1.fillAmount = 0f;
        irisBomb2.fillAmount = 0f;
        irisBomb3.fillAmount = 0f;
        irisBomb4.fillAmount = 0f;

        StartCoroutine(AnimationStart());
    }

    IEnumerator AnimationStart()
    {
        StartCoroutine(AnimationBomb1());

        yield return new WaitForSeconds(0.1f);

        irisBomb2.fillAmount = 1f;
        StartCoroutine(AnimationBomb2());

        irisBomb4.fillAmount = 1f;
        StartCoroutine(AnimationBomb4());

        yield return new WaitForSeconds(0.3f);

        irisBomb3.fillAmount = 1f;
        StartCoroutine(AnimationBomb3());

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(AnimationExit());

        yield return new WaitForSeconds(0.5f);

        DestroyToServer();
    }

    IEnumerator AnimationBomb1()
    {
        float timer = 0f;

        while(true)
        {
            if (timer >= 0.1f)
            {
                break;
            }

            irisBomb1.fillAmount = timer * 10f;

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AnimationBomb2()
    {
        float timer = 0f;

        while (true)
        {
            if (timer >= 0.1f)
            {
                break;
            }

            irisBomb2.color = new Color(1f, 1f, 1f, timer * 10f);

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AnimationBomb4()
    {
        float timer = 0f;

        while (true)
        {
            if (timer >= 0.4f)
            {
                break;
            }

            irisBomb4.color = new Color(1f, 1f, 1f, timer * 2.5f);

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AnimationBomb3()
    {
        float timer = 0f;

        while (true)
        {
            if (timer >= 0.1f)
            {
                break;
            }

            irisBomb3.color = new Color(1f, 1f, 1f, timer * 10f);

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator AnimationExit()
    {
        float timer = 0.5f;

        while (true)
        {
            if (timer <= 0f)
            {
                break;
            }
            irisBomb1.color = new Color(1f, 1f, 1f, timer * 2f);
            irisBomb2.color = new Color(1f, 1f, 1f, timer * 2f);
            irisBomb3.color = new Color(1f, 1f, 1f, timer * 2f);
            irisBomb4.color = new Color(1f, 1f, 1f, timer * 2f);

            timer -= Time.deltaTime;
            yield return null;
        }
    }
}
