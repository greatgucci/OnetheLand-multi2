using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이제 자기가 맡은 캐릭터의 컨트롤러를 마음대로 수정합시다~
/// </summary>
public class DianaControl : PlayerControl {

    protected override void LateUpdate()
    {
        base.LateUpdate();// *<주의>* 이거 빼먹으면안대용!

        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Mouse0) && playerData.cooltime[0] <= 0)
        {
            DoSkill(0);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && playerData.cooltime[1] <= 0f)
        {
            DoSkill(1);//Skill1
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerData.cooltime[2] <= 0f)
        {
            DoSkill(2);//Skill2
        }
        else if (Input.GetKeyDown(KeyCode.R) && playerData.cooltime[3] <= 0f)
        {
            DoSkill(3);//Skill2
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && playerData.cooltime[4] <= 0f)
        {
            DoSkill(4);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerData.cooltime[8] <= 0f)
        {
            Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }
        else if (Input.GetKeyDown(KeyCode.Q) && playerData.cooltime[9] <= 0f)
        {
            DoSkill(5);
        }

    }
}
