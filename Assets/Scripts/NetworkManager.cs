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
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    public GameObject[] playerPrefabs;
    public Text ping;
    private bool isP1Ready;
    private bool isP2Ready;

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
        UIManager.instance.StartTimer();
    }

    [PunRPC]
    private void GameStartEffect()
    {
        StartEffectRoutine = StartCoroutine(StartEffect());
    }
    [PunRPC]    
    private void GameOverEffect(int loser)
    {
        EndEffectRoutine = StartCoroutine(EndEffect(loser));
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
    [PunRPC]
    private void SetRandomSeed(int ran)
    {
        Random.InitState(ran);
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
        photonView.RPC("SetRandomSeed", PhotonTargets.All, Random.Range(0, int.MaxValue));
        photonView.RPC("GameStartEffect", PhotonTargets.AllViaServer);
        while(StartEffectRoutine ==null)
        {
            yield return null;
        }
        yield return StartEffectRoutine;
        yield return new WaitForSeconds(0.1f);//wait
        photonView.RPC("StartTimeCount", PhotonTargets.AllViaServer);
        photonView.RPC("GameUpdate_RPC", PhotonTargets.AllViaServer, GameUpdate.GAMING);
    }

    /// <summary>
    /// 방장 쪽에서 루틴 돌림 인풋,연출 동기화
    /// </summary>
    IEnumerator GameOverRoutine(int loser)
    {
        photonView.RPC("GameUpdate_RPC", PhotonTargets.AllViaServer, GameUpdate.END);
        photonView.RPC("GameOverEffect", PhotonTargets.AllViaServer, loser);
        while (EndEffectRoutine == null)
        {
            yield return null;
        }
        yield return EndEffectRoutine;
        yield return new WaitForSeconds(0.1f);
        LeaveRoom();
    }
    Coroutine StartEffectRoutine;
    IEnumerator StartEffect()
    {
        yield return null;
        UIManager.instance.SetPortrait(PlayerManager.instance.GetPlayerByNum(1).character, PlayerManager.instance.GetPlayerByNum(2).character);
        UIManager.instance.CharacterStartOn(PlayerManager.instance.GetPlayerByNum(1).character, PlayerManager.instance.GetPlayerByNum(2).character);
        UIManager.instance.SetCharacterStart(1, true);
        UIManager.instance.SetCharacterStart(2, false);
        PlayCharacterStartSound(PlayerManager.instance.GetPlayerByNum(1).character);
        yield return new WaitForSeconds(characterVoice.clip.length + 0.5f);
        UIManager.instance.SetCharacterStart(1, false);
        UIManager.instance.SetCharacterStart(2, true);
        PlayCharacterStartSound(PlayerManager.instance.GetPlayerByNum(2).character);
        yield return new WaitForSeconds(characterVoice.clip.length + 0.5f);
        UIManager.instance.CharacterStartOff();
        yield return new WaitForSeconds(0.5f);

        PlayStartSound();
        UIManager.instance.StartEventTimerOn();//START!
        yield return new WaitForSeconds(1f);
        UIManager.instance.StartEventTimerOff();
    }
    Coroutine EndEffectRoutine;
    IEnumerator EndEffect(int loser)
    {
        int winner = 0;
        if(loser ==1)
        {
            winner = 2;
        }else
        {
            winner = 1;
        }
        PlayerManager.instance.GetPlayerByNum(loser).PlayLoseAnime();
        PlayerManager.instance.GetPlayerByNum(winner).PlayWinAnime();
        yield return new WaitForSeconds(1f);
        UIManager.instance.WinnerCharacterOn(winner);
        UIManager.instance.SetCharacterStart(winner, true);
        PlayCharacterWinSound(PlayerManager.instance.GetPlayerByNum(winner).character);
        yield return new WaitForSeconds(3.5f);
        UIManager.instance.PlayEndBlackAnimation();
        yield return new WaitForSeconds(4f);
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
