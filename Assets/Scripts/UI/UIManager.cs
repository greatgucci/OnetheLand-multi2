using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    Image p1CharacterPortrait, p2CharacterPortrait;
    Image p1CharacterEvent, p2CharacterEvent;
    Text timeText;

    Image p1Skg, p2Skg;
    Image p1Hp, p2Hp;
    Text StartEventTimer;
    Animator animator;
    //public Sprite[] StartEventTimerSprites;

    private void Awake()
    {
        instance = this;

        p1Hp = transform.Find("HpUI").Find("P1Status").Find("HPGuage1").GetComponent<Image>();
        p2Hp = transform.Find("HpUI").Find("P2Status").Find("HPGuage2").GetComponent<Image>();

        p1Skg = transform.Find("HpUI").Find("P1Status").Find("SkillGuage1").GetComponent<Image>();
        p2Skg = transform.Find("HpUI").Find("P2Status").Find("SkillGuage2").GetComponent<Image>();

        p1CharacterPortrait = transform.Find("HpUI").Find("P1Status").Find("CharacterFrame1").Find("CharacterImage").GetComponent<Image>();
        p1CharacterPortrait = transform.Find("HpUI").Find("P2Status").Find("CharacterFrame2").Find("CharacterImage").GetComponent<Image>();

        p1CharacterEvent = transform.Find("Event").Find("P1Event").GetComponent<Image>();
        p2CharacterEvent = transform.Find("Event").Find("P2Event").GetComponent<Image>();
        timeText = transform.Find("texts").Find("Time").GetComponent<Text>();

        StartEventTimer = transform.Find("Event").Find("Timer").GetComponent<Text>();
        animator = GetComponent<Animator>();
    }

    public void SetHp(int pNum, float hp)
    {
        if(pNum == 1)
        {
            p1Hp.fillAmount = hp;
        }
        else if (pNum == 2)
        {
            p2Hp.fillAmount = hp;
        }
    }

    public void SetSkg(int pNum, float skg)
    {
        if (pNum == 1)
        {
            p1Skg.fillAmount = skg;
        }
        else if(pNum ==2)
        {
            p2Skg.fillAmount = skg;
        }
    }
    #region GameEvent
    public void SetPortrait(Character player1,Character player2)
    {
        switch(player1)
        {
            case Character.DIANA:
                p1CharacterPortrait.sprite = Resources.Load<Sprite>("UI/Assets/battleui/BattleScreen_Face_Diana_NEW");
                break;

            case Character.IRIS:
                p1CharacterPortrait.sprite = Resources.Load<Sprite>("UI/Assets/battleui/BattleUI_Face_Iris");
                break;
        }
        switch(player2)
        {
            case Character.DIANA:
                p2CharacterPortrait.sprite = Resources.Load<Sprite>("UI/Assets/battleui/BattleScreen_Face_Diana_NEW");
                break;

            case Character.IRIS:
                p2CharacterPortrait.sprite = Resources.Load<Sprite>("UI/Assets/battleui/BattleUI_Face_Iris");
                break;
        }
    }

    /// <summary>
    ///  시작할때 캐릭터 이미지 옆에 뜨는거
    /// </summary>
    public void CharacterStartOn(Character player1,Character player2)
    {
        p1CharacterEvent.enabled = true;
        p2CharacterEvent.enabled = true;

        switch (player1)
        {
            case Character.DIANA:
                p1CharacterEvent.sprite = Resources.Load<Sprite>("UI/Assets/DialogueScreen_CharacterImage_Diana_Normal_NEW");
                break;
            case Character.IRIS:
                p1CharacterEvent.sprite = Resources.Load<Sprite>("UI/Assets/DialogueScreen_CharacterImage_Iris_Normal");
                break;
        }
        switch (player2)
        {
            case Character.DIANA:
                p2CharacterPortrait.sprite = Resources.Load<Sprite>("UI/Assets/DialogueScreen_CharacterImage_Diana_Normal_NEW");
                break;

            case Character.IRIS:
                p2CharacterPortrait.sprite = Resources.Load<Sprite>("UI/Assets/DialogueScreen_CharacterImage_Iris_Normal");
                break;
        }
    }
    public void CharacterStartOff()
    {
        p1CharacterEvent.enabled = false;
        p2CharacterEvent.enabled = false;
    }
    public void SetCharacterStart(int player,bool b)
    {
        if(b)
        {
            switch(player)
            {
                case 1:
                    p1CharacterEvent.color = Color.white;
                    break;
                case 2:
                    p2CharacterEvent.color = Color.white;
                    break;
            }
        }else
        {
            switch (player)
            {
                case 1:
                    p1CharacterEvent.color = Color.grey;
                    break;
                case 2:
                    p2CharacterEvent.color = Color.grey;
                    break;
            }
        }
    }

    public void StartEventTimerUpdate(int i)
    {
        if (i == 3)
        {
            StartEventTimer.text = "Start";
            return;
        }

        StartEventTimer.text = "" + (3-i);
        //StartEventTimer.sprite = StartEventTimerSprites[i];
    }
    public void StartEventTimerOn()
    {
        StartEventTimer.enabled = true;
    }
    public void StartEventTimerOff()
    {
        StartEventTimer.enabled = false;
    }
    public void WinnerCharacterOn(int winner)
    {
        if(winner==1)
        {
            p1CharacterEvent.enabled = true;
        }else
        {
            p2CharacterEvent.enabled = true;
        }
    }
    public void PlayEndBlackAnimation()
    {
        animator.Play("Black", -1, 0f);
    }
    #endregion


    public void StartTimer()
    {
        timeText.enabled = true;
        timer = StartCoroutine(TimeCount());
    }
    public void StopTime()
    {
        StopCoroutine(timer);
    }
    Coroutine timer;
    IEnumerator TimeCount()
    {
        float t = 180;
        while (t > 0)
        {
            t -= Time.deltaTime;
            timeText.text = "" + (int)t;
            yield return null;
        }
        if (PhotonNetwork.isMasterClient)
        {
            if (PlayerManager.instance.GetPlayerByNum(1).CurrentHp >= PlayerManager.instance.GetPlayerByNum(2).CurrentHp)
            {
                NetworkManager.instance.GameOver(2);
            }
            else if (PlayerManager.instance.GetPlayerByNum(1).CurrentHp < PlayerManager.instance.GetPlayerByNum(2).CurrentHp)
            {
                NetworkManager.instance.GameOver(1);
            }
        }
    }

}
