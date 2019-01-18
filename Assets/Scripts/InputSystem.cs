using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {

    public static InputSystem instance;

    bool isInputAble = false;
    public bool IsInputAble
    {
        get { return isInputAble; }
        set { isInputAble = value; }
    }

    private void Awake()
    {
        instance = this;
    }

    PlayerControl myPc;

    public void SetMyPlayerControl(PlayerControl pc)
    {
        myPc = pc;
    }

    public bool PlayerMove(float x, float y)
    {
        if (!isInputAble || GameManager.instance.gameUpdate != GameUpdate.INGAME)
        {
            myPc.Stop();
            return false;
        }

        myPc.Move(x, y);
        return true;
    }
    public bool PlayerDoSkill(int i)
    {
        if (!isInputAble || GameManager.instance.gameUpdate != GameUpdate.INGAME)
        {
            return false;
        }

        myPc.SkillControl(i);
        return true;
    }

    
}
