using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppetContoller : MonoBehaviour {

    PlayerData playerData;
    private void Awake()
    {
        playerData = transform.Find("PlayerData").GetComponent<PlayerData>();
    }
    // Use this 

    // Use this for initialization
    void Start ()
    {
        playerData.SetPlayerNum(2);
        SetPlayerPos(playerData.playerNum);
    }

    protected virtual void SetPlayerPos(int pnum)
    {
        if (pnum == 1)
        {
            transform.position = new Vector3(-4, 0, 0);
        }
        else if (pnum == 2)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = new Vector3(4, 0, 0);
        }
    }
}
