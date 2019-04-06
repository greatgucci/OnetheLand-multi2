using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Re_Iris_Bullet_3_Hallucination : Bullet
{
    GameObject commuOb;
    public void Init_Iris_Bullet_3_Hallu(int _shooterNum, int commuID)
    {
        photonView.RPC("Init_Iris_Bullet_3_Hallu_RPC", PhotonTargets.All, _shooterNum, commuID);
    }

    [PunRPC]
    protected void Init_Iris_Bullet_3_Hallu_RPC(int _shooterNum, int commuID)
    {
        commuOb = PhotonView.Find(commuID).gameObject;
        Invoke("DestroyToServer", 4f);
        shooterNum = _shooterNum;
        if (shooterNum == 1)
        {
            oNum = 2;
            transform.Find("Renderer").GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (shooterNum == 2)
        {
            oNum = 1;
        }
        originalMoveVector = transform.position;
        Move(shooterNum);
    }

    protected override void Move(int _shooterNum)
    {

    }
    protected override void OnTriggerStay2D(Collider2D collision)
    {
        if (isTirggerTime == true)
        {
            if (GameManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
            {
                return;
            }

            if (collision.GetComponent<Bullet>() != null)
            //데미지 공식 - 레이저의 경우(디스트로이가 안 되는 경우) ( 20 * 초 * 데미지 )
            {
                if (collision.GetComponent<Bullet>().shooterNum != shooterNum)
                {
                    collision.GetComponent<Bullet>().DestroyToServer();
                    DestroyToServer();
                }
            }
        }
    }

    IEnumerator ShootLaser()
    {
        GameManager.instance.Local.transform.parent.GetComponent<PlayerControl>().CancleInvisible();

        float rotatingAngle;

        GameObject warningSquare = FavoriteFunction.WarningSquare(transform.position, 1f, 0.2f); ;
        warningSquare.transform.localScale = new Vector3(60f, 0.5f, 1f);
        /* 상대쪽으로 발사
        DVector = commuOb.transform.position - transform.position;
        DVector.Normalize();

        rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
        warningSquare.transform.Rotate(Vector3.forward, rotatingAngle);
        */
        if (shooterNum == 1)
        {
            DVector = commuOb.transform.position - transform.position;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            warningSquare.transform.Rotate(Vector3.forward, rotatingAngle);
        }
        else
        {
            DVector = commuOb.transform.position - transform.position;
            DVector.Normalize();

            rotatingAngle = DVector.y > 0 ? Vector3.Angle(DVector, Vector3.right) : -Vector3.Angle(DVector, Vector3.right);
            warningSquare.transform.Rotate(Vector3.forward, rotatingAngle);
        }
        yield return new WaitForSeconds(0.2f);

        if (photonView.isMine)
        {
            Re_Iris_Bullet_3 bul;
            bul = PhotonNetwork.Instantiate
            ("Re_Iris_Bullet_3", transform.position, Quaternion.identity, 0).
            GetComponent<Re_Iris_Bullet_3>();

            PhotonView view = commuOb.GetComponent<PhotonView>();
            bul.Init_Iris_Bullet_3(GameManager.instance.myPnum, view.viewID);
        }

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);
    }

    protected override void DestroyCallBack()
    {
        StartCoroutine(ShootLaser());
    }

    Vector3 originalMoveVector;
    
    protected override void Update()
    {
        base.Update();
        /*
        if (!photonView.isMine)
            return;

        Vector3 tempVec3 = GameManager.instance.Local.transform.position;
        originalMoveVector = tempVec3 - originalMoveVector;

        transform.Translate(originalMoveVector);
        */
    }
}
