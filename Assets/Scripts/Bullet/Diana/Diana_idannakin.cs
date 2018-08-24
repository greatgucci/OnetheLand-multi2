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
			GetComponent<SpriteRenderer> ().color = new Color (180f,0f,0f,i);
			yield return new WaitForSeconds (0.2f/255);
		}
		//실행
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
