using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetContoller : PlayerControl {

    private new void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        playerData = transform.Find("PlayerData").GetComponent<PlayerData>();
    }
    // Use this 

    // Use this for initialization
    protected override void StartCall()
    {
        pNum = 2;
        GameManager.instance.Opponent = playerData;
        playerData.SetPlayerNum(2, this);
        playerData.SetStatus(0, 100, 0);
        SetStartPosition(playerData.playerNum);
    }
    protected override void LateUpdate()
    {
        return;
    }
}
