using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Photon.PunBehaviour
{
    public float speed=5;
    public int damage=100;
    public float destroyTime = 10;
    public Rigidbody2D rgbd;
    public int shooterNum;
    public int oNum;
	protected enum type {Range_Attack,bullet,warning,etc};
    protected bool isTirggerTime = true;
    private Vector3 dVector;
 
    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        StartCoroutine(TriggerTimer());
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
                //데미지 공식 - 레이저의 경우(디스트로이가 안 되는 경우) ( 20 * 초 * 데미지 )
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
	protected void SetTag(type this_type)
	{
		if (this_type == type.bullet) {
			tag = "Bullet";
		} else if (this_type == type.Range_Attack) {
			tag = "Range_Attack";
		} else if (this_type == type.warning) {
			tag = "Warning";
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
	public virtual void DestroyToServer()
    {
        photonView.RPC("DestroyToServer_RPC", PhotonTargets.All);
    }
    protected virtual void DestroyCallBack()
    {
    }
}
