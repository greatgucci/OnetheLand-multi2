using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_default_Range : Bullet 
{
    Vector3 dVector;
    GameObject parent;

    public void Init_Diana_Bullet2_default_Range(int _shooterNum, int domicil_gameobject, Vector3 dVector)
	{
		photonView.RPC ("Init_Diana_Bullet2_default_Range_RPC", PhotonTargets.All,_shooterNum , domicil_gameobject, dVector);
	}
	[PunRPC]
	private void Init_Diana_Bullet2_default_Range_RPC(int _shooterNum, int domicil_gameobject, Vector3 dVector)
	{
		SetTag (type.warning);
		parent = PhotonView.Find (domicil_gameobject).gameObject;
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
        this.dVector = dVector;
		transform.SetParent(parent.transform);
		StartCoroutine(MoveDianaSkillLine());
	}
   
    protected override void OnTriggerStay2D(Collider2D collision)
	{
	}
	IEnumerator MoveDianaSkillLine()//질문을 해보도록 함. 과연 레이저 형식이 나은지 아니면 탄환이 나은지
	{
		float timer = 0f;

		while (true)
		{

			if (timer > 1f)
			{
				break;
			}
            transform.localPosition = new Vector3(0f, 0f, 0f);
            transform.rotation = Quaternion.identity;
            DVector = (dVector.normalized - parent.transform.position.normalized).normalized;
            Debug.Log(DVector);
            FavoriteFunction.RotateBullet(gameObject);
            timer += Time.deltaTime;
			yield return null;
		}
		if (PlayerManager.instance.myPnum == shooterNum) {
			Diana_Bullet2_default d_b_d = PhotonNetwork.Instantiate("Diana_Skill2Line", transform.position, Quaternion.identity, 0).GetComponent<Diana_Bullet2_default>();
			d_b_d.Init_Diana_Bullet2_default(PlayerManager.instance.myPnum,DVector);
			DestroyToServer();
		}
	}

}

