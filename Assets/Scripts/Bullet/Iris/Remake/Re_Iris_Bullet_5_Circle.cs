using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Bullet_5_Circle : Bullet
{
    GameObject commuObject;

    public void Init_Iris_Bullet_5_Circle(int _shooterNum, int commuID)
    {
        photonView.RPC("Init_Iris_Bullet_5_Circle_RPC", PhotonTargets.All, _shooterNum, commuID);
    }

    [PunRPC]
    protected void Init_Iris_Bullet_5_Circle_RPC(int _shooterNum, int commuID)
    {
        Invoke("DestroyToServer", 4f);
        commuObject = PhotonView.Find(commuID).gameObject;
        shooterNum = _shooterNum;
        if (shooterNum == 1)
        {
            oNum = 2;
        }
        else if (shooterNum == 2)
        {
            oNum = 1;
        }
        Move(_shooterNum);
    }

    protected override void Move(int _shooterNum)
    {
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        float timer = 0f;
        float rotatingAngle;
        float tempAngle = 0f;
        
        while(true)
        {
            if (timer >= 1f)
            {
                break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        GameObject warningSquare = FavoriteFunction.WarningSquare(transform.position - new Vector3(0f, 0.3f, 0f), 1f, 3f);
        warningSquare.transform.localScale = new Vector3(60f, 0.5f, 1f);

        while (true)
        {
            if (timer >= 1.9f)
            {
                break;
            }

            DVector = commuObject.transform.position - warningSquare.transform.position;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            warningSquare.transform.Rotate(Vector3.forward, rotatingAngle - tempAngle);
            tempAngle = rotatingAngle;

            timer += Time.deltaTime;
            yield return null;
        }
        GameObject target = PhotonNetwork.Instantiate("TargetStatic", commuObject.transform.position, Quaternion.identity, 0);
        Destroy(commuObject);

        PhotonView view;
        view = target.GetComponent<PhotonView>();

        yield return new WaitForSeconds(0.1f);

        Destroy(warningSquare);

        if (photonView.isMine)
        {
            Re_Iris_Bullet_5 bul;
            bul = PhotonNetwork.Instantiate
            ("Re_Iris_Bullet_5", transform.position - new Vector3(0f, 0.3f, 0f), Quaternion.identity, 0).
            GetComponent<Re_Iris_Bullet_5>();

            bul.Init_Iris_Bullet_5(GameManager.instance.myPnum, view.viewID);
        }
    }
}
