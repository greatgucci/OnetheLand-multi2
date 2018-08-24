using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_NormalBullet : Bullet{
	GameObject skillObject;

	public void Init_Diana_NormalBullet(int _shooterNum, int domicilnum, Vector3 dVector, bool type)
	{
		photonView.RPC ("Init_Diana_NormalBullet_RPC",PhotonTargets.All,_shooterNum, domicilnum, dVector, type);
	}
	[PunRPC]
	private void Init_Diana_NormalBullet_RPC(int _shooterNum, int domicilnum, Vector3 dVector, bool type)
	{
		skillObject = PhotonView.Find (domicilnum).gameObject;
		DVector = dVector;
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
		speed = 15f;
		damage = 100;
		FavoriteFunction.RotateBullet (gameObject);
		rgbd.velocity = DVector * speed;
        if (type)
            StartCoroutine(Colison(domicilnum));
        else
            transform.localScale *= 0.6f;
        
    }
    IEnumerator Colison(int domicilnum)
    {
        Vector3 stratposition = transform.position;
        Diana_NormalBullet bullet;
        float distance = 0;
        while (true)
        {
            distance += (stratposition - transform.position).magnitude;
            stratposition = transform.position;
            if(distance >4f)
            {
                if (PlayerManager.instance.myPnum == shooterNum)
                {
                    DestroyToServer();
                    bullet = PhotonNetwork.Instantiate("Diana_NormalBullet", transform.position, Quaternion.identity, 0).GetComponent<Diana_NormalBullet>();
                    bullet.Init_Diana_NormalBullet(shooterNum, domicilnum, DVector + new Vector3(0f, 0.2f, 0f), false);
                    bullet = PhotonNetwork.Instantiate("Diana_NormalBullet", transform.position, Quaternion.identity, 0).GetComponent<Diana_NormalBullet>();
                    bullet.Init_Diana_NormalBullet(shooterNum, domicilnum, DVector + new Vector3(0f, 0f, 0f), false);
                    bullet = PhotonNetwork.Instantiate("Diana_NormalBullet", transform.position, Quaternion.identity, 0).GetComponent<Diana_NormalBullet>();
                    bullet.Init_Diana_NormalBullet(shooterNum, domicilnum, DVector + new Vector3(0f, -0.2f, 0f), false);
                }
            }
            yield return null;
        }
        

    }
	protected override void OnTriggerStay2D (Collider2D collision)
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
				Heresy_Sign ();
				SetActiveToServer(true);
				skillObject.transform.parent.GetComponent<DianaControl> ().idannakin.Impact ();
				DestroyToServer();
			}
			if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
			{
				PlayerManager.instance.Local.CurrentSkillGage += 1f;
			}
		}
	}
	void Heresy_Sign()
	{
		photonView.RPC ("Heresy_Sign_RPC",PhotonTargets.All);
	}
	[PunRPC]
	void Heresy_Sign_RPC()
	{
		skillObject.transform.parent.GetComponent<DianaControl> ().skill_can = true;
	}
	public void SetActiveToServer(bool ing)
	{
		photonView.RPC ("SetActiveToServer_RPC", PhotonTargets.All, ing);
	}
	[PunRPC]
	void SetActiveToServer_RPC(bool ing)
	{
		skillObject.transform.parent.GetComponent<DianaControl>().idannakin.gameObject.SetActive (ing);
	}
}
