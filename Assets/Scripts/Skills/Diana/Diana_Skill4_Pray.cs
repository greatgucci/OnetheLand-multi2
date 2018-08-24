using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill4_Pray : Skills {

	public float praying_time;
	public bool praying;
	GameObject impact;
	PhotonView view;
	void Start()
	{
		transform.parent.GetComponent<DianaControl> ().pray = gameObject;
		transform.parent.GetComponent<DianaControl> ().skill4_create = true;
		view = GetComponent<PhotonView> ();
	}
	public override void Excute ()
	{
		if (!praying) {
			transform.parent.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0f,0f);
			StartCoroutine (Pray ());
		} else {
			praying = false;
		}
	}
	IEnumerator Pray()
	{
		impact=PhotonNetwork.Instantiate("Diana_Pray",transform.position,Quaternion.identity,0);
		impact.GetComponent<Diana_pary_aura> ().SetParent(view.viewID);
		float praying_time_temp=0;
		praying = true;
		while (true)
		{

			PlayerManager.instance.GetPlayerByNum(PlayerManager.instance.myPnum).GetFetter(Time.deltaTime);
			praying_time_temp += Time.deltaTime;
            if (praying_time_temp > 1f)
            {
				PlayerManager.instance.Local.Defense = (praying_time_temp + 2) / 10;
				if (PlayerManager.instance.Local.Defense >= 0.6f)
					PlayerManager.instance.Local.Defense = 0.6f;
			}
			if (!praying) 
			{
				impact.GetComponent<Diana_pary_aura> ().DestroyToServer ();
				break;
			}
            yield return null;
		}
		praying_time += praying_time_temp;
	}
}
