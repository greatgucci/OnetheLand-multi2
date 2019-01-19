using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStick : MonoBehaviour,IDragHandler,IPointerUpHandler,IPointerDownHandler
{

    private Image bgImg;
    private Image joystickImg;
    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }
    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos = new Vector2(pos.x / bgImg.rectTransform.sizeDelta.x, pos.y / bgImg.rectTransform.sizeDelta.y);
            pos = pos * 2;
            pos = (pos.magnitude > 1.0f) ? pos.normalized : pos;

            //move joystick img
            joystickImg.rectTransform.anchoredPosition =
                new Vector3(pos.x * (bgImg.rectTransform.sizeDelta.x / 3), pos.y * (bgImg.rectTransform.sizeDelta.y / 3));

            InputSystem.instance.joyStickVector = pos;
            
            
      }
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }
    public virtual void OnPointerUp(PointerEventData ped)
    {
        InputSystem.instance.joyStickVector = Vector2.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;
    }

}
