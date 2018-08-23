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
}
