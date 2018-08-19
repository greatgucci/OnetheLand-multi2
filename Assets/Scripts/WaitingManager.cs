using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class WaitingManager : Photon.PunBehaviour
{
    public static WaitingManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] playerPrefabs;
    public Text ping;

    private void Start()
    {

        PlayerManager.instance.playMode = PlayMode.WAITING;


        if (PhotonNetwork.isMasterClient)
        {
            PlayerInsantiate(PlayerPrefs.GetInt("Character"));
            
        }
        MakePuppet();
        PlayerManager.instance.gameUpdate = GameUpdate.GAMING;
    }
    #region Photon Messages



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
        PhotonNetwork.Instantiate(this.playerPrefabs[i].name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0).GetComponent<PlayerControl>();
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
    private void MakePuppet()
    {
        PlayerManager.instance.Opponent = Instantiate(Resources.Load("Puppet") as GameObject).GetComponentInChildren<PlayerData>();
    }
}
