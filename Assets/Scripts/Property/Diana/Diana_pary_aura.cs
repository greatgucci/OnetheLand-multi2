using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_pary_aura : Photon.PunBehaviour {

	public void DestroyToServer()
	{
		photonView.RPC ("DestroyToServer_RPC", PhotonTargets.All);
	}
	[PunRPC]
	void DestroyToServer_RPC()
	{
		Destroy (gameObject);
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
}
