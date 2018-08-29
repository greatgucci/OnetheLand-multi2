using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    Image p1Portrait, p2Portrait;
    Image p1CharacterEvent, p2CharacterEvent;
    Text timeText;

    Image p1Skg, p2Skg;
    Image p1Hp, p2Hp;
    Image GameStartImage;
    Animator animator;
    Image timeRed;
    Image vsImage;
    Image you1,you2;
    Transform SkillIcons;
    public Sprite Iris, Diana;
    //public Sprite[] StartEventTimerSprites;

    private void Awake()
    {
        instance = this;

        p1Hp = transform.Find("HpUI").Find("P1Status").Find("HPGuage1").GetComponent<Image>();
        p2Hp = transform.Find("HpUI").Find("P2Status").Find("HPGuage2").GetComponent<Image>();

        p1Skg = transform.Find("HpUI").Find("P1Status").Find("SkillGuage1").GetComponent<Image>();
        p2Skg = transform.Find("HpUI").Find("P2Status").Find("SkillGuage2").GetComponent<Image>();

        p1Portrait = transform.Find("HpUI").Find("P1Status").Find("CharacterFrame1").Find("CharacterImage").GetComponent<Image>();
        p2Portrait = transform.Find("HpUI").Find("P2Status").Find("CharacterFrame2").Find("CharacterImage").GetComponent<Image>();

        p1CharacterEvent = transform.Find("Event").Find("P1Event").GetComponent<Image>();
        p2CharacterEvent = transform.Find("Event").Find("P2Event").GetComponent<Image>();
        vsImage = transform.Find("Event").Find("Vs").GetComponent<Image>();
        
        timeText = transform.Find("texts").Find("Time").GetComponent<Text>();

        GameStartImage = transform.Find("Event").Find("Timer").GetComponent<Image>();
        timeRed = timeText.transform.Find("ClockBG").GetComponent<Image>();
        SkillIcons = transform.Find("SkillIcon");
        you1 = transform.Find("HpUI").Find("P1Status").Find("You").GetComponent<Image>();
        you2 = transform.Find("HpUI").Find("P2Status").Find("You").GetComponent<Image>();
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
        Debug.Log("Player1 is " +player1.ToString() + "  Player2 is"+player2.ToString());
        switch(player1)
        {
            case Character.DIANA:
                p1Portrait.sprite = Diana;
                break;

            case Character.IRIS:
                p1Portrait.sprite = Iris;
                break;
        }
        switch(player2)
        {
            case Character.DIANA:
                p2Portrait.sprite = Diana;
                break;

            case Character.IRIS:
                p2Portrait.sprite = Iris;
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
                p1CharacterEvent.sprite = Resources.Load<Sprite>("UI/Assets/StandingCharacter_Diana_NEW");
                break;
            case Character.IRIS:
                p1CharacterEvent.sprite = Resources.Load<Sprite>("UI/Assets/StandingCharacter_Iris");
                break;
        }
        switch (player2)
        {
            case Character.DIANA:
                p2CharacterEvent.sprite = Resources.Load<Sprite>("UI/Assets/StandingCharacter_Diana_NEW");
                break;

            case Character.IRIS:
                p2CharacterEvent.sprite = Resources.Load<Sprite>("UI/Assets/StandingCharacter_Iris");
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

    public void StartEventTimerOn()
    {
        GameStartImage.enabled = true;
    }
    public void StartEventTimerOff()
    {
        GameStartImage.enabled = false;
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
    public void PlayStartAnimation()
    {
        animator.Play("Start");
    }
    public void PlayEndBlackAnimation()
    {
        animator.Play("Black");
    }
    public void SkillIconMove(bool b)
    {
        if(b)
        {
            SkillIcons.position = new Vector3(0, 2000, 0);
        }
        else
        {
            SkillIcons.position = new Vector3(960, 540, 0);
        }
    }
    public void VsImage(bool b)
    {
        vsImage.enabled = b;
    }
    public void YouImageOn(int i)
    {
        if(i ==1)
        {
            you1.enabled = true;
        }else if(i==2)
        {
            you2.enabled = true;
        }
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
        bool isRedOn=false;
        while (t > 0)
        {
            if(t<50 && !isRedOn)
            {
                isRedOn = true;
                timeRed.enabled = true;
            }
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
