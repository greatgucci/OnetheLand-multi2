using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltimeUI : MonoBehaviour {

    public Sprite[] dianaSkillIcon;
    public Sprite[] irisSkillIcon;
    public Image[] skillIcon;
    public Image[] skillIconCool;

    private void Awake()
    {
        int cNum = PlayerPrefs.GetInt("Character");

        for (int i = 0; i < 6; i++)
        {
            if (cNum == 0)
            {
                skillIcon[i].sprite = dianaSkillIcon[i];
            }
            if (cNum == 1)
            {
                skillIcon[i].sprite = irisSkillIcon[i];
            }

            skillIconCool[i].fillAmount = 0f;
        }
    }

    public static void SetCoolTimeUI(int i, float coolTime)
    {
        int i_Temp = 0;

        if (i == 0)
        {
            i_Temp = 0;
        }else if (i == 1)
        {
            i_Temp = 1;
        }else if (i == 4)
        {
            i_Temp = 2;
        }else if (i == 9)
        {
            i_Temp = 3;
        }else if (i == 2)
        {
            i_Temp = 4;
        }else if (i == 3)
        {
            i_Temp = 5;
        }
        else
        {
            return;
        }
        

        Debug.Log(GameObject.Find("SkillIcon" + i_Temp).transform.Find("Image").name);

        GameObject.Find("SkillIcon" + i_Temp).transform.Find("Image")
            .GetComponent<SkillCooltimeUIDisplay>().SetCoolDisplay(coolTime);
    }
}
