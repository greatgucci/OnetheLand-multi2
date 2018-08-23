using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisAnime : PlayerAnimation
{

    protected override string IntToAnimationName(int name)
    {
        switch (name)
        {
            case 0:
                return "idle";
            case 1:
                return "MoveFront";
            case 2:
                return "MoveBack";
            case 3:
                return "BaseAttack1";
            case 4:
                return "BaseAttack2andR";
            case 5:
                return "SkillEandQ";
            case 6:
                return "Shift";
            case 7:
                return "win";
            case 8:
                return "lose";
            default:
                Debug.Log("Int To AnimationName 오류");
                return "";
        }
    }
}
