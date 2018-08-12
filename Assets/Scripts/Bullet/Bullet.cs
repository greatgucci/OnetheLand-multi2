using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Photon.PunBehaviour
{
    public float speed=5;
    public int damage=100;
    public float destroyTime = 10;
    protected Rigidbody2D rgbd;
    public int shooterNum;
    public int oNum;
    protected GameObject commuObject;
    protected GameObject[] commuObjects;

    protected bool isTirggerTime = true;
    private Vector3 dVector;
 
    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        StartCoroutine(TriggerTimer());
    }

    public void Init(int _shooterNum)
    {
        photonView.RPC("Init_RPC", PhotonTargets.All,_shooterNum);
    }

    public void Init(int _shooterNum, int communicatingObject)
    {
        photonView.RPC("Init_RPC", PhotonTargets.All, _shooterNum, communicatingObject);
    }
    public void Init(int _shooterNum,int[] communicatingObjects)
    {
        photonView.RPC("Init_RPC", PhotonTargets.All, _shooterNum, communicatingObjects);
    }

    /// <summary>
    /// 슛터 넘버를 서버에 전달
    /// </summary>
    [PunRPC]
    protected void Init_RPC(int _shooterNum)
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
    
    /// <summary>
    /// 슛터 넘과 상호작용하는 오브젝트 1개 전달
    /// </summary>
    [PunRPC]
    protected void Init_RPC(int _shooterNum, int communicatingObject)
    {
        commuObject = PhotonView.Find(communicatingObject).gameObject;
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
    /// <summary>
    /// 슛터넘과 상호작용하는 오브젝트 여러개 전달
    /// </summary>
    [PunRPC]
    protected void Init_RPC(int _shooterNum, int[] communicatingObjects)
    {
        for(int i=0; i<communicatingObjects.Length;i++)
        {
            commuObjects[i] = PhotonView.Find(communicatingObjects[i]).gameObject;
        }
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

    /// <summary>
    /// 서버를 통해 파괴
    /// </summary>
    [PunRPC]
    protected void DestroyToServer_RPC()
    {
        DestroyCallBack();
        Destroy(gameObject);
    }
    protected virtual void Move(int _shooterNum) {}

    protected virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (isTirggerTime == true)
        {
            if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
            {
                return;
            }

            if (collision.tag == "Player" + oNum)
            {
                PlayerManager.instance.Local.CurrentHp -= damage;
                DestroyToServer();
            }
            if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
            {
                PlayerManager.instance.Local.CurrentSkillGage += 1f;
            }
        }
    }
    
    

    IEnumerator TriggerTimer()
    {
        float timer = 0f;

        while(true)
        {
            isTirggerTime = false;

            while(true)
            {
                if (timer >= 0.1f)
                {
                    break;
                }

                timer += Time.deltaTime;
                yield return null;
            }

            isTirggerTime = true;

            yield return null;
        }
    }

    public Vector3 DVector
    {
        get { return dVector; }
        set { dVector = value; }
    }
    protected virtual void DestroyToServer()
    {
        photonView.RPC("DestroyToServer_RPC", PhotonTargets.All);
    }
    protected virtual void DestroyCallBack()
    {
    }
}
