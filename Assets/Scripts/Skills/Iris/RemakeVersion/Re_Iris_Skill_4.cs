using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Skill_4 : Skills
{
    public override void Excute()
    {
        if (isRunning)
            return;

        StartCoroutine(Shoot_IrisSkill_4());
        StartCoroutine(Waiting());

        Debug.Log(AudioController.instance);
        AudioController.instance.PlayEffectSound(Character.IRIS, 0);
    }

    bool isRunning = false;
    IEnumerator Waiting()
    {
        isRunning = true;
        yield return new WaitForSeconds(1 / delay);
        isRunning = false;
    }

    IEnumerator Shoot_IrisSkill_4()
    {
        Re_Iris_Bullet_4 bul = new Re_Iris_Bullet_4();

        yield return new WaitForSeconds(0.1f);

        Vector3 tempPositon = transform.position + new Vector3(GameManager.instance.Local.playerNum == 1 ? 7f : -7f, 0f, 0f);
        GameObject target = PhotonNetwork.Instantiate("TargetStatic", tempPositon, Quaternion.identity, 0);

        PhotonView view;
        view = target.GetComponent<PhotonView>();

        float raiserSize = 3f;
        GameObject warningSquare = FavoriteFunction.WarningSquare(tempPositon + new Vector3(-raiserSize * 0.5f, 0f, 0f), 1f, 3f);
        warningSquare.transform.localScale = new Vector3(raiserSize, 30f, 1f);

        yield return new WaitForSeconds(0.2f);

        Destroy(warningSquare);

        bul = PhotonNetwork.Instantiate
    ("Re_Iris_Bullet_4", tempPositon + new Vector3(0f, 10f, 0f), Quaternion.identity, 0).
    GetComponent<Re_Iris_Bullet_4>();
        bul.Init_Iris_Bullet_4(GameManager.instance.myPnum, view.viewID, raiserSize);
    }
}
