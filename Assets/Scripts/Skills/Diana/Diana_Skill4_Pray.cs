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
        float praying_time_temp=0;
        Diana_Bullet1_Thunder thunder;
        float x;
        float y;

        GameManager.instance.Opponent.GetSilence(true);
        GameManager.instance.Local.GetSilence(true);
        GameManager.instance.Local.GetFetter(true);

        transform.parent.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        while (true)
		{
            x= Random.Range(-9f,9f);
            y = Random.Range(-5f, 5f);
            Warnning(GameManager.instance.myPnum, new Vector3(x, y, 0f));
            yield return new WaitForSeconds(0.2f);
            AudioController.instance.PlayEffectSound(Character.DIANA, 2);

            thunder = PhotonNetwork.Instantiate("Diana_Lighting", new Vector3(x, y, 0f), Quaternion.identity, 0).GetComponent<Diana_Bullet1_Thunder>();
            thunder.Diana_Thunder(GameManager.instance.myPnum,2);
            praying_time_temp += 0.25f;
            if (praying_time_temp > 6f)
			{
				impact.GetComponent<Diana_pary_aura> ().DestroyToServer ();
                transform.parent.GetComponent<DianaControl>().Pray_Win(GameManager.instance.myPnum);
                break;
			}
            yield return new WaitForSeconds(0.05f);
        }
        GameManager.instance.Local.GetSilence(false);
        GameManager.instance.Local.GetFetter(false);
        GameManager.instance.Opponent.GetSilence(false);
    }
    void Warnning(int shooterNum, Vector3 position)
    {
        view_me.RPC("Warnning_RPC",PhotonTargets.All,shooterNum, position);
    }
    [PunRPC]
    void Warnning_RPC(int shooterNum, Vector3 position)
    {
        if (!(GameManager.instance.myPnum == shooterNum))
        {
            warnning = Instantiate(warnning_pile, position, Quaternion.identity);
            warnning.transform.localScale = transform.localScale;
        }
    }
}
