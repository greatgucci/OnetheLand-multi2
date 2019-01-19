using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum NetWorkMode
{
    ONLINE,
    OFFLINE
}
public class photonScript : Photon.PunBehaviour
{
    public int SelectedCharIndex;
    public Text debug;
    public string versionName;
    public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

    [Header("Lan Setting")]
    public string LanIp;
    public int LanPort;
    public string AppID;
    public NetWorkMode netWorkMode;



    private void Awake()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
        PhotonNetwork.logLevel = Loglevel;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.AuthValues = new AuthenticationValues(Random.Range(int.MinValue, int.MaxValue).ToString());

        PlayerPrefs.SetInt("Character", 0);
    }

    #region CallBacks
    public override void OnConnectionFail(DisconnectCause cause)
    {
        base.OnConnectionFail(cause);
        PhotonNetwork.AuthValues = new AuthenticationValues(Random.Range(int.MinValue, int.MaxValue).ToString());
    }
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        if(netWorkMode == NetWorkMode.OFFLINE)
        {
            PhotonNetwork.CreateRoom("OffLine", new RoomOptions() { MaxPlayers = 2, PublishUserId = false }, null);
            PhotonNetwork.LoadLevel("Ingame");
        }
        else if (netWorkMode == NetWorkMode.ONLINE)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2, PublishUserId = false }, null);
    }
    public override void OnJoinedRoom()
    {
        debug.text = "다른 플레이어를 기다리고 있어요.";       
    }
    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        if (PhotonNetwork.room.PlayerCount >= 2)
        {
            PhotonNetwork.LoadLevel("Ingame");
        }
    }

    #endregion
    #region ButtonActions
    public void SetNetworkMode(int i)
    {
        switch (i)
        {
            case 0:
                netWorkMode = NetWorkMode.ONLINE;
                break;
            case 1:
                netWorkMode = NetWorkMode.OFFLINE;
                break;
        }
    }
    public void SelectCharacter(int i)
    {
        PlayerPrefs.SetInt("Character", i);
    }
    /// <summary>
    /// 버튼 눌렀을때 호출
    /// </summary>
    public void ConnectCallBack()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }

        if (netWorkMode == NetWorkMode.ONLINE)
        {
            PhotonNetwork.PhotonServerSettings.HostType = ServerSettings.HostingOption.PhotonCloud;
            PhotonNetwork.PhotonServerSettings.PreferredRegion = CloudRegionCode.kr;
            PhotonNetwork.PhotonServerSettings.Protocol = ExitGames.Client.Photon.ConnectionProtocol.Udp;
            PhotonNetwork.PhotonServerSettings.AppID = AppID;
            PhotonNetwork.ConnectUsingSettings(versionName);
            debug.text = "서버에 접속중..";
        }
        else if(netWorkMode == NetWorkMode.OFFLINE)
        {
            PhotonNetwork.PhotonServerSettings.HostType = ServerSettings.HostingOption.OfflineMode;
            PhotonNetwork.offlineMode = true;       
        }
    }
    #endregion
    IEnumerator TimeCount()
    {
        yield return new WaitForSeconds(8f);
        debug.text = "서버 접속에 실패했습니다..";
    }
}
