using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill4_Pray : Skills {

	public float praying_time;
	public bool praying;
	GameObject impact;
	void Start()
	{
		transform.parent.GetComponent<DianaControl> ().pray = gameObject;
		transform.parent.GetComponent<DianaControl> ().skill4_create = true;
	}
	public override void Excute ()
	{
		if (!praying) 
		{
			impact= PhotonNetwork.Instantiate("Diana_Pray",transform.position,Quaternion.identity,0);
			impact.transform.SetParent (transform);
			StartCoroutine (Pray ());
		}
		else 
		{
			praying = false;
			impact.GetComponent<Diana_pary_aura> ().DestroyToServer ();
		}
			
	}
	IEnumerator Pray()
	{
		
		float praying_time_temp=0;
		praying = true;
		while (true)
		{

			PlayerManager.instance.GetPlayerByNum(PlayerManager.instance.myPnum).GetFetter(Time.deltaTime);
			praying_time_temp += Time.deltaTime;
            if (praying_time_temp > 1f)
            {
            }
			if (!praying) 
			{
				break;
			}
            yield return null;
		}
		praying_time += praying_time_temp;
	}
}
