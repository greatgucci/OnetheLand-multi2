using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이제 자기가 맡은 캐릭터의 컨트롤러를 마음대로 수정합시다~
/// </summary>
public class DianaControl : PlayerControl {

	public bool skill_can=false;
	public bool skill4_create = false;
	public bool skill1_playing = false;
	public GameObject pray;
	protected override bool LateUpdate()
    {

		if (!base.LateUpdate ()) 
		{
			return false;
		}// *<주의>* 이거 빼먹으면안대용!
		Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerData.cooltime[0] <= 0)
        {
			if (!skill1_playing)
			{
				DoSkill(0);//좌

				pray.GetComponent<Diana_Skill4_Pray> ().praying=false;
			}
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && playerData.cooltime[1] <= 0f)
        {
			DoSkill(1);//우
			pray.GetComponent<Diana_Skill4_Pray> ().praying=false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerData.cooltime[2] <= 0f)
        {
			DoSkill(2);//E skill1
			pray.GetComponent<Diana_Skill4_Pray> ().praying=false;
        }
		else if (Input.GetKeyDown(KeyCode.R) && playerData.cooltime[3] <= 0f)
        {
			DoSkill(3);//R skill2
			pray.GetComponent<Diana_Skill4_Pray> ().praying=false;
        }
		else if ((Input.GetKeyDown(KeyCode.LeftShift) && playerData.cooltime[4] <= 0f)&&skill_can)
        {
			skill_can = false;
			DoSkill(4);//LeftShift skill3
			pray.GetComponent<Diana_Skill4_Pray> ().praying=false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerData.cooltime[8] <= 0f)
        {
			Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
			pray.GetComponent<Diana_Skill4_Pray> ().praying=false;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            DoSkill(5);//Q 궁
		}
		if(skill4_create&&pray.GetComponent<Diana_Skill4_Pray> ().praying_time>=60f)
		{
			//승리
		}
		return true;
    }
}
