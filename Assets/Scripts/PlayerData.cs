using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 규칙 : 로컬의 캐릭터 데이터는 로컬에서만 수정하도록 한다.
/// Local에서 이동,Hp(동기화됨),스킬게이지(동기화됨) 관리하는 스크립트
/// </summary>
public class PlayerData : Photon.PunBehaviour, IPunObservable
{
    #region Variables
    public int playerNum;
    private float currentHp;
    private float fullHp;
    private float currentSkillGage;
    private float fullSkillGage;
    public float CurrentHp
    { get { return currentHp; }
        set
        {
            if(photonView.isMine )
            {
                currentHp = value;
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
        }else
        {
            fullHp = _fullHp;
            fullSkillGage = _fullSkg;
            currentHp = _hp;
            currentSkillGage = _skg;
            UpdateHpUI(currentHp);
            UpdateSkgUI(currentSkillGage);
        }
    }
    #endregion

    private void Update()
    {
        if(photonView.isMine)
        {
            CurrentSkillGage += Time.deltaTime * 0.5f;
        }
    }

    /// <summary>
    /// Local아닐경우 이것으로 UI업데이트
    /// </summary>
    private void UpdateHpUI(float hp)
    {
     if(PlayerManager.instance.playMode == PlayMode.ONLINE)
        UIManager.instance.SetHp(playerNum, currentHp);
    }
    private void UpdateSkgUI(float skg)
    {
        if (PlayerManager.instance.playMode == PlayMode.ONLINE)
            UIManager.instance.SetSkg(playerNum, currentSkillGage);
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

    public void SetPlayerNum(int pnum)
    {
        playerNum = pnum;
        transform.parent.gameObject.tag = "Player" + pnum;
        transform.parent.gameObject.name = gameObject.name + "_" + pnum;
    }
    #endregion

}
