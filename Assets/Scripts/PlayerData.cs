using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 규칙 : 로컬의 캐릭터 데이터는 로컬에서만 수정하도록 한다.
/// Hp(동기화됨),스킬게이지(동기화됨) 관리하는 스크립트
/// 네트워크 상에서는 플레이어에 관해 이것으로 접근한다.
/// </summary>
public class PlayerData : Photon.PunBehaviour, IPunObservable
{
    #region Variables
    public int playerNum;
    private float currentHp;
    private float fullHp;
    private float currentSkillGage;
    private float fullSkillGage;
    private float isFlip; // 1일 경우 오른쪽, -1일 경우 왼쪽을 보고 있는 거임
    private float globalCool = 0.5f;
    private float defense;
    private PlayerControl playerControl;
    public float[] cooltime = new float[10];
    public float cooltimeSpd = 1f;
    Aim Aim_Object;
    PlayerData opponent;
    protected AudioSource voice2;
    public MultiSound hitSource;

    public Vector3 aimVector = new Vector3(0f,0f,0f);
    public Vector3 aimPosition = new Vector3(0f, 0f, 0f);
    public Character character;
    public float Defense
    {
        get { return defense; }
        set { defense = value; }
    }
    public float CurrentHp
    { get { return currentHp; }
        set
        {
            if(PlayerManager.instance.gameUpdate == GameUpdate.END) //게임 종료시에는 체력 변화 없음
            {
                return;
            }
            if(photonView.isMine)
            {
                if(currentHp>value)
                {
                    //PlayHitSound();//내가 맞는소리는 로컬에서만
                }
                currentHp = value*(1-defense);
                UpdateHpUI(currentHp);
                if (currentHp <= 0)
                {
                    NetworkManager.instance.GameOver(playerNum);
                }
            }
            else { Debug.Log("주인이 아닌 캐릭터에서 수정에 접근했습니다."); }      
        }
    }
    public float FullHp
    { get { return fullHp; }
      set
        {
            if(photonView.isMine)
            fullHp = value;
        }
    }
    public float CurrentSkillGage
    { get { return currentSkillGage; }
        set
        {
            if (photonView.isMine)
            {
                if(value>fullSkillGage)
                {
                    currentSkillGage = fullSkillGage;
                }else
                {
                    currentSkillGage = value;
                }
                UpdateSkgUI(currentSkillGage);
            }
            else { Debug.Log("주인이 아닌 캐릭터에서 수정에 접근했습니다."); }
        }
    }
    public float FullSkillGage
    { get { return fullSkillGage; }
        set
        {
            if (photonView.isMine)
                fullSkillGage = value;
        }
    }

    public void SetStatus(float _fullHp,float _fullSkg,float _hp,float _skg)
    {
        if(photonView.isMine)
        {
            FullHp = _fullHp;
            FullSkillGage = _fullSkg;
            CurrentHp = _hp;
            CurrentSkillGage = _skg;

            for(int i = 0; i < cooltime.Length; i++)
            {
                cooltime[i] = 0f;
            }

            opponent = PlayerManager.instance.Opponent;
        }
        else
        {
            fullHp = _fullHp;
            fullSkillGage = _fullSkg;
            currentHp = _hp;
            currentSkillGage = _skg;
            UpdateHpUI(currentHp);
            UpdateSkgUI(currentSkillGage);
        }
    }

    public void SetCooltime(int skillNum, float _cooltime)
    {
        for (int i = 0; i < cooltime.Length; i++)
        {

            if (i == skillNum)
            {
                cooltime[i] = _cooltime;
                SkillCooltimeUI.SetCoolTimeUI(i, cooltime[i]);
            }
            else
            {
                if (cooltime[i] <= globalCool)
                {
                    cooltime[i] += globalCool;
                    SkillCooltimeUI.SetCoolTimeUI(i, cooltime[i]);
                }
            }
        }
    }
    #endregion

