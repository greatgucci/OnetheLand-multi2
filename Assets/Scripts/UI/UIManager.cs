using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    GameObject p1Hp_Temp;
    GameObject p2Hp_Temp;
    GameObject p1Skg_Temp, p2Skg_Temp;
    Image p1Skg, p2Skg;
    Image p1Hp, p2Hp;

    private void Awake()
    {
        instance = this;

        p1Hp_Temp = GameObject.Find("HPGuage1");
        p2Hp_Temp = GameObject.Find("HPGuage2");
        
        p1Skg_Temp = GameObject.Find("SkillGuage1");
        p2Skg_Temp = GameObject.Find("SkillGuage2");

        p1Hp = p1Hp_Temp.GetComponent<Image>();
        p2Hp = p2Hp_Temp.GetComponent<Image>();

        p1Skg = p1Skg_Temp.GetComponent<Image>();
        p2Skg = p2Skg_Temp.GetComponent<Image>();
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
