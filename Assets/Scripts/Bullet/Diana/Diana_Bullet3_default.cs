using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet3_default : Bullet 
{
    GameObject warning;
    Vector3 destination;
    float sustain_time;
    Vector3 position_temp;
	float distance;
	float distance_temp;
    bool next= false;
	public void Init_Diana_Bullet3_default(int _shooterNum,Vector3 vector3, Vector3 vector)
	{
		photonView.RPC ("Init_Diana_Bullet3_default_RPC", PhotonTargets.All,_shooterNum, vector3, vector);
	}
	[PunRPC]
	private void Init_Diana_Bullet3_default_RPC(int _shooterNum, Vector3 vector3, Vector3 vector)
	{
		SetTag (type.bullet);
		shooterNum = _shooterNum;
		speed = 10f;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		DVector = vector;
		destination = vector3;
		FavoriteFunction.RotateBullet (gameObject);
		rgbd.velocity = DVector * speed;
		StartCoroutine (Shootbullet ());
	}
	private IEnumerator Shootbullet()
    {
        position_temp = transform.position;
		distance = Mathf.Abs ((transform.position - destination).magnitude);
		if (PlayerManager.instance.myPnum != shooterNum) {
			warning=Instantiate (Resources.Load("Diana_Bullet3_default_Warning") as GameObject,destination,Quaternion.identity);
			warning.GetComponent<Diana_Bullet3_default_Warninig> ().position=destination;
            sustain_time = (destination - transform.position).magnitude / speed;
        }
        Invoke("Warning_delete", sustain_time);
        while (true) {
			distance_temp += Mathf.Abs((transform.position - position_temp).magnitude);
			if(distance<distance_temp)
            {
                while (!next&& PlayerManager.instance.myPnum == shooterNum)
                {
                    yield return null;
                }
                if (PlayerManager.instance.myPnum == shooterNum) {
					Diana_Bullet3_default_firezone d_b_d_w = PhotonNetwork.Instantiate 
						("Diana_Bullet3_default_firezone", destination, Quaternion.identity, 0).GetComponent<Diana_Bullet3_default_firezone> ();
					d_b_d_w.Init_Diana_Bullet3_default_firezone (shooterNum);
				}
				break;
			}
            else
            {
                position_temp = transform.position;
                yield return null;
            }
		}
		if (PlayerManager.instance.myPnum == shooterNum)
			DestroyToServer ();
	}
    protected override void OnTriggerStay2D(Collider2D collision)
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
                Warning_delete();
                DestroyToServer();
            }
            if (collision.gameObject.name == "Graze" && collision.transform.parent.tag == "Player" + oNum)
            {
                PlayerManager.instance.Local.CurrentSkillGage += 1f;
            }
        }
    }
    void Warning_delete()
    {
        if (PlayerManager.instance.myPnum != shooterNum)
            Destroy(warning);
        next = true;
    }

}
