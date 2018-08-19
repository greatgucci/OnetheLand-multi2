using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetContoller : PlayerControl {

    PlayerData playerData;
    private void Awake()
    {
        playerData = transform.Find("PlayerData").GetComponent<PlayerData>();
    }
    // Use this 

    // Use this for initialization
    void Start ()
    {
        playerData.SetPlayerNum(2,this);
        SetPlayerPos(playerData.playerNum);
    }
    protected override void LateUpdate()
    {
        return;
    }
}
