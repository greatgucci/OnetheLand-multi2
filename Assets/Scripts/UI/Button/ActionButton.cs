using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public enum ButtonNum
{
    FIRST,
    SECONDE,
    THIRD
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
        }
        button.color = normal;
    }
	
}
