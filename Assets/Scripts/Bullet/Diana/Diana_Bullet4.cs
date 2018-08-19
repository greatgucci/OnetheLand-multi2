using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diana_Bullet4 : Bullet
{
    public void Init_Diana_Bullet4(int _shooterNum)
    {
        photonView.RPC("Init_Diana_Bullet4_RPC", PhotonTargets.All, _shooterNum);
    }
    [PunRPC]
    protected void Init_Diana_Bullet4_RPC(int _shooterNum)
    {
        SetTag(type.bullet);
        StartCoroutine(Rotate_Cross);
    }
    private IEnumerator Rotate_Cross
    {
        get
        {
            DVector = new Vector3(Mathf.Cos(30 * Mathf.Deg2Rad), Mathf.Sin(30 * Mathf.Deg2Rad), 0f);
            for (int i = 0; i < 12; i++)
            {
                FavoriteFunction.RotateBullet(gameObject);
            }
        }
    }
}
