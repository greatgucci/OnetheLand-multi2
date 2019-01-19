using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이제 자기가 맡은 캐릭터의 컨트롤러를 마음대로 수정합시다~
/// </summary>
///
public class IrisControl : PlayerControl
{
    public override void SkillControl()
    {

        //TODO: 각자 skillNum에따라 스킬 발동되게 작업

        if (InputSystem.instance.button1Pressed && playerData.cooltime[1] <= 0)
        {
            DoSkill(0);
            playerAnimation.AddAnimationLayer(3,false);
            SetAnimationLayerEmpty(0.667f);
            GameManager.instance.Local.SetCooltime(1, 0.7f);
        }
        else if (InputSystem.instance.button2Pressed && playerData.cooltime[2] <= 0f)
        {
            DoSkill(1);//Skill1
            playerAnimation.AddAnimationLayer(4, false);
            SetAnimationLayerEmpty(0.667f);
            GameManager.instance.Local.SetCooltime(2, 1.0f);
        }
        else if (InputSystem.instance.button3Pressed && playerData.cooltime[3] <= 0f)
        {
            DoSkill(5);//Skill2
            playerAnimation.AddAnimationLayer(5, false);
            SetAnimationLayerEmpty(0.667f);
            GameManager.instance.Local.SetCooltime(3, 5f);
        }
        else if (Input.GetKeyDown(KeyCode.R) && playerData.cooltime[3] <= 0f)
        {
            DoSkill(3);//Skill2
            playerAnimation.AddAnimationLayer(4, false);
            SetAnimationLayerEmpty(0.667f);
            GameManager.instance.Local.SetCooltime(3, 5f);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && playerData.cooltime[4] <= 0f)
        {
            DoSkill(4);
            playerAnimation.AddAnimationLayer(6, false);
            SetAnimationLayerEmpty(0.667f);
            GameManager.instance.Local.SetCooltime(4, 4f);
        }
        
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            AudioController.instance.PlayEffectSound(Character.IRIS, 7);
        }
        
        else if (Input.GetKeyDown(KeyCode.Q) && playerData.cooltime[9] <= 0f && GameManager.instance.Local.CurrentSkillGage >= 100f)
        {
            DoSkill(5);
            SetAnimationLayerEmpty(0.667f);
            playerAnimation.AddAnimationLayer(5, false);
            GameManager.instance.Local.CurrentSkillGage -= 100;
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
