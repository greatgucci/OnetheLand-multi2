using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Diana_Skill_3 : Skills
{
    DianaControl dCon;

    private void Start()
    {
        dCon = GameManager.instance.Local.transform.GetComponentInParent<DianaControl>();

    }

    public override void Excute()
    {
        if (isRunning)
            return;

        dCon.IncreaseAttackLevel();

        StartCoroutine(Waiting());

    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    
    
}
