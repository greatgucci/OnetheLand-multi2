using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iris_Skill3Circle : Bullet {

    GameObject warningSquare;
    GameObject irisSkill3Animation;

    protected override void Move(int _shooterNum)
    {
        irisSkill3Animation = Resources.Load("IrisSkill3Animation") as GameObject;

        if (PlayerManager.instance.Local.playerNum == oNum)//피격자 입장에서 판정
        {
            warningSquare = FavoriteFunction.WarningSquare(transform.position, 1f, 10f);
            warningSquare.transform.localScale = new Vector3(30f, 0.75f, 1f);
            StartCoroutine(MoveCircle());
        }

        StartCoroutine(DestroyCircle());
    }

    protected override void OnTriggerStay2D(Collider2D collision)
    {

    }

    IEnumerator MoveCircle()
    {
        float rotatingAngle;
        float rotatingAngle_Temp = 0f;

        float timer = 0f;

        while(true)
        {
            if(warningSquare == null)
            {
                break;
            }

            if (timer > 0.5f)
            {
                Destroy(warningSquare);
                
                break;
            }

            DVector = commuObject.transform.position - transform.position;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            warningSquare.transform.Rotate(Vector3.forward, rotatingAngle - rotatingAngle_Temp);
            rotatingAngle_Temp = rotatingAngle;

            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DestroyCircle()
    {
        GameObject irisSkill3Animation_Temp;
        irisSkill3Animation_Temp = Instantiate(irisSkill3Animation, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(2.0f);

        Destroy(irisSkill3Animation_Temp);
        DestroyToServer();
    }
}
