using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : Photon.PunBehaviour {

    Vector3 temp;
    public Vector3 aimVector_Temp;
    public Vector3 aimPosition_Temp;
    public PlayerData myPD;


	// Use this for initialization
	void Start () {
        myPD = PlayerManager.instance.Local;

    }
	
	// Update is called once per frame
	void Update () {

        temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(temp.x, temp.y);
        aimVector_Temp = transform.position - myPD.transform.position;
        aimVector_Temp.Normalize();
        aimPosition_Temp = transform.position;
        
    }
}
