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
    private PlayerControl self;

    public Text tempGameStartText;
    public Text tempGameOverText;
    #region Photon Messages

    private void Start()
    {

        PlayerManager.instance.playMode = PlayMode.ONLINE;
        
        PlayerInsantiate(PlayerPrefs.GetInt("Character"));

        if(photonView.isMine)
        {
            StartCoroutine(GameStartRoutine());
        }
    }
 
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
    public void GameOver(int loser)
    {
        StartCoroutine(GameOverRoutine(loser));
    }
    #endregion

    private void Update()
    {
        ping.text = "Ping : " + PhotonNetwork.GetPing();
    }

    #region PUNRPC
    [PunRPC]
    private void StartTimeCount()
    {
        StartCoroutine(TimeCount());
    }

    [PunRPC]
    private void GameStartEffect()
    {
        tempGameStartText.rectTransform.anchoredPosition = new Vector3(0, 0, 0);

        if (PlayerManager.instance.myPnum == 1)
        {
            tempGameStartText.text = "YOUR PLAYER 1 \n <<<<<<<<<";
        }else if(PlayerManager.instance.myPnum == 2)
        {
            tempGameStartText.text = "YOUR PLAYER 2 \n >>>>>>>>>";
        }
    }
    [PunRPC]
    private void GameStartEffectOff()
    {
        tempGameStartText.rectTransform.anchoredPosition = new Vector3(0, 2000, 0);
    }
    [PunRPC]    
    private void GameOverEffect(int loser)
    {
        tempGameOverText.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        if(PlayerManager.instance.Local.playerNum == loser)
        {
            tempGameOverText.text = "넌 졌어";
        }else
        {
            tempGameOverText.text = "당신은 승리";
        }
    }
    [PunRPC]
    private void GoToWaiting()
    {
        PhotonNetwork.LoadLevel(1);
    }
    #endregion
    /// <summary>
    /// 로컬 플레이어의 인풋을 설정합니다.
    /// </summary>
    [PunRPC]
    private void InputSetOk(bool b)
    {
       self.IsInputAble = b;
    }

    /// <summary>
    /// 방장 쪽에서 루틴돌림
    /// </summary>
    /// <returns></returns>
    IEnumerator GameStartRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        photonView.RPC("GameStartEffect", PhotonTargets.AllViaServer);
        yield return new WaitForSeconds(2.9f);
        photonView.RPC("GameStartEffectOff", PhotonTargets.AllViaServer);
        photonView.RPC("InputSetOk", PhotonTargets.AllViaServer, true);
        photonView.RPC("StartTimeCount", PhotonTargets.AllViaServer);
    }

    /// <summary>
    /// 패배자 쪽에서 루틴 돌림 인풋,연출 동기화
    /// </summary>
    IEnumerator GameOverRoutine(int loser)
    {
        PlayerManager.instance.GetPlayerControlByNum(1).IsColEnable(false);
        PlayerManager.instance.GetPlayerControlByNum(2).IsColEnable(false);

        photonView.RPC("InputSetOk", PhotonTargets.AllViaServer, false);
        photonView.RPC("GameOverEffect", PhotonTargets.AllViaServer, loser); 
        yield return new WaitForSeconds(3f);
        LeaveRoom();
    }

    /// <summary>
    /// 시작 지점만 동기화
    /// </summary>
    /// <returns></returns>
    IEnumerator TimeCount()
    {
         float t =99;
        while(true)
        {
            t -= Time.deltaTime;
            timeText.text = "" + (int)t;
            yield return null;
        }
    }
}
