using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillCooltimeUIDisplay : MonoBehaviour {

    Image myImage;

    private void Awake()
    {
        myImage = GetComponent<Image>();
    }

    public void SetCoolDisplay(float coolTime)
    {
        StartCoroutine(CoolDisplay(coolTime));
    }

    IEnumerator CoolDisplay(float coolTIme)
    {
        float currentCoolPercent = 1f;

        while(true)
        {
            if (currentCoolPercent <= 0f)
            {
                break;
            }

            myImage.fillAmount = currentCoolPercent;

            currentCoolPercent -= Time.deltaTime * (1f / coolTIme);
            yield return null;
        }

        currentCoolPercent = 0f;
        myImage.fillAmount = currentCoolPercent;
    }
}
