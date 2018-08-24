using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이제 자기가 맡은 캐릭터의 컨트롤러를 마음대로 수정합시다~
/// </summary>
public class DianaControl : PlayerControl {

	public bool skill_can=false;
	public bool skill4_create = false;
	public bool skill1_playing = false;
	public GameObject pray;
    public bool isPraying;
	public Diana_idannakin idannakin;
	PhotonView view_oponent;
	public GameObject diana_pray_win;
	GameObject diana_white;
	int oNum;
	bool start;
	protected override void Start()
	{
		base.Start ();
		start = true;
	}
    protected override void MoveControl()
    {
        Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    protected override void SkillControl()
    {

		if (!skill1_playing)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && playerData.cooltime[0] <= 0)
             {
            PlayerManager.instance.Local.SetCooltime(0, 0f);
            if (start) 
			{
				oNum = PlayerManager.instance.Local.playerNum == 1 ? 2 : 1;
				view_oponent = PlayerManager.instance.GetPlayerByNum (oNum).GetComponent<PhotonView> ();
				idannakin = PhotonNetwork.Instantiate ("idannakin",PlayerManager.instance.GetPlayerByNum(oNum).transform.position+new Vector3(0f, 0.9f,0f),Quaternion.identity,0).GetComponent<Diana_idannakin> ();
				idannakin.SetParent (view_oponent.viewID);
				SetActiveToServer (false);
				start = false;
			}
                DoSkill(0);//좌
                playerAnimation.AddAnimationLayer(3, false);
                pray.GetComponent<Diana_Skill4_Pray>().praying = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerData.cooltime[1] <= 0f)
        {
            PlayerManager.instance.Local.SetCooltime(1, 1f);
            DoSkill(1);//우
            playerAnimation.AddAnimationLayer(4, false);
            pray.GetComponent<Diana_Skill4_Pray>().praying = false;
        }
        if (!skill1_playing)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerData.cooltime[2] <= 0f)
            {
            PlayerManager.instance.Local.SetCooltime(2, 2f);
	            DoSkill(2);//E skill1
	            playerAnimation.AddAnimationLayer(5, false);
	            SetAnimationLayerEmpty(0.633f);
				pray.GetComponent<Diana_Skill4_Pray>().praying = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && playerData.cooltime[3] <= 0f)
        {
            PlayerManager.instance.Local.SetCooltime(3, 3f);
            if (start)
			{
				oNum = PlayerManager.instance.Local.playerNum == 1 ? 2 : 1;
				view_oponent = PlayerManager.instance.GetPlayerByNum (oNum).GetComponent<PhotonView> ();
				idannakin = PhotonNetwork.Instantiate ("idannakin",PlayerManager.instance.GetPlayerByNum(oNum).transform.position+new Vector3(0f, 0.9f,0f),Quaternion.identity,0).GetComponent<Diana_idannakin> ();
				idannakin.SetParent (view_oponent.viewID);
				SetActiveToServer (false);
				start = false;
			}
			DoSkill(3);//R skill2

			PlayerManager.instance.GetPlayerByNum (oNum).GetSilence (2f);
            pray.GetComponent<Diana_Skill4_Pray>().praying = false;

        }
        else if ((Input.GetKeyDown(KeyCode.LeftShift) && playerData.cooltime[4] <= 0f) && skill_can)
        {
            PlayerManager.instance.Local.SetCooltime(4, 4f);
            skill_can = false;
			DoSkill(4);//LeftShift skill3
			SetActiveToServer (false);
            pray.GetComponent<Diana_Skill4_Pray>().praying = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && playerData.cooltime[9] <= 0f/* && PlayerManager.instance.Local.CurrentSkillGage >= 100f*/)
        {
            PlayerManager.instance.Local.SetCooltime(9, 10f);
            DoSkill(5);//Q 궁
            playerAnimation.AddAnimationLayer(6, false);
            SetAnimationLayerEmpty(6f);
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
	void SetActiveToServer(bool ing)
	{
		photonView.RPC ("SetActiveToServer_RPC", PhotonTargets.All, ing);
	}
	[PunRPC]
	void SetActiveToServer_RPC(bool ing)
	{
		idannakin.gameObject.SetActive (ing);
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
        if (!(PlayerManager.instance.myPnum == _shooterNum))
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
            if(!(PlayerManager.instance.myPnum == _shooterNum))
                ColorMap(i,_shooterNum);
			yield return null;
		}
		yield return new WaitForSeconds (1f);
		for (float i = 0f; i < 255f; i += Time.deltaTime*255*2)
        {
            if (!(PlayerManager.instance.myPnum == _shooterNum))
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
        if(PlayerManager.instance.myPnum==_shooterNum)
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
