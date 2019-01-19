using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {

    public static InputSystem instance;

    public float joyStickX;
    public float joyStickY;

    public bool joyStickPressed;
    public bool button1Pressed;
    public bool button2Pressed;
    public bool button3Pressed;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        #if UNITY_EDITOR //PC조작
        
        joyStickX = Input.GetAxisRaw("Horizontal");
        joyStickY = Input.GetAxisRaw("Vertical");
        if(Mathf.Abs(joyStickX)>0 || Mathf.Abs(joyStickY)>0)
        {
            joyStickPressed = true;
        }
        else
        {
            joyStickPressed = false;
        }
           
        button1Pressed = Input.GetKey(KeyCode.J);
        button2Pressed = Input.GetKey(KeyCode.K);
        button3Pressed = Input.GetKey(KeyCode.L);
        #endif
    }
}
