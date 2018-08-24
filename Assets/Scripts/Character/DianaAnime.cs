using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaAnime : PlayerAnimation
{
    protected override string IntToAnimationName(int name)
    {
        switch(name)
        {
            case 0:
                return "idle";
            case 1:
                return "moveFront";
            case 2:
                return "moveBack";
            case 3:
                return "BaseAttack1"; 
            case 4:
                return "BaseAttack2";
            case 5:
                return "SkillE";
            case 6:
                return "SkillQandR(Pray)";
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
