using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum NetWorkMode
{
    CLOUD,
    LAN
}
public class photonScript : Photon.PunBehaviour
{
    public int SelectedCharIndex;
    public Text debug;
    public string versionName = "0.1";
    public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

    [Header("Lan Setting")]
    public string LanIp;
    public int LanPort;
    public string AppID;
    public NetWorkMode netWorkMode;


    private bool isConnecting;

    private void Awake()
    {
        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 30;
        PhotonNetwork.logLevel = Loglevel;
        PhotonNetwork.autoJoinLobby = false;
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.AuthValues = new AuthenticationValues(Random.Range(int.MinValue, int.MaxValue).ToString());

        PlayerPrefs.SetInt("Character", 0);
        DataManager.Initalization();
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
        if (isConnecting)
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
        if (PhotonNetwork.isMasterClient)
        {
            debug.text = "Debug : Joined Room";
            SceneManager.LoadScene("WaitingScene");
        }
    }

    #endregion
    #region ButtonActions
    public void SetNetworkMode(int i)
    {
        switch (i)
        {
            case 0:
                netWorkMode = NetWorkMode.CLOUD;
                break;
            case 1:
                netWorkMode = NetWorkMode.LAN;
                break;
        }
    }
    public void SelectCharacter(int i)
    {
        PlayerPrefs.SetInt("Character", i);
    }
    public void Connect()
    {
        connecting = StartCoroutine(connectToPhoton());
    }
    #endregion
    #region private
    Coroutine connecting;
    IEnumerator connectToPhoton()
    {
        isConnecting = true;
        PhotonNetwork.Disconnect();
        yield return new WaitForSeconds(0.1f);
        if (netWorkMode == NetWorkMode.CLOUD)
        {
            PhotonNetwork.PhotonServerSettings.HostType = ServerSettings.HostingOption.PhotonCloud;
            PhotonNetwork.PhotonServerSettings.PreferredRegion = CloudRegionCode.kr;
            PhotonNetwork.PhotonServerSettings.Protocol = ExitGames.Client.Photon.ConnectionProtocol.Udp;
            PhotonNetwork.PhotonServerSettings.AppID = AppID;

            PhotonNetwork.ConnectUsingSettings(versionName); //클라우드 접속
            debug.text = "Connecting To Photon Cloud";

        }
        else if (netWorkMode == NetWorkMode.LAN)
        {
            PhotonNetwork.PhotonServerSettings.HostType = ServerSettings.HostingOption.SelfHosted;
            PhotonNetwork.PhotonServerSettings.ServerAddress = LanIp;
            PhotonNetwork.PhotonServerSettings.ServerPort = LanPort;
            PhotonNetwork.PhotonServerSettings.Protocol = ExitGames.Client.Photon.ConnectionProtocol.Udp;
            PhotonNetwork.PhotonServerSettings.AppID = AppID;


            PhotonNetwork.ConnectUsingSettings(versionName); // 랜접속
            debug.text = "Connetting To Lan";
        }
    }
    #endregion
    IEnumerator TimeCount()
    {
        yield return new WaitForSeconds(8f);
        debug.text = "Server not connected Try Another NetworkMode";
    }
}
