using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이제 자기가 맡은 캐릭터의 컨트롤러를 마음대로 수정합시다~
/// </summary>
///
public enum SkillID{
    SKILL1, SKILL2, SKILL3, SKILL4, SKILL5, SKILL6
}

public class IrisControl : PlayerControl
{
    protected override void Awake()
    {
        base.Awake();

        cooltime[0] = 2f;
        cooltime[1] = 3f;
        cooltime[2] = 7f;
        cooltime[3] = 7f;
        cooltime[4] = 5f;
        cooltime[5] = 45f; //

        cost[2] = 4;
        cost[3] = 3;
        cost[4] = 2;
        cost[5] = 8;
    }

    public override void SkillControl()
    {
        //TODO: 각자 skillNum에따라 스킬 발동되게 작업

        if (InputSystem.instance.button1Pressed && playerData.cooltime[(int)SkillID.SKILL1] <= 0f)
        {
            DoSkill((int)SkillID.SKILL1);
            //playerAnimation.AddAnimationLayer(3,false);
           // SetAnimationLayerEmpty(0.667f);
        }
        else if (InputSystem.instance.button2Pressed && playerData.cooltime[(int)SkillID.SKILL2] <= 0f)
        {
            DoSkill((int)SkillID.SKILL2);//Skill1
        }
        else if (InputSystem.instance.button3Pressed && playerData.cooltime[(int)SkillID.SKILL3] <= 0f)
        {
            DoSkill((int)SkillID.SKILL3);//Skill2
        }
        else if (InputSystem.instance.button4Pressed && playerData.cooltime[(int)SkillID.SKILL4] <= 0f)
        {
            DoSkill((int)SkillID.SKILL4);//Skill2
        }
        else if (InputSystem.instance.button5Pressed && playerData.cooltime[(int)SkillID.SKILL5] <= 0f)
        {
            DoSkill((int)SkillID.SKILL5);
        }
        
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            AudioController.instance.PlayEffectSound(Character.IRIS, 7);
        }
        
        else if (InputSystem.instance.button6Pressed && playerData.cooltime[(int)SkillID.SKILL6] <= 0f)
        {
            DoSkill((int)SkillID.SKILL6);
            //GameManager.instance.Local.CurrentSkillGage -= 100;
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
