using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class NetworkManager : Photon.PunBehaviour {

    public static NetworkManager instance;
    private void Awake()
    {
        instance = this;
    }
    public GameObject[] playerPrefabs;
    public Text ping;
    public Text timeText;
    private bool isP1Ready;
    private bool isP2Ready;
    public Text tempGameStartText;
    public Text tempGameOverText;
    private void Start()
    {

        PlayerManager.instance.playMode = PlayMode.ONLINE;

        PlayerInsantiate(PlayerPrefs.GetInt("Character"));

        if (PhotonNetwork.isMasterClient)
        {
            StartCoroutine(GameStartRoutine());
        }
    }

    #region Photon Messages
    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        PhotonNetwork.SetMasterClient(PhotonNetwork.player);
        PhotonNetwork.LoadLevel(1);
    }
    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
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
    public void GameOver(int loser)
    {
        photonView.RPC("GameOver_RPC", PhotonTargets.All, loser);
    }
    public void PlayerReady(int pNum)
    {
        photonView.RPC("PlayerReady_RPC", PhotonTargets.All,pNum);
    }
    #endregion

    private void Update()
    {
        ping.text = "Ping : " + PhotonNetwork.GetPing();
    }


    #region PUNRPC
    [PunRPC]
    private void PlayerReady_RPC(int pNum)
    {
        if(pNum == 1)
        {
            isP1Ready = true;
        }else if(pNum==2)
        {
            isP2Ready = true;
        }
    }
    [PunRPC]
    private void GameOver_RPC(int loser)
    {
        UIManager.instance.StopTime();
        if(PhotonNetwork.isMasterClient)
        {
            StartCoroutine(GameOverRoutine(loser));
        }
    }
    [PunRPC]
    private void StartTimeCount()
    {
        UIManager.instance.StartTime();
    }

    [PunRPC]
    private void GameStartEffect()
    {        
        UIManager.instance.SetPortrait(PlayerManager.instance.GetPlayerByNum(1).character,PlayerManager.instance.GetPlayerByNum(2).character);
    }
    [PunRPC]
    private void GameStartEffectOff()
    {
    }
    [PunRPC]    
    private void GameOverEffect(int loser)
    {

    }
    [PunRPC]
    private void GoToWaiting()
    {
        PhotonNetwork.LoadLevel(1);
    }
    [PunRPC]
    private void GameUpdate_RPC(GameUpdate update)
    {
        PlayerManager.instance.gameUpdate = update;
    }
    #endregion

    /// <summary>
    /// 방장 쪽에서 루틴돌림
    /// </summary>
    /// <returns></returns>
    IEnumerator GameStartRoutine()
    {
        while(true)
        {
            if(isP1Ready&&isP2Ready)
            {
                break;
            }
            yield return null;
        }
        yield return null;
        photonView.RPC("GameStartEffect", PhotonTargets.AllViaServer);
        yield return new WaitForSeconds(2.5f);
        photonView.RPC("GameStartEffectOff", PhotonTargets.AllViaServer);
        photonView.RPC("GameUpdate_RPC", PhotonTargets.AllViaServer, GameUpdate.GAMING);
        photonView.RPC("StartTimeCount", PhotonTargets.AllViaServer);
    }

    /// <summary>
    /// 방장 쪽에서 루틴 돌림 인풋,연출 동기화
    /// </summary>
    IEnumerator GameOverRoutine(int loser)
    {
        photonView.RPC("GameUpdate_RPC", PhotonTargets.AllViaServer, GameUpdate.END);
        photonView.RPC("GameOverEffect", PhotonTargets.AllViaServer, loser); 
        yield return new WaitForSeconds(3f);
        LeaveRoom();
    }


}