    void Awake()
    {
        voice2 = GetComponent<AudioSource>();
    }
    private void Start()
    {
        if (photonView.isMine)
        {
            Aim_Object = Instantiate(Resources.Load("Aim") as GameObject, Input.mousePosition, Quaternion.identity)
        .GetComponent<Aim>();
        }
    }
    public int HitSoundRan = 20;
    private void PlayHitSound()
    {
        int ran = Random.Range(0, 100);
        if(ran<=HitSoundRan)
        {
            voice2.clip = hitSource.RandomSound;
            voice2.Play();
        }
    }
    private void Update()
    {
        if(photonView.isMine)
        {
            CurrentSkillGage += Time.deltaTime * 1f;

            for (int i = 0; i < cooltime.Length; i++)
            {
                if (cooltime[i] > 0f)
                {
                    cooltime[i] -= Time.deltaTime * cooltimeSpd;
                }
            }

            if ((transform.position.x - Aim_Object.aimVector_Temp.x) * (transform.position.x - aimVector.x) < 0)
            {
                Debug.Log(transform.position.x - Aim_Object.aimVector_Temp.x);
                Debug.Log("Flip");
            }

            aimVector = Aim_Object.aimVector_Temp;
            aimPosition = Aim_Object.aimPosition_Temp;

            if (CurrentHp <= 1000f)
            {
                //CurrentHp += Time.deltaTime * 10f;
            }
        }
    }

    /// <summary>
    /// Local아닐경우 이것으로 UI업데이트
    /// </summary>
    private void UpdateHpUI(float hp)
    {
     if(PlayerManager.instance.playMode == PlayMode.ONLINE)
        UIManager.instance.SetHp(playerNum, currentHp/fullHp);
    }
    private void UpdateSkgUI(float skg)
    {
        if (PlayerManager.instance.playMode == PlayMode.ONLINE)
        {
            UIManager.instance.SetSkg(playerNum, currentSkillGage/fullSkillGage);
        }
    }
    #region Public
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)//내가 이 캐릭터의 주인일때 stream에 write
        {
            stream.SendNext(currentHp);
            stream.SendNext(currentSkillGage);
        }
        else//주인이 아니면 받고 UI에 적용
        {
            currentHp = (float)stream.ReceiveNext();
            currentSkillGage = (float)stream.ReceiveNext();
            UpdateHpUI(currentHp);
            UpdateSkgUI(currentSkillGage);
        }
    }

    public void SetPlayerNum(int pnum,PlayerControl pc)
    {
        playerControl = pc;
        playerNum = pnum;
        transform.parent.gameObject.tag = "Player" + pnum;
        transform.parent.gameObject.name = gameObject.name + "_P" + pnum;
    }
    #endregion
    /// <summary>
    /// 플레이어의 콜라이더 설정,한 클라에서만 호출 하면 자동으로 연동함 , 수동으로 조절
    /// </summary>
    /// <param name="b"></param>
    public void SetColliderEnable(bool b)
    {
        playerControl.photonView.RPC("IsColEnable_RPC", PhotonTargets.All, b);
    }
    /// <summary>
    /// 한클라에서만 호출하면 자동으로 연동함 , 자동으로 1초후 풀림
    /// </summary>
    public void GetStun(float t=1f)
    {
        playerControl.photonView.RPC("GetStun_RPC", PhotonTargets.All,t);
    }
	public void GetSilence(float t=1f)
	{
		playerControl.photonView.RPC("GetSilence_RPC", PhotonTargets.All,t);
	}
    public void GetFetter(float t = 1f)
    {
        playerControl.photonView.RPC("GetFetter_RPC", PhotonTargets.All, t);
    }

    /// <summary>
    /// 한클라에서만 호출하면 자동으로 연동함 , 수동으로 조절
    /// </summary>
    public void SetInputEnable(bool b)
    {
        playerControl.photonView.RPC("SetInputEnable_RPC", PhotonTargets.All,b);
    }

    public void PlayWinAnime()
    {
        photonView.RPC("PlayWinAnime_RPC", PhotonTargets.All);
    }
    public void PlayLoseAnime()
    {
        photonView.RPC("PlayLoseAnime_RPC", PhotonTargets.All);
    }

    [PunRPC]
    private void PlayWinAnime_RPC()
    {
        playerControl.WinAnim();
    }

    [PunRPC]
    private void PlayLoseAnime_RPC()
    {
        playerControl.LoseAnim();
    }

}
