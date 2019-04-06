using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Diana_Skill_1 : Skills
{
    DianaControl dCon;

    private void Start()
    {
        dCon = GameManager.instance.Local.transform.GetComponentInParent<DianaControl>();
        StartCoroutine(Shoot_Re_Diana_Skill1());

    }

    public override void Excute()
    {
        if (isRunning)
            return;

        dCon.attack_On = dCon.attack_On == true ? false : true;
        StartCoroutine(Waiting());

    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_Re_Diana_Skill1()
    {
        while (true)
        {
            if (dCon.attack_On)
            {
                Re_Diana_Bullet_1 bul = new Re_Diana_Bullet_1();

                yield return new WaitForSeconds(0.1f);

                bul = PhotonNetwork.Instantiate
                        ("Re_Diana_Bullet_1", transform.position, Quaternion.identity, 0).
                        GetComponent<Re_Diana_Bullet_1>();
                bul.Init_Diana_Bullet_1(GameManager.instance.myPnum);
            }

            switch(dCon.attack_Level)
            {
                case 0:
                    yield return new WaitForSeconds(1.5f);
                    break;
                case 1:
                    yield return new WaitForSeconds(0.75f);
                    break;
                case 2:
                    yield return new WaitForSeconds(0.5f);
                    break;
                default:
                    yield return new WaitForSeconds(1.5f);
                    break;
            }
        }
    }
}
