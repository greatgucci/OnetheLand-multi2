using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_HeresyStigma : Photon.PunBehaviour
{
    int oNum;
    int shooterNum;
    GameObject impact;
    private bool isExist= true;
    GameObject parentObject;
    public void Init_HeresyStigma(int _shooterNum, int parent_domicilNum)
    {
        photonView.RPC("Init_HeresyStigma_RPC", PhotonTargets.All, _shooterNum, parent_domicilNum);
    }
    [PunRPC]
    private void Init_HeresyStigma_RPC(int _shooterNum, int parent_domicilNum)
    {
        shooterNum = _shooterNum;
        oNum = shooterNum == 1 ? 2 : 1;
        parentObject = PhotonView.Find(parent_domicilNum).gameObject;
        transform.SetParent(parentObject.transform);
        StartCoroutine(Impact());
    }
    public void Destroy_HeresyStigma()
    {
        photonView.RPC("Destroy_HeresyStigma_RPC", PhotonTargets.All);
    }
    [PunRPC]
    private void Destroy_HeresyStigma_RPC()
    {
        isExist = false;
        Destroy(gameObject);
    }
    private IEnumerator Impact()
    {
        float time=0;
        int sequence=1;
        while (isExist)
        {
            if(sequence ==1)
            {
                time += Time.deltaTime;
                if(time>1f)
                {
                    time = 1f;
                    sequence = 2;
                }
                ChangeColor(180f / 255f, 0f, 0f, time);
                yield return null;
            }
            else if(sequence ==2)
            {
                ChangeColor(1f, 1f, 1f, 1f);
                impact=PhotonNetwork.Instantiate("HeresyStigma_Explosion",transform.position,Quaternion.identity,0);
                Destroy_Heresy();
                yield return new WaitForSeconds(0.5f);
                ChangeColor(180f/255f, 0f, 0f, 1f);
                isExist = false;
            }
        }
    }
    private void ChangeColor(float r, float g , float b, float a)
    {
        GetComponent<SpriteRenderer>().color= new Color(r,g,b,a);
    }
    public void Destroy_Heresy()
    {
        photonView.RPC("Destroy_Heresy_RPC", PhotonTargets.All);
    }
    [PunRPC]
    private void Destroy_Heresy_RPC()
    {
        Destroy(impact);
    }
}
