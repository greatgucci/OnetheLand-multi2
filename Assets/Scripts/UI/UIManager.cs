using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    public Image p1CharacterPortrait, p2CharacterPortrait;
    Image p1Skg, p2Skg;
    Image p1Hp, p2Hp;
    public Text timeText;
    private void Awake()
    {
        instance = this;

        p1Hp = transform.Find("HpUI").Find("P1Status").Find("HPGuage1").GetComponent<Image>();
        p2Hp = transform.Find("HpUI").Find("P2Status").Find("HPGuage2").GetComponent<Image>();

        p1Skg = transform.Find("HpUI").Find("P1Status").Find("SkillGuage1").GetComponent<Image>();
        p2Skg = transform.Find("HpUI").Find("P2Status").Find("SkillGuage2").GetComponent<Image>();

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

    public void SetPortrait(Character player1,Character player2)
    {
        switch(player1)
        {
            case Character.DIANA:
                break;

            case Character.IRIS:
                p1CharacterPortrait.sprite = Resources.Load<Sprite>("UI/Assets/battleui/BattleUI_Face_Iris");
                break;
        }
        switch(player2)
        {
            case Character.DIANA:
                break;

            case Character.IRIS:
                p2CharacterPortrait.sprite = Resources.Load<Sprite>("UI/Assets/battleui/BattleUI_Face_Iris");
                break;
        }
    }
    public void StartTime()
    {
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
    /*
    Text p1Hp, p1Skg, p2Hp, p2Skg;
    private void Awake()
    {
        instance = this;
        p1Hp = transform.Find("P1Status").Find("hp").GetComponent<Text>();
        p1Skg = transform.Find("P1Status").Find("skg").GetComponent<Text>();

        p2Hp = transform.Find("P2Status").Find("hp").GetComponent<Text>();
        p2Skg = transform.Find("P2Status").Find("skg").GetComponent<Text>();
    }
    public void SetHp(int pNum,float hp)
    {
        if(pNum == 1)
        {
            p1Hp.text = hp.ToString();
        }else if(pNum ==2)
        {
            p2Hp.text = hp.ToString();
        }
    }
    public void SetSkg(int pNum, float skg)
    {
        if(pNum == 1)
        {
            p1Skg.text = skg.ToString();
        }
        else if(pNum ==2)
        {
            p2Skg.text = skg.ToString();
        }
    }
    */

}
