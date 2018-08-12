using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet2_Warning : Bullet {
	Vector3 start_position;
	protected override void Move(int _shooterNum)
	{
		start_position = transform.position;
		speed = 10000;
		DVector = commuObject.GetComponent<Bullet> ().DVector;
		FavoriteFunction.RotateBullet(gameObject);
		rgbd.velocity = DVector * speed;
		StartCoroutine (stay());
	}
	protected override void OnTriggerEnter2D(Collider2D collision)
	{
	}
	IEnumerator stay()
	{
		while (true) {
			
			yield return null;
			if ((transform.position - start_position).magnitude > commuObject.GetComponent<Diana_Bullet2_data> ().distance)
			{
				rgbd.velocity = new Vector2(0,0);
				Invoke ("DestroyToServer",2f);
				break;
			}
		}
	}
}
