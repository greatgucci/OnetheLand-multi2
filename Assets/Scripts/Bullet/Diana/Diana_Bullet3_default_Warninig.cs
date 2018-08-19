using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet3_default_Warninig : Bullet
{
    public Vector3 position;
	void Start()
	{
        Invoke("DestroyToServer",3f);
        SetTag(type.warning);
		transform.position= position;
	}
    protected override void OnTriggerStay2D (Collider2D collision)
	{
		if (collision.gameObject.name=="Diana_Bullet3_default_firezone(clone)"&&collision.gameObject.GetComponent<Bullet>().shooterNum==shooterNum) {
			Debug.Log ("Hello");
			gameObject.GetComponent<Bullet>().DestroyToServer ();
		}
	}
}
