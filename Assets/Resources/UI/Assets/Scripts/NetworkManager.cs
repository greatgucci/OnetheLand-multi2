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
        characterVoice = GetComponent<AudioSource>();
    }

    public GameObject[] playerPrefabs;
    public Text ping;
    private bool isP1Ready;
    private bool isP2Ready;

    private void Start()
    {
        if(PhotonNetwork.offlineMode)
        {
            GameManager.instance.networkMode = NetworkMode.OFFLINE;
        }else
        {
            GameManager.instance.networkMode = NetworkMode.ONLINE;
        }

        if (PhotonNetwork.isMasterClient)
        {
            GameManager.instance.myPnum = 1;
        }
        else
        {
            GameManager.instance.myPnum = 2;
        }
        CamManager.instance.SetCam(GameManager.instance.myPnum);

        PlayerInsantiate(PlayerPrefs.GetInt("Character"));

        StartCoroutine(PingUpdate());

        if (PhotonNetwork.isMasterClient)
        {
            StartCoroutine(StartStage());
        }

    }

    #region Photon Messages
    public override void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        LeaveRoom();
    }
    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    public override void OnLeftRoom()
    {      
        SceneManager.LoadScene("Title");
    }
    #endregion

    #region Public Methods

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    /// <summary>
    /// 자신의 캐릭터를 생성
    /// </summary>
    public void PlayerInsantiate(int i)
    {
        PhotonNetwork.Instantiate(this.playerPrefabs[i].name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);

        if(PhotonNetwork.offlineMode)
        {
            PhotonNetwork.Instantiate("Puppet", new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
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

    IEnumerator PingUpdate()
    {
        while(true)
        {
            ping.text = "Ping : " + PhotonNetwork.GetPing();
            yield return new WaitForSeconds(0.3f);
        }
    }


    #region PUNRPC
    /// <summary>
    /// 각 플레이어의 Player Object들이 생성되고 GameManager에 등록이 끝남
    /// </summary>
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
    private void GameOver_RPC(int winner)
    {
        Debug.Log("Player" + GameManager.instance.GetPlayerByNum(winner) +"has Win");
        StartCoroutine(GameOverStage(winner));
    }
    [PunRPC]
    private void StartTimeCount()
    {
        UIManager.instance.StartTimer();
    }

    [PunRPC]
    private void GoToWaiting()
    {
        PhotonNetwork.LoadLevel(1);
    }
    [PunRPC]
    private void GameUpdate_RPC(GameUpdate update)
    {
        GameManager.instance.gameUpdate = update;
    }
    [PunRPC]
    private void SetRandomSeed(int ran)
    {
        Random.InitState(ran);
    }
    #endregion
    /// <summary>
    /// 게임 시작 연출 방장 쪽에서 루틴돌림
    /// </summary>
    /// <returns></returns>
    IEnumerator StartStage()
    {
        if (GameManager.instance.networkMode == NetworkMode.ONLINE)
        {
            while (true)
            {
                if (isP1Ready && isP2Ready)
                {
                    break;
                }
                yield return null;
            }
        }//플레이어 기다리기
        yield return null;
        photonView.RPC("SetRandomSeed", PhotonTargets.All, Random.Range(0, int.MaxValue));
        yield return new WaitForSeconds(0.1f);//wait
        photonView.RPC("StartTimeCount", PhotonTargets.AllViaServer);
        photonView.RPC("GameUpdate_RPC", PhotonTargets.AllViaServer, GameUpdate.INGAME);
        PlayStartSound();
    }

    IEnumerator GameOverStage(int winner)
    {
        PlayCharacterWinSound(GameManager.instance.GetPlayerByNum(winner).character);
        UIManager.instance.PlayEndBlackAnimation();
        yield return new WaitForSeconds(3f);
        LeaveRoom();
    }

    private AudioSource characterVoice;
    public MultiSound dianaStart;
    public MultiSound irisStart;
    public MultiSound dianaWin;
    public MultiSound irisWin;
    public MultiSound GameStart;

    private void PlayCharacterStartSound(Character cha)
    {
        characterVoice.Stop();
        switch(cha)
        {
            case Character.DIANA:
                characterVoice.clip = dianaStart.RandomSound;
                break;
            case Character.IRIS:
                characterVoice.clip = irisStart.RandomSound;
                break;
        }
        characterVoice.Play();
    }

    private void PlayCharacterWinSound(Character cha)
    {
        characterVoice.Stop();
        switch (cha)
        {
            case Character.DIANA:
                characterVoice.clip = dianaWin.RandomSound;
                break;
            case Character.IRIS:
                characterVoice.clip = irisWin.RandomSound;
                break;
        }
        characterVoice.Play();
    }
    private void PlayStartSound()
    {
        characterVoice.Stop();
        characterVoice.clip = GameStart.RandomSound;
        characterVoice.Play();
    }
}
