using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_idannakin : Photon.PunBehaviour {

	PhotonView view_oponent;
	int oNum=0;
	public void Impact()
	{
		photonView.RPC ("Impact_RPC", PhotonTargets.All);
	}
	[PunRPC]
	void Impact_RPC()
	{
		StartCoroutine (Impacting ());
	}
	IEnumerator Impacting()
	{
		for (float i = 0f; i > 255f; i+=1f) {
			ColorrMap (i);
			yield return new WaitForSeconds (0.2f/255);
		}
		ColorsMap (255);
		yield return new WaitForSeconds (1f);
		ColorrMap (255);
		//실행
	}
	void ColorrMap(float i)
	{
		photonView.RPC ("ColorrMap_RPC", PhotonTargets.All, i);
	}
	[PunRPC]
	void ColorrMap_RPC(float i)
	{
		GetComponent<SpriteRenderer> ().color = new Color(180/255,0,0,i/255);
	}
	void ColorsMap(float i)
	{
		photonView.RPC ("ColorsMap_RPC", PhotonTargets.All, i);
	}
	[PunRPC]
	void ColorsMap_RPC(float i)
	{
		GetComponent<SpriteRenderer> ().color = new Color(1,1,1,i/255);
	}
	public void SetParent(int photonid)
	{
		photonView.RPC ("SetParent_RPC", PhotonTargets.All,photonid);
	}

	[PunRPC]
	void SetParent_RPC(int photonid)
	{
		GameObject parentObject = PhotonView.Find (photonid).gameObject;
		transform.SetParent (parentObject.transform);
	}
	public void DestroyToServer()
	{
		photonView.RPC ("DestroyToServer_RPC", PhotonTargets.All);
	}
	[PunRPC]
	void DestroyToServer_RPC()
	{
		Destroy (gameObject);
	}


}
