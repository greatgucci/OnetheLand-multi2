using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour {

    public static InputSystem instance;

    public Vector2 joyStickVector;

    public bool button1Pressed;
    public bool button2Pressed;
    public bool button3Pressed;
    public bool button4Pressed;
    public bool button5Pressed;
    public bool button6Pressed;

    public bool useKeyBoard;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
    #if UNITY_STANDALONE_WIN //PC조작
        if (useKeyBoard)
        {
            joyStickVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            button1Pressed = Input.GetKey(KeyCode.J);
            button2Pressed = Input.GetKey(KeyCode.K);
            button3Pressed = Input.GetKey(KeyCode.L);
            button4Pressed = Input.GetKey(KeyCode.N);
            button5Pressed = Input.GetKey(KeyCode.M);
            button6Pressed = Input.GetKey(KeyCode.Comma);
        }
        #endif
    }
}
