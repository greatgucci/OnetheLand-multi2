using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    GameObject[] p1Hp = new GameObject[5];
    GameObject[] p2Hp = new GameObject[5];
    GameObject p1Skg_Temp, p2Skg_Temp;
    Image p1Skg, p2Skg;

    private void Awake()
    {
        instance = this;
        for (int i = 0; i < 5; i++)
        {
            p1Hp[i] = GameObject.Find("P1HP" + (i + 1));
            p2Hp[i] = GameObject.Find("P2HP" + (i + 1));
        }

        p1Skg_Temp = GameObject.Find("SkillGuage1");
        p2Skg_Temp = GameObject.Find("SkillGuage2");

        p1Skg = p1Skg_Temp.GetComponent<Image>();
        p2Skg = p2Skg_Temp.GetComponent<Image>();
    }

    public void SetHp(int pNum, float hp)
    {
        if (pNum == 1)
        {
            for (int i = 0; i < 5; i++)
            {
                if (i <= (hp - 1))
                {
                    p1Hp[i].SetActive(true);
                }
                else
                {
                    p1Hp[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                if (i <= (hp - 1))
                {
                    p2Hp[i].SetActive(true);
                }
                else
                {
                    p2Hp[i].SetActive(false);
                }
            }
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
