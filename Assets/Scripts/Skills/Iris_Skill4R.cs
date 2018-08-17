using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill4R : Skills{

    Vector3 createPosition;

    public override void Excute()
    {
        if (isRunning)
        {
            return;
        }

        Iris_Bullet4R iris_Bullet4R;

        for (int i = 0; i < 4; i++)
        {

            createPosition = FavoriteFunction.PIVectorCal(i <= 1 ? -50f + (25f * i) : -50f + (25f * (i + 1)));
            if (i == 0 || i == 3)
            {
                createPosition *= 1.5f;
            }
            else if (i == 1 || i == 2)
            {
                createPosition *= 0.75f;
            }
            createPosition += transform.position;

            iris_Bullet4R = PhotonNetwork.Instantiate("Iris_Bullet4R", createPosition, Quaternion.identity, 0)
                .GetComponent<Iris_Bullet4R>();
            iris_Bullet4R.Init_Iris_Bullet4R(PlayerManager.instance.myPnum, i);
        }

        StartCoroutine(Waiting());
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        PlayerManager.instance.Local.SetCooltime(0, 1.2f);
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }
}
