using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이제 자기가 맡은 캐릭터의 컨트롤러를 마음대로 수정합시다~
/// </summary>
public class IrisControl : PlayerControl
{
    protected override void MoveControl()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    protected override void SkillControl()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerData.cooltime[0] <= 0)
        {
            DoSkill(0);
           playerAnimation.AddAnimationLayer(3,false);
            SetAnimationLayerEmpty(0.667f);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && playerData.cooltime[1] <= 0f)
        {
            DoSkill(1);//Skill1
           playerAnimation.AddAnimationLayer(4, false);
            SetAnimationLayerEmpty(0.667f);
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerData.cooltime[2] <= 0f)
        {
            DoSkill(2);//Skill2
           playerAnimation.AddAnimationLayer(5, false);
            SetAnimationLayerEmpty(0.667f);
        }
        else if (Input.GetKeyDown(KeyCode.R) && playerData.cooltime[3] <= 0f)
        {
            DoSkill(3);//Skill2
            playerAnimation.AddAnimationLayer(4, false);
            SetAnimationLayerEmpty(0.667f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && playerData.cooltime[4] <= 0f)
        {
            DoSkill(4);
           playerAnimation.AddAnimationLayer(6, false);
            SetAnimationLayerEmpty(0.667f);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerData.cooltime[8] <= 0f)
        {
            Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //?
        }
        else if (Input.GetKeyDown(KeyCode.Q) && playerData.cooltime[9] <= 0f)
        {
            DoSkill(5);
            SetAnimationLayerEmpty(0.667f);
            playerAnimation.AddAnimationLayer(5, false);
        }
    }
    protected override void MoveAnimationChange(MoveState move)
    {
        if (move == moveState)
            return;

        switch (move)
        {
            case MoveState.Idle:
               playerAnimation.ChangeAnim(0,true);
                moveState = MoveState.Idle;
                break;
            case MoveState.MoveFront:
               playerAnimation.ChangeAnim(1,true);
                moveState = MoveState.MoveFront;
                break;
            case MoveState.MoveBack:
               playerAnimation.ChangeAnim(2,true);
                moveState = MoveState.MoveBack;
                break;
        }
    }
}
