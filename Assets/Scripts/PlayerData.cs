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
    private short currentDamage;
    private short currentSkillGage;
    private short fullSkillGage;

    private float globalCool = 0.25f;
    private float defense;

    private PlayerControl playerControl;
    public float[] cooltime = new float[10];
    public float cooltimeSpd = 1f;
    PlayerData opponent;
    protected AudioSource voice2;
    public MultiSound hitSource;

    public Vector3 aimVector = new Vector3(0f,0f,0f);//TODO : 제거
    public Vector3 aimPosition = new Vector3(0f, 0f, 0f);//TODO : 제거
    public Character character;
    public short CurrentDamage
    { get { return currentDamage; }
        set
        {
            if(GameManager.instance.gameUpdate == GameUpdate.END) //게임 종료시에는 체력 변화 없음
            {
                return;
            }
            if(photonView.isMine)
            {
                if(currentDamage>value)
                {
                    PlayHitSound();//내가 맞는소리는 로컬에서만
                }
                currentDamage = value;
                UpdateHpUI();
            }
            else { Debug.Log("주인이 아닌 캐릭터에서 수정에 접근했습니다."); }      
        }
    }

    public short CurrentSkillGage
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
                UpdateSkgUI();
            }
            else { Debug.Log("주인이 아닌 캐릭터에서 수정에 접근했습니다."); }
        }
    }
    public short FullSkillGage
    { get { return fullSkillGage; }
        set
        {
            if (photonView.isMine)
                fullSkillGage = value;
        }
    }
	

    public void SetStatus(short damage,short _fullSkg, short _skg)
    {
        if(photonView.isMine)
        {
            CurrentDamage = damage;
            FullSkillGage = _fullSkg;
            CurrentSkillGage = _skg;

            for(int i = 0; i < cooltime.Length; i++)
            {
                cooltime[i] = 0f;
            }

            opponent = GameManager.instance.Opponent;
        }
        else
        {
            fullSkillGage = _fullSkg;
            currentDamage = damage;
            currentSkillGage = _skg;
            UpdateHpUI();
            UpdateSkgUI();
        }
    }

    public void SetCooltime(int skillNum, float _cooltime)
    {
        for (int i = 0; i < cooltime.Length; i++)
        {

            if (i == skillNum)
            {
                cooltime[i] = _cooltime;
                //SkillCooltimeUI.SetCoolTimeUI(i, cooltime[i]);
            }
            else
            {
                if (cooltime[i] <= globalCool)
                {
                    cooltime[i] += globalCool;
                    //SkillCooltimeUI.SetCoolTimeUI(i, cooltime[i]);
                }
            }
        }
    }
    #endregion

    void Awake()
    {
        voice2 = GetComponent<AudioSource>();
    }

    private void PlayHitSound()
    {
        if (voice2.isPlaying)
            return;

            voice2.clip = hitSource.RandomSound;
            voice2.Play();
        
    }
    float counter = 0f;
    private void Update()
    {
        if(photonView.isMine)
        {           
            for (int i = 0; i < cooltime.Length; i++)
            {
                if (cooltime[i] > 0f)
                {
                    cooltime[i] -= Time.deltaTime * cooltimeSpd;
                }
            }

            if (CurrentDamage > 200)
            {
                CurrentDamage = 200;
            }

            if (transform.position.x < -20f && playerNum == 1)
            {
                NetworkManager.instance.GameOver(playerNum);
            }
            else if (transform.position.x > 20f && playerNum == 2)
            {
                NetworkManager.instance.GameOver(playerNum);
            }
        }
    }

    /// <summary>
    /// Local아닐경우 이것으로 UI업데이트
    /// </summary>
    private void UpdateHpUI()
    {
        UIManager.instance.SetHp(playerNum, currentDamage);
    }
    private void UpdateSkgUI()
    {
			UIManager.instance.SetSkg(playerNum, (float)currentSkillGage/fullSkillGage);
    }
    #region Public
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)//내가 이 캐릭터의 주인일때 stream에 write
        {
            stream.SendNext(currentDamage);
            stream.SendNext(currentSkillGage);
        }
        else//주인이 아니면 받고 UI에 적용
        {
            currentDamage = (short)stream.ReceiveNext();
            currentSkillGage = (short)stream.ReceiveNext();

            UpdateHpUI();
			UpdateSkgUI ();
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
	public void GetSilence(bool b)
	{
		playerControl.photonView.RPC("GetSilence_RPC", PhotonTargets.All,b);
	}
    public void GetFetter(bool b)
    {
        playerControl.photonView.RPC("GetFetter_RPC", PhotonTargets.All, b);
    }
    public void GetKnockBack(float x , float y)
    {
        playerControl.photonView.RPC("KnockBack_RPC", PhotonTargets.All, x, y);
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
