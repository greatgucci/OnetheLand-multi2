using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_HeresyStigmaCreate : Photon.PunBehaviour
{
    int oNum;
    int shooterNum;
    int mydomicilNum;
    GameObject parentObject;
    PhotonView view;
    Diana_HeresyStigma HeresyStigma;
    public bool isExist = false;
    private bool isCreate = true;
    public int Init_InHeresyStigma(int _shooterNum, int parent_domicilNum)
    {
        photonView.RPC("Init_InHeresyStigma_RPC", PhotonTargets.All, _shooterNum, parent_domicilNum);
        return mydomicilNum;
    }
    [PunRPC]
    private void Init_InHeresyStigma_RPC(int _shooterNum, int parent_domicilNum)
    {
        shooterNum = _shooterNum;
        name = "HeresyStigmaCreate" + shooterNum;
        view = GetComponent<PhotonView>();
        oNum = shooterNum == 1 ? 2 : 1;
        parentObject = PhotonView.Find(parent_domicilNum).gameObject;
        transform.SetParent(parentObject.transform);
        if(PlayerManager.instance.myPnum==shooterNum)
        {
            StartCoroutine(Create_HeresyStigma());
        }
        mydomicilNum = view.viewID;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "DianaNormalBullet" + shooterNum || collision.tag == "HolyLand" + shooterNum)
        {
            isExist = true;
        }
        
    }
    private IEnumerator Create_HeresyStigma()
    {
        while(true)
        {
            if(isCreate)
            {
                if (isExist)
                {
                    HeresyStigma = PhotonNetwork.Instantiate("HeresyStigma", transform.position+new Vector3(0f,0.9f,0f), Quaternion.identity, 0).GetComponent<Diana_HeresyStigma>();
                    HeresyStigma.Init_HeresyStigma(shooterNum, view.viewID);
                    isCreate = false;
                }
            }
            else
            {
                if(!isExist)
                {
                    HeresyStigma.Destroy_HeresyStigma();
                    isCreate = true;
                }
            }
            yield return null;
        }
    }

}
