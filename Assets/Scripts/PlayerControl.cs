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
	private Skills Attack_default;
	private int Attack_default_Num=4;
    public float speed = 6f;
    public float distance = 0f;
    int pNum;
    private bool isInputAble = false;
    private bool isDash = false;
    public bool IsInputAble
    {
        get { return isInputAble; }
        set
        {
            isInputAble = value;
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
    }
    // Use this for initialization
    void Start ()
    {
        pNum = PlayerManager.instance.myPnum;
        if (photonView.isMine)
        {
            PlayerManager.instance.Local = playerData;
            playerData.SetPlayerNum(PlayerManager.instance.myPnum);
        }
        else
        {
            PlayerManager.instance.Opponent = playerData;
            if(pNum == 1)
            {
                playerData.SetPlayerNum(2);
            }else
            {
                playerData.SetPlayerNum(1);
            }
        }
        playerData.SetStatus(1000,100,1000,0);
        SetPlayerPos(playerData.playerNum);
    }

    /// <summary>
    /// 입력 여기서 처리
    /// </summary>
    void LateUpdate ()
    {
        if(!photonView.isMine)
        {
            return;
        }
        if(!isInputAble)
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
         *         PlayerManager.instance.Local.SetCooltime(스킬 숫자, 클타임);
         * ...으로 각 스킬 스크립트에서 쿨타임 넣음
        */
        if (Input.GetKeyDown(KeyCode.Mouse1) && PlayerManager.instance.Local.cooltime[0] <= 0f)
        {
            DoSkill(0);//Skill1
        }
		else if (Input.GetKeyDown(KeyCode.E) && PlayerManager.instance.Local.cooltime[1] <= 0f)
        {
            DoSkill(1);//Skill2
        }
        else if (Input.GetKeyDown(KeyCode.R) && PlayerManager.instance.Local.cooltime[2] <= 0f)
        {
            DoSkill(2);//Skill2
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && PlayerManager.instance.Local.cooltime[3] <= 0f)
        {
            DoSkill(3);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerManager.instance.Local.cooltime[8] <= 0f)
        {
            Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }

		if (Input.GetKeyDown (KeyCode.Mouse0) && PlayerManager.instance.Local.cooltime [Attack_default_Num] <= 0f) {
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

    }

    #region privates
    [PunRPC]
    private void IsColEnable_RPC(bool b)
    {
        Debug.Log(playerData.playerNum +"col is "+ b);
        col.enabled = b;
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

        if (pNum == 1)
        {
            if (x > 0 && transform.position.x < 9)
            {
                
                rgbd.velocity += new Vector2(x, 0f) * speed;
            }

            if (x < 0 && transform.position.x > -9)
            {
                rgbd.velocity += new Vector2(x, 0f) * speed;
            }

            if (y > 0 && transform.position.y < 5)
            {
                rgbd.velocity += new Vector2(0f, y) * speed;
            }

            if (y < 0 && transform.position.y > -5)
            {
                rgbd.velocity += new Vector2(0f, y) * speed;
            }
        }
        else if (pNum ==2 )
        {
            if (x > 0 && transform.position.x < 9)
            {
                rgbd.velocity += new Vector2(x, 0f) * speed;
            }

            if (x < 0 && transform.position.x > -9)
            {
                rgbd.velocity += new Vector2(x, 0f) * speed;
            }

            if (y > 0 && transform.position.y < 5)
            {
                rgbd.velocity += new Vector2(0f, y) * speed;
            }

            if (y < 0 && transform.position.y > -5)
            {
                rgbd.velocity += new Vector2(0f, y) * speed;
            }
        }

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

        while(true)
        {
            if (timer >= 0.25f)
            {
                break;
            }

            rgbd.velocity = new Vector2(0f, 0f);

            speed -= Time.deltaTime * 48f;
            rgbd.velocity += new Vector2(x, y) * speed;

            if (transform.position.x > 9 || transform.position.x < -9 || transform.position.y > 5 || transform.position.y < -5)
                rgbd.velocity = new Vector2(0f, 0f);

            timer += Time.deltaTime;
            yield return null;
        }

        speed = 6f;
        isDash = false;
    }
    protected virtual void Teleport(float x, float y)
    {
        transform.position += new Vector3(x * distance, y * distance) ;
        if (pNum == 1)
        {
            if(transform.position.x>0)
            {
                transform.position = new Vector3(0, transform.position.y);
            }
            if (transform.position.x < -9)
            {
                transform.position = new Vector3(-9, transform.position.y);
            }
            if (transform.position.y > 5)
            {
                transform.position = new Vector3(transform.position.x, 5);
            }
            if (transform.position.y < -5)
            {
                transform.position = new Vector3(transform.position.x, -5);
            } 
        }
        else if (pNum == 2)
        {
            if (transform.position.x < 0)
            {
                transform.position = new Vector3(0, transform.position.y);
            }
            if (transform.position.x > 9)
            {
                transform.position = new Vector3(9, transform.position.y);
            }
            if (transform.position.y > 5)
            {
                transform.position = new Vector3(transform.position.x, 5);
            }
            if (transform.position.y < -5)
            {
                transform.position = new Vector3(transform.position.x, -5);
            }
        }
    }
    protected virtual void SetPlayerPos(int pnum)
    {
        if (pnum == 1)
        {
            transform.position = new Vector3(-4, 0, 0);
        }
        else if (pnum == 2)
        {
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
    public void IsColEnable(bool b)
    {
        photonView.RPC("IsColEnable_RPC", PhotonTargets.All, b);
    }

}
