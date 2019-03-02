using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum ButtonNum
{
    FIRST,
    SECONDE,
    THIRD,
    FOURTH,
    FIFTH,
    SIXTH
}

public class ActionButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    Image button;
    private void Awake()
    {
        button = GetComponent<Image>();
    }

    public ButtonNum num = ButtonNum.FIRST;
    public Color normal;
    public Color pressed;

    public virtual void OnPointerDown(PointerEventData ped)
    {
        switch (num)
        {
            case ButtonNum.FIRST:
                InputSystem.instance.button1Pressed = true;
                break;
            case ButtonNum.SECONDE:
                InputSystem.instance.button2Pressed = true;
                break;
            case ButtonNum.THIRD:
                InputSystem.instance.button3Pressed = true;
                break;
            case ButtonNum.FOURTH:
                InputSystem.instance.button4Pressed = true;
                break;
            case ButtonNum.FIFTH:
                InputSystem.instance.button5Pressed = true;
                break;
            case ButtonNum.SIXTH:
                InputSystem.instance.button6Pressed = true;
                break;
        }
        button.color = pressed;
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        switch (num)
        {
            case ButtonNum.FIRST:
                InputSystem.instance.button1Pressed = false;
                break;
            case ButtonNum.SECONDE:
                InputSystem.instance.button2Pressed = false;
                break;
            case ButtonNum.THIRD:
                InputSystem.instance.button3Pressed = false;
                break;
            case ButtonNum.FOURTH:
                InputSystem.instance.button4Pressed = false;
                break;
            case ButtonNum.FIFTH:
                InputSystem.instance.button5Pressed = false;
                break;
            case ButtonNum.SIXTH:
                InputSystem.instance.button6Pressed = false;
                break;
        }
        button.color = normal;
    }
	
}
