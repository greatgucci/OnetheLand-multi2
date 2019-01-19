using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour {

    public static CamManager instance;

    Camera cam1;//Player1찍고있는 Cam(왼)
    Camera cam2;//Player2찍고있는 Cam(오)

	void Awake ()
    {
        cam1 = transform.Find("Cam1").GetComponent<Camera>();
        cam2 = transform.Find("Cam2").GetComponent<Camera>();
        instance = this;
	}

    public void SetCam(int pNum)
    {
        if(pNum ==1)
        {
            cam1.rect = new Rect(0, 0.5f, 1, 1);
            cam2.rect = new Rect(0, -0.5f, 1, 1);
        }
        else if(pNum == 2)
        {
            cam1.rect = new Rect(0, -0.5f, 1, 1);
            cam2.rect = new Rect(0, 0.5f, 1, 1);
        }
    }
}
