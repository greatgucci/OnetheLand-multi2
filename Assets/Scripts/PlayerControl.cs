using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Input과 컨트롤을 관리
/// </summary>
public class PlayerControl : Photon.PunBehaviour
{
    private Rigidbody2D rgbd;
    private BoxCollider2D col;
    public Skills[] Skills;
	public Skills[] Attack_defaults;
    GameObject StunEffect;
	private Skills Attack_default;
	private int Attack_default_Num=4;
    public float speed = 6f;
    private float distance = 0.8f;
    int pNum;
    private bool isFaceRight = false;
    private bool isInputAble = true;
    private bool isDash = false;

    private bool IsFaceRight
    {
        get { return isFaceRight; }
        set
        {
            isFaceRight = value;
            if (isFaceRight)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    PlayerAnimation playerAnimation;
    PlayerData playerData;
    private void Awake()
    {
        rgbd = GetComponent<Rigidbody2D>();
        playerData = transform.Find("PlayerData").GetComponent<PlayerData>();
        playerAnimation = GetComponentInChildren<PlayerAnimation>();
        col = GetComponent<BoxCollider2D>();
        StunEffect = Resources.Load("Effects/Stun") as GameObject;
    }
    // Use this for initialization
    void Start ()
    {
        pNum = PlayerManager.instance.myPnum;
        if (photonView.isMine)
        {
            PlayerManager.instance.Local = playerData;
            playerData.SetPlayerNum(PlayerManager.instance.myPnum,this);
        }
        else
        {
            PlayerManager.instance.Opponent = playerData;
            if(pNum == 1)
            {
                playerData.SetPlayerNum(2, this);
            }else
            {
                playerData.SetPlayerNum(1, this);
            }
        }
        playerData.SetStatus(1000,100,1000,0);
        SetPlayerPos(playerData.playerNum);
    }

    /// <summary>
    /// 입력 여기서 처리
    /// </summary>
    protected virtual void LateUpdate ()
    {
        if (!photonView.isMine || !isInputAble || PlayerManager.instance.gameUpdate != GameUpdate.GAMING)
        {
            rgbd.velocity = Vector2.zero;
            return;
        }


        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        /*
         * 0 : 마우스 우클릭
         * 1 : E
         * 2 : R
         * 3 : 좌 Shift
         * 4 : 일반 공격 1
         * 5 : 일반 공격 2
         * 6 : 일반 공격 3
         * 7 : 일반 공격 4
         * 8 : 대시
         * 9 : 궁극기
         *         PlayerManager.instance.Local.SetCooltime(스킬 숫자, 클타임);
         * ...으로 각 스킬 스크립트에서 쿨타임 넣음
        */
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerData.cooltime[0] <= 0f)
        {
            DoSkill(0);//Skill1
        }
		else if (Input.GetKeyDown(KeyCode.E) && playerData.cooltime[1] <= 0f)
        {
            DoSkill(1);//Skill2
        }
        else if (Input.GetKeyDown(KeyCode.R) && playerData.cooltime[2] <= 0f)
        {
            DoSkill(2);//Skill2
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && playerData.cooltime[3] <= 0f)
        {
            DoSkill(3);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerData.cooltime[8] <= 0f)
        {
            Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

        else if (Input.GetKeyDown(KeyCode.Q) && playerData.cooltime[9] <= 0f)
        {
            DoSkill(4);
        }

		if (Input.GetKeyDown (KeyCode.Mouse0) && playerData.cooltime [Attack_default_Num] <= 0f) {
			AttackSet (Attack_default_Num-4);
			Attack_default.Excute ();
		}

		if(Input.GetKeyDown (KeyCode.Alpha1))
		{
			Attack_default_Num = 4;
		}
		else if(Input.GetKeyDown (KeyCode.Alpha2))
		{
			Attack_default_Num = 5;
		}
		else if(Input.GetKeyDown (KeyCode.Alpha3))
		{
			Attack_default_Num = 6;
		}
		else if(Input.GetKeyDown (KeyCode.Alpha4))
		{
			Attack_default_Num = 7;
		}
        if(IsFaceRight && playerData.aimVector.x<0)
        {
            photonView.RPC("SetIsFaceRight", PhotonTargets.All, false);
        }else if(!IsFaceRight && playerData.aimVector.x>0)
        {
            photonView.RPC("SetIsFaceRight", PhotonTargets.All, true);
        }
    }

    #region privates
    [PunRPC]
    private void SetIsFaceRight(bool b)
    {
        IsFaceRight = b;
    }

    [PunRPC]
    private void SetColliderEnable_RPC(bool b)
    {
        if(photonView.isMine)
        {
            col.enabled = b;
        }
    }

    [PunRPC]
    private void GetStun_RPC()
    {
        StartCoroutine(StunRoutine());
    }
    [PunRPC]
    private void SetInputEnable_RPC(bool b)
    {
        if(photonView.isMine)
        {
            isInputAble = b;
        }
    }

    private void DoSkill(int skillNum)
    {
        Skills[skillNum].Excute();
    }
	private void AttackSet(int skillNum)
	{
		Attack_default = Attack_defaults [skillNum];
	}

    protected virtual void Move(float x, float y)
    {
        if (isDash == true)
        {
            return;
        }
        rgbd.velocity = new Vector2(0f, 0f);

        Vector2 moveVector;
        moveVector = new Vector2(x, y);
        moveVector.Normalize();
        speed = 6f;

        
            if (x > 0 && transform.position.x > 9)
            {   
                moveVector = new Vector2(0,moveVector.y);
            }
            if (x < 0 && transform.position.x < -9)
            {
                moveVector = new Vector2(0, moveVector.y);
            }
            if (y > 0 && transform.position.y > 5)
            {
                moveVector = new Vector2(moveVector.x, 0);
            }
            if (y < 0 && transform.position.y < -5)
            {
             moveVector = new Vector2(moveVector.x, 0);
            }

        rgbd.velocity += moveVector*speed;
    }

    protected void Dash(float x, float y)
    {
        StartCoroutine(DashPlay(x, y));
    }

    IEnumerator DashPlay(float x, float y)
    {
        isDash = true;

        float timer = 0f;
        speed = 18f;

        Vector2 moveVector;
        moveVector = new Vector2(x, y);
        moveVector.Normalize();

        while (true)
        {
            if (timer >= 0.25f)
            {
                break;
            }

            rgbd.velocity = new Vector2(0f, 0f);

            speed -= Time.deltaTime * 48f;
            rgbd.velocity += moveVector * speed;

            if (transform.position.x > 9 || transform.position.x < -9 || transform.position.y > 5 || transform.position.y < -5)
                rgbd.velocity = new Vector2(0f, 0f);

            timer += Time.deltaTime;
            yield return null;
        }

        speed = 6f;
        isDash = false;
    }

    protected virtual void SetPlayerPos(int pnum)
    {
        if (pnum == 1)
        {
            transform.position = new Vector3(-4, 0, 0);
            isFaceRight = true;
        }
        else if (pnum == 2)
        {
            isFaceRight = false;
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = new Vector3(4, 0, 0);
        }
    }
    #endregion
    public void WinAnim()
    {
        if (photonView.isMine)
        {

        }
    }
    public void LoseAnim()
    {
        if (photonView.isMine)
        {

        }
    }
    public void StartAnim()
    {
        if (photonView.isMine)
        {

        }
    }
    IEnumerator StunRoutine()
    {
        GameObject stun = Instantiate(StunEffect, transform);

        if (photonView.isMine)
        {
            isInputAble = false;
        }
        yield return new WaitForSeconds(1f);
        DestroyImmediate(stun);
        yield return new WaitForEndOfFrame();
        if (photonView.isMine)
        {
            isInputAble = true;
        }
    }

}
