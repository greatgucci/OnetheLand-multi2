using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill3_Impact : Bullet {

	GameObject parentObject;
	public void shadow(int _shooterNum, int domicil_num)
	{
		photonView.RPC ("Shadow_RPC",PhotonTargets.All,_shooterNum,domicil_num);
	}
	[PunRPC]
	void Shadow_RPC(int _shooterNum,int domicil_num)
	{
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
		parentObject = PhotonView.Find (domicil_num).gameObject;
		StartCoroutine (Casting_Pray());
	}
	protected override void OnTriggerStay2D (Collider2D collision)
	{
		
	}
	public IEnumerator Casting_Pray()
	{
		float time=0;
		//float CurrentHp;
		float distance;
		//CurrentHp=PlayerManager.instance.Local.CurrentHp;
		if (!parentObject.transform.parent.GetComponent<DianaControl> ().pray.GetComponent<Diana_Skill4_Pray> ().praying) {
			while (time < 0.2f/*&&CurrentHp==PlayerManager.instance.Local.CurrentHp*/) {
				parentObject.transform.parent.GetComponent<DianaControl>().OnStartPrayAnimation();
				time += Time.deltaTime;
				//기도 모션
				yield return null;
			}
			parentObject.transform.parent.GetComponent<DianaControl>().OnCanclePrayAnimation();
		}
			
		DVector = (PlayerManager.instance.GetPlayerByNum (oNum).transform.position-parentObject.transform.position);
		distance=DVector.magnitude+1;
		parentObject.transform.parent.gameObject.transform.Translate(DVector.normalized * distance);
		yield return new WaitForSeconds (1f);
		if(PlayerManager.instance.myPnum == shooterNum)
			DestroyToServer();
	}
}
