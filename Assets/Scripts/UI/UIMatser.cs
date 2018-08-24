using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMatser : MonoBehaviour {

    UISound uiSound;

    Animator animator;
    GameObject uiMain;
    GameObject uiCharacterSelection;
    GameObject uiOption;
    GameObject mainB_Start;
    GameObject mainB_Start_Point;
    GameObject mainB_Option;
    GameObject mainB_Option_Point;
    GameObject mainB_Credit;
    GameObject mainB_Credit_Point;

    public Sprite[] standing;
    public Sprite[] description;
    public Sprite[] irisIcon;  //1번
    public Sprite[] dianaIcon;  //0번
    Image[] skillIcon = new Image[6];
    Image charac_Standing;
    Image charac_Desription;
    GameObject descriptionWindow;
    Text dWindowName;
    Text dWindowDescription;
    Dictionary<string, object> dictionaryDescription;

    private void Awake()
    {
        uiSound = GetComponent<UISound>();
        animator = GetComponent<Animator>();

        mainB_Start = transform.Find("Main").Find("GameStartButtonIdle").gameObject;
        mainB_Start.transform.SetAsLastSibling();
        mainB_Start_Point = mainB_Start.transform.Find("Point").gameObject;
        mainB_Option = transform.Find("Main").Find("OptionButtonIdle").gameObject;
        mainB_Option.transform.SetAsLastSibling();
        mainB_Option_Point = mainB_Option.transform.Find("Point").gameObject;
        mainB_Credit = transform.Find("Main").Find("CreditButtonIdle").gameObject;
        mainB_Credit.transform.SetAsLastSibling();
        mainB_Credit_Point = mainB_Credit.transform.Find("Point").gameObject;

        uiMain = transform.Find("Main").gameObject;
        uiCharacterSelection = transform.Find("CharacterSelection").gameObject;
        uiOption = transform.Find("Option").gameObject;

        charac_Desription = transform.Find("CharacterSelection").Find("CharacterDescription").Find("Description")
            .GetComponent<Image>();
        charac_Standing = transform.Find("CharacterSelection").Find("CharacterDescription").Find("Standing")
            .GetComponent<Image>();

        for (int i = 0; i < 6; i++)
        {
            skillIcon[i] = transform.Find("CharacterSelection").Find("CharacterDescription").Find("Description").Find("Icon" + i).GetComponent<Image>();
        }

        descriptionWindow = transform.Find("CharacterSelection").Find("DescriptionWindow").gameObject;
        dWindowName = descriptionWindow.transform.Find("SkillName").GetComponent<Text>();
        dWindowDescription = descriptionWindow.transform.Find("Skill").GetComponent<Text>();
    }

    private void Start()
    {
        uiMain.SetActive(true);
        uiCharacterSelection.SetActive(false);
        uiOption.SetActive(false);
    }

    #region MainUI
    public void HoverIn_MainB_Start()
    {
        mainB_Start_Point.SetActive(true);
        uiSound.PlayChoose();
    }

    public void HoverOut_MainB_Start()
    {
        mainB_Start_Point.SetActive(false);
    }

    public void Click_MainB_Start()
    {
        animator.Play("Click_Point_Start");
        StartCoroutine(MainB_Start_UILoad());
        uiSound.PlayDecision();
    }

    IEnumerator MainB_Start_UILoad()
    {
        yield return new WaitForSeconds(0.3f);

        uiCharacterSelection.SetActive(true);
        uiMain.SetActive(false);
        mainB_Start_Point.SetActive(false);
        mainB_Option_Point.SetActive(false);
        mainB_Credit_Point.SetActive(false);
    }

    public void HoverIn_MainB_Option()
    {
        mainB_Option_Point.SetActive(true);
        uiSound.PlayChoose();
    }

    public void HoverOut_MainB_Option()
    {
        mainB_Option_Point.SetActive(false);
    }

    public void Click_MainB_Option()
    {
        animator.Play("Click_Point_Option");
        StartCoroutine(MainB_Option_UILoad());
        uiSound.PlayDecision();
    }

    IEnumerator MainB_Option_UILoad()
    {
        yield return new WaitForSeconds(0.3f);

        uiMain.SetActive(false);
        uiOption.SetActive(true);
        mainB_Start_Point.SetActive(false);
        mainB_Option_Point.SetActive(false);
        mainB_Credit_Point.SetActive(false);
    }

    public void HoverIn_MainB_Credit()
    {
        mainB_Credit_Point.SetActive(true);
        uiSound.PlayChoose();
    }

    public void HoverOut_MainB_Credit()
    {
        mainB_Credit_Point.SetActive(false);
    }

    public void Click_MainB_Credit()
    {
        animator.Play("Click_Point_Credit");
        StartCoroutine(MainB_Credit_UILoad());
        uiSound.PlayDecision();
    }

    IEnumerator MainB_Credit_UILoad()
    {
        yield return new WaitForSeconds(0.3f);

        mainB_Start_Point.SetActive(false);
        mainB_Option_Point.SetActive(false);
        mainB_Credit_Point.SetActive(false);
    }
    #endregion

    #region CharacSelecUI
    public void HoverIn()
    {
        uiSound.PlayChoose();
    }

    public void Click_CharacB_BackToMain()
    {
        StartCoroutine(CharacB_BackToMain_UILoad());
        uiSound.PlayDecision();
    }

    IEnumerator CharacB_BackToMain_UILoad()
    {
        yield return new WaitForSeconds(0.2f);

        uiMain.SetActive(true);
        uiCharacterSelection.SetActive(false);

        charac_Desription.gameObject.SetActive(false);
        charac_Standing.gameObject.SetActive(false);
    }

    public void SetCharacterDescriptionUI()
    {
        int j = PlayerPrefs.GetInt("Character");
        charac_Desription.sprite = description[j];
        charac_Standing.sprite = standing[j];

        for (int i = 0; i < 6; i++)
        {
            switch (j)
            {
                case 0:
                    skillIcon[i].sprite = dianaIcon[i];
                    break;
                case 1:
                    skillIcon[i].sprite = irisIcon[i];
                    break;
            }

        }

        charac_Desription.gameObject.SetActive(true);
        charac_Standing.gameObject.SetActive(true);
    }

    public void SkillDescriptionWindow(Transform iconTransform)
    {
        int i = 0;
        descriptionWindow.SetActive(true);

        for (int j = 0; j < 6; j++)
        {
            if (iconTransform.name == "Icon" + j)
            {
                i = j;
            }
        }

        if (i == 0 || i == 1 || i == 3 || i == 4)
        {
            descriptionWindow.transform.position = iconTransform.position + new Vector3(525f, -225f, 0f);
        }
        else
        {
            descriptionWindow.transform.position = iconTransform.position + new Vector3(-300f, -225f, 0f);
        }

        SetTextDescription(i);
    }

    public void SkillDescriptionWindowExit()
    {
        descriptionWindow.SetActive(false);
    }

    public void SetTextDescription(int i)
    {
        dictionaryDescription = DataManager.GetData(PlayerPrefs.GetInt("Character"));
        if (i == 0)
        {
            dWindowName.text = dictionaryDescription["Name_Left"].ToString();
            dWindowDescription.text = dictionaryDescription["Des_Left"].ToString();
        }else if(i == 1)
        {
            dWindowName.text = dictionaryDescription["Name_Right"].ToString();
            dWindowDescription.text = dictionaryDescription["Des_Right"].ToString();
        }
        else if (i == 2)
        {
            dWindowName.text = dictionaryDescription["Name_Shift"].ToString();
            dWindowDescription.text = dictionaryDescription["Des_Shift"].ToString();
        }
        else if (i == 3)
        {
            dWindowName.text = dictionaryDescription["Name_Q"].ToString();
            dWindowDescription.text = dictionaryDescription["Des_Q"].ToString();
        }
        else if (i == 4)
        {
            dWindowName.text = dictionaryDescription["Name_E"].ToString();
            dWindowDescription.text = dictionaryDescription["Des_E"].ToString();
        }
        else if (i == 5)
        {
            dWindowName.text = dictionaryDescription["Name_R"].ToString();
            dWindowDescription.text = dictionaryDescription["Des_R"].ToString();
        }
    }
    #endregion

    #region OptionUI
    public void Click_Option_BackToMain()
    {
        uiSound.PlayDecision();
        StartCoroutine(Option_BackToMain_UILoad());
    }

    IEnumerator Option_BackToMain_UILoad()
    {
        yield return new WaitForSeconds(0.2f);

        uiMain.SetActive(true);
        uiOption.SetActive(false);
    }
    #endregion
}
