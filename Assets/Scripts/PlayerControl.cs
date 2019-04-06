using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public enum MoveState
{
    NONE,
    MoveFront,
    MoveBack,
    Idle
}
/// <summary>
/// Input과 컨트롤을 관리
/// </summary>
public abstract class PlayerControl : Photon.PunBehaviour
{
    protected MoveState moveState = MoveState.NONE;
    protected Rigidbody2D rgbd;
    protected BoxCollider2D col;

    public Skills[] Skills;
    public MultiSound[] VoiceClip;

    protected GameObject StunEffect;
    public float weight = 0f;
    public float speed = 6f;
    protected int pNum;
    protected bool isInputAble = true;
    protected bool isMoveAble = true;
    protected bool isSkillAble = true;
    protected bool isPushed = false;
    protected bool isFaceRight = true;

    protected PlayerAnimation playerAnimation;
    protected PlayerData playerData;
    protected AudioSource voiceAudio;

    public float[] cooltime = new float[6];
    public short[] cost = new short[6];

    protected bool IsFaceRight
    {
        get { return isFaceRight; }
        set
        {
            isFaceRight = value;
            if (isFaceRight)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    protected virtual void MoveAnimationChange(MoveState move)
    {
    }

    /// <summary>
    /// 로컬 캐릭터만 보이스 가능
    /// </summary>
    /// <param name="i"></param>
    public virtual void PlayVoice(byte i)
    {
        if(photonView.isMine && !voiceAudio.isPlaying)
        photonView.RPC("PlayVoice_RPC",PhotonTargets.All,i);
    }


    protected virtual void Awake()
    {
        voiceAudio = transform.Find("Voice").GetComponent<AudioSource>();
        rgbd = GetComponent<Rigidbody2D>();
        playerData = transform.Find("PlayerData").GetComponent<PlayerData>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        col = GetComponent<BoxCollider2D>();
        StunEffect = Resources.Load("Effects/Stun") as GameObject;
    }

    // Use this for initialization
     void Start ()
    {
        StartCall();
    }

    protected virtual void StartCall()
    {
        pNum = GameManager.instance.myPnum;
        if (photonView.isMine)
        {
            GameManager.instance.Local = playerData;
            playerData.SetPlayerNum(GameManager.instance.myPnum, this);
            transform.Find("LocalPoint").gameObject.SetActive(true);
        }
        else
        {
            GameManager.instance.Opponent = playerData;
            if (pNum == 1)
            {
                playerData.SetPlayerNum(2, this);
            }
            else
            {
                playerData.SetPlayerNum(1, this);
            }
        }
        playerData.SetStatus(0, 100, 0);
        SetStartPosition(playerData.playerNum);

        GameManager.instance.PlayerUpdated();
    }

    protected virtual void LateUpdate()
    {
        if(!photonView.isMine)
        {
            return;
        }

        if (!isInputAble || GameManager.instance.gameUpdate != GameUpdate.INGAME)
        {
            rgbd.velocity = new Vector2(0, 0);
            return;
        }

        if(isMoveAble)
        {
            Move(InputSystem.instance.joyStickVector.x, InputSystem.instance.joyStickVector.y);
        }

        if(isSkillAble)
        {
            SkillControl();
        }
        return;
    }

    public virtual void SkillControl()
    {
    }

    public virtual void Move(float x, float y)
    {
        if (isPushed == true)
        {
            return;
        }
        rgbd.velocity = new Vector2(0f, 0f);

        Vector2 moveVector;
        moveVector = new Vector2(x, y);
        moveVector.Normalize();

        if (pNum == 1)
        {
            if (x > 0 && transform.position.x > -0.5f)
            {
                moveVector = new Vector2(0, moveVector.y);
            }
        }
        else if (pNum == 2)
        {
            if (x < 0 && transform.position.x < 0.5f)
            {
                moveVector = new Vector2(0, moveVector.y);
            }
        }

        if (y > 0 && transform.position.y > 2.5)
        {
            moveVector = new Vector2(moveVector.x, 0);
        }
        if (y < 0 && transform.position.y < -2.5)
        {
            moveVector = new Vector2(moveVector.x, 0);
        }

        if (x > 0 && isFaceRight)
        {
            MoveAnimationChange(MoveState.MoveFront);
        }
        else if (x > 0 && !isFaceRight)
        {
            MoveAnimationChange(MoveState.MoveBack);
        }
        else if (x < 0 && isFaceRight)
        {
            MoveAnimationChange(MoveState.MoveBack);
        }
        else if (x < 0 && !isFaceRight)
        {
            MoveAnimationChange(MoveState.MoveFront);
        }
        else
        {
            MoveAnimationChange(MoveState.Idle);
        }

        rgbd.velocity += moveVector * speed;
    }


    #region privates
    [PunRPC]
    protected void SetColliderEnable_RPC(bool b)
    {
        if(photonView.isMine)
        {
            col.enabled = b;
        }
    }

    [PunRPC]
    protected void GetStun_RPC(float t)
    {

			if (StunCoroutine != null)
			{
				StopCoroutine (StunCoroutine);
			}
			StunCoroutine=StartCoroutine(StunRoutine(t));
		

    }
	[PunRPC]
	protected void GetSilence_RPC(bool b)
	{
        if(photonView.isMine)
        {
            isSkillAble = !b;
        }
	}

    [PunRPC]
    protected void GetFetter_RPC(bool b)
    {
        if (photonView.isMine)
            isMoveAble = !b;
    }
    [PunRPC]
    protected void PlayVoice_RPC(byte i)
    {
        voiceAudio.clip = VoiceClip[i].RandomSound;
        voiceAudio.Play();
    }
    [PunRPC]
    protected void KnockBack_RPC(float x, float y, float knockback)
    {
        StartCoroutine(KnockBackPlay(x, y, knockback));
    }

    public void GetInvisible()
    {
        photonView.RPC("GetInvisible_RPC", PhotonTargets.All);
    }
    [PunRPC]
    protected void GetInvisible_RPC()
    {
        //반드시 이거 사용한 곳에서 코루틴으로 지속시간 후 CancleInvisible을 콜하도록
        if (photonView.isMine)
        {
            transform.Find("Renderer").GetComponent<SkeletonAnimation>().skeleton.SetColor(new Color(1f, 1f, 1f, 0.5f));
        }
        else
        {
            transform.Find("Renderer").GetComponent<SkeletonAnimation>().skeleton.SetColor(new Color(1f, 1f, 1f, 0f));
        }
    }

    public void CancleInvisible()
    {
        photonView.RPC("CancleInvisible_RPC", PhotonTargets.All);
    }
    [PunRPC]
    protected void CancleInvisible_RPC()
    {
        transform.Find("Renderer").GetComponent<SkeletonAnimation>().skeleton.SetColor(new Color(1f, 1f, 1f, 1f));

    }

    protected void DoSkill(int skillNum)
    {
        if (GameManager.instance.Local.CurrentSkillGage < cost[skillNum])
            return;
        GameManager.instance.Local.CurrentSkillGage -= cost[skillNum];
        Skills[skillNum].Excute();
        GameManager.instance.Local.SetCooltime(skillNum, cooltime[skillNum]);
    }




    protected void Dash(float x, float y)
    {
        StartCoroutine(DashPlay(x, y));
    }
    protected IEnumerator KnockBackPlay(float x, float y, float knockback)
    {

        isPushed = true;
        float knockBackTime = 0f;
        float accumulatedDamage = knockback * (0.01f * (float)GameManager.instance.GetPlayerByNum(pNum).CurrentDamage);

        Vector2 originSpeed = new Vector2(accumulatedDamage * x, accumulatedDamage * y);
        Vector2 moveSpeed;
        while(true)
        {
            if(knockBackTime>0.6f)
            {
                break;
            }
            knockBackTime += Time.deltaTime;

            moveSpeed = Vector2.Lerp(originSpeed,Vector2.zero,knockBackTime*1.6f);
            if (pNum == 1)
            {
                if (x > 0 && transform.position.x > -0.5f)
                {
                    moveSpeed = new Vector2(0, moveSpeed.y);
                }
            }
            else if (pNum == 2)
            {
                if (x < 0 && transform.position.x < 0.5f)
                {
                    moveSpeed = new Vector2(0, moveSpeed.y);
                }
            }

            if (y > 0 && transform.position.y > 2.5)
            {
                moveSpeed = new Vector2(moveSpeed.x, 0);
            }
            if (y < 0 && transform.position.y < -2.5)
            {
                moveSpeed = new Vector2(moveSpeed.x, 0);
            }
            rgbd.velocity = moveSpeed;
            yield return null;
        }
        isPushed = false;

    }
    protected IEnumerator DashPlay(float x, float y)
    {
        isPushed = true;

        float knockBackTime = 0f;
        float distance = 3;
        Vector2 originSpeed = new Vector2(x*distance, y*distance);
        Vector2 moveSpeed;

        while (true)
        {
            if (knockBackTime > 0.3f)
            {
                break;
            }
            knockBackTime += Time.deltaTime;

            moveSpeed = Vector2.Lerp(originSpeed, Vector2.zero, knockBackTime*3);

            if (pNum == 1)
            {
                if (x > 0 && transform.position.x > -0.5f)
                {
                    moveSpeed = new Vector2(0, moveSpeed.y);
                }
            }
            else if (pNum == 2)
            {
                if (x < 0 && transform.position.x < 0.5f)
                {
                    moveSpeed = new Vector2(0, moveSpeed.y);
                }
            }

            if (y > 0 && transform.position.y > 2.5)
            {
                moveSpeed = new Vector2(moveSpeed.x, 0);
            }
            if (y < 0 && transform.position.y < -2.5)
            {
                moveSpeed = new Vector2(moveSpeed.x, 0);
            }
            rgbd.velocity = moveSpeed;
            yield return null;
        }

        isPushed = false;
    }

    protected virtual void SetStartPosition(int pnum)
    {
        if (pnum == 1)
        {
            transform.position = new Vector3(-10, 0, 0);
            IsFaceRight = true;
        }
        else if (pnum == 2)
        {
            IsFaceRight = false;
            transform.position = new Vector3(10, 0, 0);
        }
    }
    /// <summary>
    /// 애니메이션 t초후에 멈추는 함수
    /// </summary>
    protected virtual void SetAnimationLayerEmpty(float t)
    {
        if(emptyLayerRoutine != null)
        {
            StopCoroutine(emptyLayerRoutine);
        }
        emptyLayerRoutine = StartCoroutine(AnimationEmptyLayerRoutine(t));
    }
    #endregion
    public void WinAnim()
    {
        if (photonView.isMine)
        {
            playerAnimation.ChangeAnim(7, false);
        }
    }
    public void LoseAnim()
    {
        if (photonView.isMine)
        {
           playerAnimation.ChangeAnim(8,false);
        }
    }
	Coroutine StunCoroutine;

    /// <summary>
    /// 기절은 코루틴으로
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    protected IEnumerator StunRoutine(float t)
    {
        GameObject stun = Instantiate(StunEffect, transform);

        if (photonView.isMine)
        {
            isInputAble = false;
        }
        yield return new WaitForSeconds(t);
        DestroyImmediate(stun);
        yield return new WaitForEndOfFrame();
        if (photonView.isMine)
        {
            isInputAble = true;
        }
    }

    Coroutine emptyLayerRoutine;
    protected IEnumerator AnimationEmptyLayerRoutine(float t)
    {
        yield return new WaitForSeconds(t);
        playerAnimation.SetAnimationLayerEmpty();
    }
}
