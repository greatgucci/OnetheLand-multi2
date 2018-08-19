using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet4 : Bullet
{
	GameObject[] cross = new GameObject[4];
	Diana_Bullet4_Cross[] cross_component= new Diana_Bullet4_Cross[4];
	Vector3[] dVector= {Vector3.left,Vector3.down,Vector3.right,Vector3.up};
	bool stop=false;
	bool shoot=false;
	int timer=0;
	int bullet_restain=0;
    public void Init_Diana_Bullet4(int _shooterNum)
    {
        photonView.RPC("Init_Diana_Bullet4_RPC", PhotonTargets.All, _shooterNum);
    }
    [PunRPC]
    protected void Init_Diana_Bullet4_RPC(int _shooterNum)
    {
		shooterNum = _shooterNum;
		if (shooterNum == 1)
		{
			oNum = 2;
		}
		else if (shooterNum == 2)
		{
			oNum = 1;
		}
		cross [0] = PhotonNetwork.Instantiate ("Diana_Bullet4_Cross_Left",new Vector3(0f,0f,0f),Quaternion.identity,0);
		cross [2] = PhotonNetwork.Instantiate ("Diana_Bullet4_Cross_Right",new Vector3(0f,0f,0f),Quaternion.identity,0);
		cross [3] = PhotonNetwork.Instantiate ("Diana_Bullet4_Cross_Up", new Vector3 (0f, 0f, 0f), Quaternion.identity, 0);
		cross [1] = PhotonNetwork.Instantiate ("Diana_Bullet4_Cross_Down",new Vector3(0f,0f,0f),Quaternion.identity,0);
		for (int i = 0; i < 4; i++) {
			cross [i].transform.SetParent (gameObject.transform);
			cross_component [i] = cross [i].GetComponent<Diana_Bullet4_Cross>();
			cross_component[i].DVector=dVector[i];
			cross_component [i].Init_Diana_Bullet4_Cross (PlayerManager.instance.myPnum, dVector[i]);
		}
		StartCoroutine(Rotate_Cross());
		StartCoroutine(ShootBullet());
    }
	private IEnumerator Rotate_Cross()
    {


        DVector = new Vector3(Mathf.Cos(30 * Mathf.Deg2Rad), Mathf.Sin(30 * Mathf.Deg2Rad), 0f);
        for (int i = 0; i < 12; i++)
		{
			FavoriteFunction.RotateBullet(gameObject);
			shoot = true;
			timer++;
			yield return new WaitForSeconds (1f);
        }
		stop = true;

    }
	private IEnumerator ShootBullet()
	{
		while (!stop) {
			if (shoot) {
				for (int k = 0; k < 4; k++) {
					Create_temp_Bullet (dVector[k], timer, k);
				}
				yield return new WaitForSeconds(0.1f);
				bullet_restain++;
			}
			if (bullet_restain !=0) {
				bullet_restain = 0;
				shoot = false;
			}
			yield return null;
		}
		DestroyToServer ();
	}
	private void Create_temp_Bullet(Vector3 dVector, int i, int k)
	{
		if (PlayerManager.instance.myPnum == shooterNum) {
			Diana_Bullet4_instance d_b_i=PhotonNetwork.Instantiate ("Diana_Bullet4_instance",transform.position,Quaternion.identity,0).GetComponent<Diana_Bullet4_instance>();
			d_b_i.Init_Diana_Bullet4_instance (PlayerManager.instance.myPnum, i, k);
		}
	}
}
