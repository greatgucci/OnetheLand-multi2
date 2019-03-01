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
    float tempTime = 0f;
    public override void SkillControl()
    {

        //TODO: 각자 Input에 따라 스킬 발동되게 작업

		if (!skill1_playing)
        {       

            if (Input.GetKeyDown(KeyCode.Mouse0) && playerData.cooltime[0] <= 0)
            {
                GameManager.instance.Local.SetCooltime(0, 0f);
                DoSkill(0);//좌
                playerAnimation.AddAnimationLayer(3, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && playerData.cooltime[1] <= 0f)
        {
            GameManager.instance.Local.SetCooltime(1, 1f);
            DoSkill(1);//우
            playerAnimation.AddAnimationLayer(4, false);
        }
        if (!skill1_playing)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerData.cooltime[2] <= 0f)
            {
            GameManager.instance.Local.SetCooltime(2, 3f);
	            DoSkill(2);//E skill1
	            playerAnimation.AddAnimationLayer(5, false);
	            SetAnimationLayerEmpty(0.633f);
            }
        }
        if (Input.GetKeyDown(KeyCode.R) && playerData.cooltime[3] <= 0f)
        {
            GameManager.instance.Local.SetCooltime(3, 5f);
            DoSkill(3);//R skill2

        }
        else if ((Input.GetKeyDown(KeyCode.LeftShift) && playerData.cooltime[4] <= 0f) && Creating_HeresyStigma.isExist)
        {
            GameManager.instance.Local.SetCooltime(4, 3f);
            Debug.Log(Creating_HeresyStigma.isExist);
            Creating_HeresyStigma.isExist = false;
            Debug.Log(Creating_HeresyStigma.isExist);
            DoSkill(4);//LeftShift skill3
        }
        else if (Input.GetKeyDown(KeyCode.Q) && playerData.cooltime[9] <= 0f && GameManager.instance.Local.CurrentSkillGage >= 100f)
        {
            GameManager.instance.Local.SetCooltime(9, 10f);
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
