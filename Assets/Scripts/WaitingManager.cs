﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WaitingManager : Photon.PunBehaviour
{
    PlayerControl self;
    public static WaitingManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] playerPrefabs;
    public Text ping;


    #region Photon Messages

    private void Start()
    {

        PlayerManager.instance.playMode = PlayMode.WAITING;
        

        if(PhotonNetwork.isMasterClient)
        {
            PlayerInsantiate(PlayerPrefs.GetInt("Character"));
        }
        InputSetOk(true);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
 
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if(PhotonNetwork.room.PlayerCount>=2)
        {
            PhotonNetwork.LoadLevel("OnlineScene");
        }
    }


    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void PlayerInsantiate(int i)
    {
        self = PhotonNetwork.Instantiate(this.playerPrefabs[i].name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0).GetComponent<PlayerControl>();
        if (PhotonNetwork.isMasterClient)
        {
            PlayerManager.instance.myPnum = 1;
        }
        else
        {
            PlayerManager.instance.myPnum = 2;
        }
    }
    #endregion

    private void Update()
    {
        ping.text = "Ping : " + PhotonNetwork.GetPing();
    }
    private void InputSetOk(bool b)
    {
        if(self != null)
        {
            self.IsInputAble = b;
        }
    }
}
