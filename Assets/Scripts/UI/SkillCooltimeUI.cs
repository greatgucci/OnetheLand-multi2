using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltimeUI : MonoBehaviour {

    public Sprite[] dianaSkillIcon;
    public Sprite[] irisSkillIcon;
    public Image[] skillIcon;
    public Image[] skillIconCool;
    public Text[] skillCoolText;

    private PlayerControl pControl;

    public static SkillCooltimeUI Instance { get; set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;

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
    private void Start()
    {
        /*
        pControl = GameManager.instance.Local.GetComponentInParent<PlayerControl>();

        for (int i = 0; i < 6; i++)
        {

            skillCoolText[i].text = pControl.cost[i].ToString();
            if (pControl.cost[i] == 0)
                skillCoolText[i].text = string.Empty;
        }
        */
    }

    public void SetCoolTimeUI(int i, float coolTime)
    {
        Debug.Log(i);
        skillIconCool[i]
            .GetComponent<SkillCooltimeUIDisplay>().SetCoolDisplay(coolTime);
    }
}
