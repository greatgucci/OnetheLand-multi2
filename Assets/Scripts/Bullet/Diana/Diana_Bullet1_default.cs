using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet1_default : Bullet {

	protected override void Move(int _shooterNum)
	{
		speed =7f;
		if(shooterNum == 1)
		{
			rgbd.velocity = new Vector2(speed, 0);
		}else
		{
			transform.localScale = new Vector3(-1, 1, 1);
			rgbd.velocity = new Vector2(-speed, 0);
		}
	}


}
