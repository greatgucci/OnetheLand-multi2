using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_SpecialAttacks : Bullet
{
    PhotonView view;
    public void Init_Diana_Shootgun(int _shooterNum, Vector3 dVector)
    {
        photonView.RPC("Init_Diana_Shootgun_RPC", PhotonTargets.All, _shooterNum, dVector);
    }
    [PunRPC]
    private void Init_Diana_Shootgun_RPC(int _shooterNum, Vector3 dVector)
    {
        shooterNum = _shooterNum;
        oNum = shooterNum == 1 ? 2 : 1;
        view = GetComponent<PhotonView>();
    }
    public void DamageCalculating(int damage)
    {
        photonView.RPC("DamageCalculating_RPC", PhotonTargets.All, damage);
    }
    [PunRPC]
    private void DamageCalculating_RPC(int damage)
    {
        
    }
    protected override void OnTriggerStay2D(Collider2D collision)
    {

    }
}
