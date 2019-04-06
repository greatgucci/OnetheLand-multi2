using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;


    Text p1Text, p2Text;
    Text timeText;

    Image p1Skg, p2Skg;
    Text p1Hp, p2Hp;
    Animator animator;
    public Sprite Iris, Diana;
    //public Sprite[] StartEventTimerSprites;

    private void Awake()
    {
        instance = this;

        p1Hp = transform.Find("HpUI").Find("P1Status").Find("HPGuage1").GetComponent<Text>();
        p2Hp = transform.Find("HpUI").Find("P2Status").Find("HPGuage2").GetComponent<Text>();

        p1Skg = transform.Find("HpUI").Find("P1Status").Find("SkillGuage1").GetComponent<Image>();
        p2Skg = transform.Find("HpUI").Find("P2Status").Find("SkillGuage2").GetComponent<Image>();
        p1Text = transform.Find("HpUI").Find("P1Status").Find("p1Text").GetComponent<Text>();
        p2Text = transform.Find("HpUI").Find("P2Status").Find("p2Text").GetComponent<Text>();

        timeText = transform.Find("texts").Find("Time").GetComponent<Text>();    
        animator = GetComponent<Animator>();
    }

    public void SetHp(int pNum, float damage)
    {
        if(pNum == 1)
        {
            p1Hp.text = damage.ToString();
        }
        else if (pNum == 2)
        {
            p2Hp.text = damage.ToString();
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
    public void PlayEndBlackAnimation()
    {
        animator.Play("Black");
    }
    
    public void SetPlayerText()
    {
            p1Text.enabled = true;
            switch(GameManager.instance.GetPlayerByNum(1).character)
            {
                case Character.DIANA:
                    p1Text.text = "다이애나";
                    break;
                case Character.IRIS:
                    p1Text.text = "아이리스";
                    break;
                case Character.PUPPET:
                 p1Text.text = "연습용 인형";
                break;
                default:
                p1Text.text = "Missing";
                break;
            }
        
            p2Text.enabled = true;
            switch (GameManager.instance.GetPlayerByNum(2).character)
            {
                case Character.DIANA:
                    p2Text.text = "다이애나";
                    break;
                case Character.IRIS:
                    p2Text.text = "아이리스";
                    break;
            case Character.PUPPET:
                p2Text.text = "연습용 인형";
                break;
            default:
                p2Text.text = "Missing";
                break;
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
        while (t > 0)
        {
            t -= Time.deltaTime;
            timeText.text = "" + (int)t;
            yield return null;
        }
        if (PhotonNetwork.isMasterClient)
        {
            if(GameManager.instance.gameMode == GameMode.DAMAGE)
            {
                //TODO: 데미지

            }else if(GameManager.instance.gameMode == GameMode.HP)
            {
                if (GameManager.instance.GetPlayerByNum(1).CurrentDamage >= GameManager.instance.GetPlayerByNum(2).CurrentDamage)
                {
                    NetworkManager.instance.GameOver(2);
                }
                else if (GameManager.instance.GetPlayerByNum(1).CurrentDamage < GameManager.instance.GetPlayerByNum(2).CurrentDamage)
                {
                    NetworkManager.instance.GameOver(1);
                }
            }
        }
    }

}
