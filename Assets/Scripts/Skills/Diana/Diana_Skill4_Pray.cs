using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Skill4_Pray : Skills {

    GameObject warnning;
    GameObject warnning_pile;
    public float praying_time;
	public bool praying;
	GameObject impact;
	Diana_pary_aura pary;
	PhotonView view;
    PhotonView view_me;
    private void Awake()
    {
        warnning_pile=Resources.Load("WarningCircle") as GameObject;
        view_me = GetComponent<PhotonView>();
    }
    void Start()
	{
		transform.parent.GetComponent<DianaControl> ().pray = gameObject;
		view = transform.parent.GetComponent<PhotonView> ();
	}
	public override void Excute ()
	{
        AudioController.instance.PlayEffectSound(Character.DIANA, 7);
	    StartCoroutine (Pray ());
	}
	IEnumerator Pray()
	{
		impact=PhotonNetwork.Instantiate("Diana_Pray",transform.position,Quaternion.identity,0);
		pary = impact.GetComponent<Diana_pary_aura> ();
		pary.SetParent(view.viewID);
        PlayerManager.instance.Local.Defense = 1f;
        PlayerManager.instance.GetPlayerByNum(PlayerManager.instance.myPnum == 1 ? 2 : 1).GetSilence(5f);
        PlayerManager.instance.GetPlayerByNum(PlayerManager.instance.myPnum).GetFetter(5f);
        float praying_time_temp=0;
        Diana_Bullet1_Thunder thunder;
        float x;
        float y;
        while (true)
		{
            x= Random.Range(-9f,9f);
            y = Random.Range(-5f, 5f);
            Warnning(PlayerManager.instance.myPnum, new Vector3(x, y, 0f));
            yield return new WaitForSeconds(0.2f);
            AudioController.instance.PlayEffectSound(Character.DIANA, 2);
            thunder = PhotonNetwork.Instantiate("Diana_Thunder", new Vector3(x, y, 0f), Quaternion.identity, 0).GetComponent<Diana_Bullet1_Thunder>();
            thunder.Diana_Thunder(PlayerManager.instance.myPnum,2);
            praying_time_temp += 0.25f;
			if (praying_time_temp > 5f)
			{
				impact.GetComponent<Diana_pary_aura> ().DestroyToServer ();
                transform.parent.GetComponent<DianaControl>().Pray_Win(PlayerManager.instance.myPnum);
                break;
			}
            yield return new WaitForSeconds(0.05f);
		}
	}
    void Warnning(int shooterNum, Vector3 position)
    {
        view_me.RPC("Warnning_RPC",PhotonTargets.All,shooterNum, position);
    }
    [PunRPC]
    void Warnning_RPC(int shooterNum, Vector3 position)
    {
        if (!(PlayerManager.instance.myPnum == shooterNum))
        {
            Debug.Log("a");
            warnning = Instantiate(warnning_pile, position, Quaternion.identity);
            warnning.transform.localScale = transform.localScale;
        }
    }
}
