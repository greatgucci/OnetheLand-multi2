using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이제 자기가 맡은 캐릭터의 컨트롤러를 마음대로 수정합시다~
/// </summary>
public class DianaControl : PlayerControl {

    public bool skill1_playing = false;
    public bool skill4_create = false;
    public GameObject pray;
    public bool isPraying;
	public GameObject diana_pray_win;
    Diana_HeresyStigmaCreate Creating_HeresyStigma;
	GameObject diana_white;
    int oNum;
    PhotonView view_oponent;

    public bool attack_On = false;
    public int attack_Level = 0;
    float levelDownTime = 0f;

    protected override void Awake()
    {
        base.Awake();

        cooltime[0] = 1.5f;
        cooltime[1] = 1.5f;
        cooltime[2] = 1f;
        cooltime[3] = 10f;
        cooltime[4] = 7f;
        cooltime[5] = 60f; //

        cost[2] = 3;

    }

    protected override void LateUpdate()
    {
        base.LateUpdate();

        if (levelDownTime > 0f) levelDownTime -= Time.deltaTime;
        if (levelDownTime <= 0f && attack_Level > 0) attack_Level = 0;
    }

    protected override void StartCall()
    {
        base.StartCall();
        StartCoroutine(Ready());
    }

    private IEnumerator Ready()
    {
        yield return new WaitForSeconds(1f);
        oNum = GameManager.instance.myPnum == 1 ? 2 : 1;
        view_oponent = GameManager.instance.GetPlayerByNum(oNum).GetComponent<PhotonView>();
        if (photonView.isMine)
        {
            Creating_HeresyStigma = PhotonNetwork.Instantiate("HeresyStigmaCreate", GameManager.instance.GetPlayerByNum(oNum).transform.position, Quaternion.identity, 0).GetComponent<Diana_HeresyStigmaCreate>();
            Creating_HeresyStigma.Init_InHeresyStigma(GameManager.instance.myPnum, view_oponent.viewID);
        }
    }

    public override void SkillControl()
    {

        //TODO: 각자 Input에 따라 스킬 발동되게 작업

        if (InputSystem.instance.button1Pressed && playerData.cooltime[(int)SkillID.SKILL1] <= 0f)
        {
            DoSkill((int)SkillID.SKILL1);
        }
        else if (InputSystem.instance.button2Pressed && playerData.cooltime[(int)SkillID.SKILL2] <= 0f)
        {
            DoSkill((int)SkillID.SKILL2);
            Dash(InputSystem.instance.joyStickVector.x, InputSystem.instance.joyStickVector.y);
        }
        else if (InputSystem.instance.button3Pressed && playerData.cooltime[(int)SkillID.SKILL3] <= 0f)
        {
            DoSkill((int)SkillID.SKILL3);
        }
        else if (InputSystem.instance.button4Pressed && playerData.cooltime[(int)SkillID.SKILL4] <= 0f)
        {
            DoSkill((int)SkillID.SKILL4);
        }
        else if (InputSystem.instance.button5Pressed && playerData.cooltime[(int)SkillID.SKILL5] <= 0f)
        {
            DoSkill((int)SkillID.SKILL5);
        }

        else if (InputSystem.instance.button6Pressed && playerData.cooltime[(int)SkillID.SKILL6] <= 0f)
        {
            DoSkill((int)SkillID.SKILL6);
            GameManager.instance.Local.SetCooltime((int)SkillID.SKILL6, 4f);
            //GameManager.instance.Local.CurrentSkillGage -= 100;
        }
    }
    protected override void MoveAnimationChange(MoveState move)
    {
        if (move == moveState)
            return;

        switch (move)
        {
            case MoveState.Idle:
               playerAnimation.ChangeAnim(0,true);
                moveState = MoveState.Idle;
                break;
            case MoveState.MoveFront:
               playerAnimation.ChangeAnim(1,true);
                moveState = MoveState.MoveFront;
                break;
            case MoveState.MoveBack:
                playerAnimation.ChangeAnim(2,true);
                moveState = MoveState.MoveBack;
                break;
        }
    }

    public void IncreaseAttackLevel()
    {
        if(attack_Level < 2) attack_Level++;
        levelDownTime = 3f;
    }

	public void Pray_Win(int _shooterNum)
	{
		photonView.RPC ("Pray_Win_RPC", PhotonTargets.All, _shooterNum);
	}
	[PunRPC]
	void Pray_Win_RPC(int _shooterNum)
	{
       StartCoroutine(Impact(_shooterNum));
        
	}
	IEnumerator Impact(int _shooterNum)
	{
        if (!(GameManager.instance.myPnum == _shooterNum))
        {
            diana_white = Instantiate(Resources.Load("Diana_White") as GameObject, new Vector3(0f, 0f, 0f), Quaternion.identity);
            Map_White(_shooterNum);
            yield return null;
        }
		
	}
	void Map_White(int _shooterNum)
	{
		photonView.RPC ("Map_White_RPC", PhotonTargets.All, _shooterNum);
	}
	[PunRPC]
	void Map_White_RPC(int _shooterNum)
	{
		StartCoroutine (WhiteOut (_shooterNum));
	}
	IEnumerator WhiteOut(int _shooterNum)
	{
		for (float i = 0f; i < 255f; i += Time.deltaTime*255) {
            if(!(GameManager.instance.myPnum == _shooterNum))
                ColorMap(i,_shooterNum);
			yield return null;
		}
		yield return new WaitForSeconds (1f);
		for (float i = 0f; i < 255f; i += Time.deltaTime*255*2)
        {
            if (!(GameManager.instance.myPnum == _shooterNum))
                ColorMap(255- i,_shooterNum);
			yield return null;
		}
		Destroy_white ();
	}

	void ColorMap(float i, int _shooterNum)
	{

		photonView.RPC ("ColorMap_RPC", PhotonTargets.All, i, _shooterNum);
	}
	[PunRPC]
	void ColorMap_RPC(float i, int _shooterNum)
	{
        if(GameManager.instance.myPnum==_shooterNum)
		    diana_white.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,i/255);
	}


	void Destroy_white()
	{
		photonView.RPC ("Destroy_white_RPC", PhotonTargets.All);
	}
	[PunRPC]
	void Destroy_white_RPC()
	{
		Destroy (diana_white);
	}
}
