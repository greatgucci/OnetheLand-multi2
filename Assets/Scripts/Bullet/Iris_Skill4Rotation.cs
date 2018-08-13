using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill4Rotation : Bullet {

    Vector3 tempPosition;

    public void Init_Iris_Skill4Rotation(int _shooterNum)
    {
        photonView.RPC("Init_Iris_Skill4Rotation_RPC", PhotonTargets.All, _shooterNum);
    }

    [PunRPC]
    protected void Init_Iris_Skill4Rotation_RPC(int _shooterNum)
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
        transform.parent = PlayerManager.instance.GetPlayerByNum(_shooterNum).transform;

        StartCoroutine(IrisSkill4_Rotate());
    }


    IEnumerator IrisSkill4_Rotate()
    {
        float timer = 0.4f;
        float timer_Temp = 0f;

        while(true)
        {
            if (timer_Temp >= timer)
            {
                tempPosition = transform.position;
                StartCoroutine(FixThisPosition());
                break;
            }

            transform.localPosition = Vector3.zero;

            timer_Temp += Time.deltaTime;
            yield return null;
        }

        
    }

    IEnumerator FixThisPosition()
    {
        while (true)
        {
            transform.position = tempPosition;
            yield return null;
        }
    }
}
