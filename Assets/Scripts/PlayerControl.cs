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
    public float speed = 6f;
    public float distance = 0f;
    int pNum;
    private bool isInputAble = false;
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
        
        if (Input.GetKeyDown(KeyCode.Z) && PlayerManager.instance.Local.cooltime[0] <= 0f)
        {
            DoSkill(0);//Skill1
            PlayerManager.instance.Local.SetCooltime(0, 1f);
        }
        else if (Input.GetKeyDown(KeyCode.X) && PlayerManager.instance.Local.cooltime[1] <= 0f)
        {
            DoSkill(1);//Skill2
            PlayerManager.instance.Local.SetCooltime(1, 1f);
        }
        else if (Input.GetKeyDown(KeyCode.C) && PlayerManager.instance.Local.cooltime[2] <= 0f)
        {
            DoSkill(2);//Skill2
            PlayerManager.instance.Local.SetCooltime(2, 1f);
        }
        else if (Input.GetKeyDown(KeyCode.V) && PlayerManager.instance.Local.cooltime[3] <= 0f)
        {
            DoSkill(3);
            PlayerManager.instance.Local.SetCooltime(3, 1f);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && PlayerManager.instance.Local.cooltime[8] <= 0f)
        {
            Teleport(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            PlayerManager.instance.Local.SetCooltime(8, 0.5f);
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

    protected virtual void Move(float x, float y)
    {
        rgbd.velocity = new Vector2(0f, 0f);
        
        if (pNum == 1)
        {
            if (x > 0 && transform.position.x < 0)
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

            if (x < 0 && transform.position.x > 0)
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
