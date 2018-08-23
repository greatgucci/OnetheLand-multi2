using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill4_Pray : Skills {

	public float praying_time;
	public bool praying;
	void Start()
	{
		transform.parent.GetComponent<DianaControl> ().pray = gameObject;
		transform.parent.GetComponent<DianaControl> ().skill4_create = true;
	}
	public override void Excute ()
	{
		StartCoroutine (Pray ());
	}
	IEnumerator Pray()
	{
		GameObject impact= PhotonNetwork.Instantiate("Diana_Pray",transform.position,Quaternion.identity,0);
		impact.transform.SetParent (transform);
		float praying_time_temp=0;
		praying = true;
		float speed = transform.parent.gameObject.GetComponent<DianaControl> ().speed;
		transform.parent.gameObject.GetComponent<DianaControl> ().speed = 0f;
		while (true)
		{
			
			praying_time_temp += Time.deltaTime;
			if (praying_time_temp>1f)
				praying = true;
			if (praying_time_temp>3f)
				//
			if ((Input.GetKeyUp (KeyCode.Q) || !Input.GetKey (KeyCode.Q))||Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.A)) 
			{
				impact.GetComponent<Diana_pary_aura> ().DestroyToServer ();
				transform.parent.gameObject.GetComponent<DianaControl> ().speed = speed;
				praying = false;
				break;
			}
            yield return null;
		}
		praying_time += praying_time_temp;
	}
}
