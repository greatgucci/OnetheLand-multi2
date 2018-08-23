using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetContoller : PlayerControl {

    private new void Awake()
    {
        playerData = transform.Find("PlayerData").GetComponent<PlayerData>();
    }
    // Use this 

    // Use this for initialization
    new void Start ()
    {
        playerData.SetPlayerNum(2,this);
        SetPlayerPos(playerData.playerNum);
    }
    protected override bool LateUpdate()
    {
		return false;
    }
}
