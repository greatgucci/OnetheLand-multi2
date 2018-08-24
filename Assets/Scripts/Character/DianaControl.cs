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
	GameObject diana_pray_win;
	GameObject diana_white;
	int oNum;
	bool start;
	void Start()
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

		PlayerManager.instance.Local.CurrentPrayGage = pray.GetComponent<Diana_Skill4_Pray> ().praying_time;
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerData.cooltime[0] <= 0)
        {
            PlayerManager.instance.Local.SetCooltime(0, 1f);
            if (start) 
			{
				oNum = PlayerManager.instance.Local.playerNum == 1 ? 2 : 1;
				view_oponent = PlayerManager.instance.GetPlayerByNum (oNum).GetComponent<PhotonView> ();
				idannakin = PhotonNetwork.Instantiate ("idannakin",PlayerManager.instance.GetPlayerByNum(oNum).transform.position+new Vector3(0f, 0.9f,0f),Quaternion.identity,0).GetComponent<Diana_idannakin> ();
				idannakin.SetParent (view_oponent.viewID);
				SetActiveToServer (false);
				start = false;
			}
            if (!skill1_playing)
            {
                DoSkill(0);//좌
                playerAnimation.AddAnimationLayer(3, false);
                pray.GetComponent<Diana_Skill4_Pray>().praying = false;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse1) && playerData.cooltime[1] <= 0f)
        {
            PlayerManager.instance.Local.SetCooltime(1, 2f);
            DoSkill(1);//우
            playerAnimation.AddAnimationLayer(4, false);
            pray.GetComponent<Diana_Skill4_Pray>().praying = false;
        }
        else if (Input.GetKeyDown(KeyCode.E) && playerData.cooltime[2] <= 0f)
        {
            PlayerManager.instance.Local.SetCooltime(2, 5f);
            if (!skill1_playing)
			{
	            DoSkill(2);//E skill1
	            playerAnimation.AddAnimationLayer(5, false);
	            SetAnimationLayerEmpty(0.633f);
				pray.GetComponent<Diana_Skill4_Pray>().praying = false;
			}
        }
        else if (Input.GetKeyDown(KeyCode.R) && playerData.cooltime[3] <= 0f)
        {
            PlayerManager.instance.Local.SetCooltime(3, 8f);
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
            PlayerManager.instance.Local.SetCooltime(4, 6f);
            skill_can = false;
			DoSkill(4);//LeftShift skill3
			SetActiveToServer (false);
            pray.GetComponent<Diana_Skill4_Pray>().praying = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && playerData.cooltime[8] <= 0f)
        {
            Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            pray.GetComponent<Diana_Skill4_Pray>().praying = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            PlayerManager.instance.Local.SetCooltime(9, 0f);
            DoSkill(5);//Q 궁
            OnStartPrayAnimation();
        }
        if(!pray.GetComponent<Diana_Skill4_Pray>().praying)
        {
            OnCanclePrayAnimation();
        }
        if (skill4_create && pray.GetComponent<Diana_Skill4_Pray>().praying_time >= 1f)
        {
			PlayerManager.instance.Local.Defense = 1f;
			diana_pray_win=PhotonNetwork.Instantiate ("Diana_Pray_Win",transform.position,Quaternion.identity,0);
			Pray_Win ();
			PlayerManager.instance.GetPlayerByNum (2).GetStun (10f);
			PlayerManager.instance.GetPlayerByNum (1).GetStun (10f);
			skill4_create = false;
        }
    }
    public void OnStartPrayAnimation()
    {
        if (isPraying)
            return;
        playerAnimation.AddAnimationLayer(6, false);
        isPraying = true;
    }
    public void OnCanclePrayAnimation()
    {
        if(isPraying)
        {
            isPraying = false;
            SetAnimationLayerEmpty(0f);
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


	void Pray_Win()
	{
		photonView.RPC ("Pray_Win_RPC", PhotonTargets.All);
	}
	[PunRPC]
	void Pray_Win_RPC()
	{
		StartCoroutine (Impact ());
		StartCoroutine (UltDirectionCoroutine ());
	}
	IEnumerator Impact()
	{
		yield return new WaitForSeconds (2f);
		Destroy (diana_pray_win);
		diana_white=PhotonNetwork.Instantiate ("Diana_White",new Vector3(0f,0f,0f),Quaternion.identity,0);
		Map_White ();
		yield return null;
		
	}
	void Map_White()
	{
		photonView.RPC ("Map_White_RPC", PhotonTargets.All);
	}
	[PunRPC]
	void Map_White_RPC()
	{
		StartCoroutine (WhiteOut ());
	}
	IEnumerator WhiteOut()
	{
		for (float i = 0f; i < 255f; i += Time.deltaTime*255) {
			ColorMap(i);
			yield return null;
		}
		yield return new WaitForSeconds (1f);
		for (float i = 0f; i < 255f; i += Time.deltaTime*255*2) {
			ColorMap(255-i);
			yield return null;
		}
		Destroy_white ();
		ImWin ();
	}
	void ColorMap(float i)
	{

		photonView.RPC ("ColorMap_RPC", PhotonTargets.All, i);
	}
	[PunRPC]
	void ColorMap_RPC(float i)
	{
		diana_white.GetComponent<SpriteRenderer> ().color = new Color(1,1,1,i/255);
	}
	IEnumerator UltDirectionCoroutine()
	{
		float timer = 0f;
		SpriteRenderer mapColor = GameObject.Find("Map1BackGround").GetComponent<SpriteRenderer>(); //이거 다른 맵에서도 적용 가능하게 바꿔야함

		mapColor.color = new Color(0.6f, 0.6f, 0.8f, 1f);

		yield return new WaitForSeconds(0.5f);

		while (true)
		{
			if (timer >= 2f)
			{
				break;
			}

			mapColor.color = new Color(0.6f + (timer * 0.8f), 0.6f + (timer * 0.8f), 0.8f + (timer * 0.4f), 1f);

			timer += Time.deltaTime;
			yield return null;
		}
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
	private void ImWin()
	{
		NetworkManager.instance.GameOver (PlayerManager.instance.Opponent.playerNum);
	}
}
