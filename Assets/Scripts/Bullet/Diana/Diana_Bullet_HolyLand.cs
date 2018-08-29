using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet_HolyLand :Bullet
{
    bool isSilenece = false;
	public void Init_Diana_HolyLand(int _shooterNum)
	{
		photonView.RPC ("Init_Diana_HolyLand_RPC",PhotonTargets.All,_shooterNum);
	}
	[PunRPC]
	private void Init_Diana_HolyLand_RPC(int _shooterNum)
    {
		shooterNum = _shooterNum;
		oNum=shooterNum==1? 2 : 1;
        tag = "HolyLand" + shooterNum;
        if(PlayerManager.instance.myPnum==shooterNum)
        {
            StartCoroutine(GetSilencing());
        }
    }
	protected override void OnTriggerStay2D (Collider2D collision)
	{
		if (isTirggerTime == true)
		{
			if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
			{
				return;
			}

			if (collision.tag == "Player" + oNum)
            //데미지 공식 - 레이저의 경우(디스트로이가 안 되는 경우) ( 20 * 초 * 데미지 )
            {
                PlayerManager.instance.GetPlayerByNum(oNum).GetSilence(true);
            }
		}
	}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isTirggerTime == true)
        {
            if (PlayerManager.instance.Local.playerNum != oNum)//피격자 입장에서 판정
            {
                return;
            }

            if (collision.tag == "Player" + oNum)
            //데미지 공식 - 레이저의 경우(디스트로이가 안 되는 경우) ( 20 * 초 * 데미지 )
            {
                PlayerManager.instance.GetPlayerByNum(oNum).GetSilence(false);
            }
        }
    }
    IEnumerator GetSilencing()
    {
        float time = 0;
        while(time<3f)
        {
            time += Time.deltaTime;
            yield return null;
        }
        DestroyToServer();
    }
}