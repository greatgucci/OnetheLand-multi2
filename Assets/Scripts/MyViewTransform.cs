using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SystemHalf;
using System;
using ExitGames.Client.Photon;
//Local 캐릭터가 아닌 캐릭터의 위치 동기화
public class MyViewTransform : Photon.MonoBehaviour, IPunObservable
{
    
    private void Awake()
    {
    }
    public float lastUpdateTime = 0.1f;
    Vector3 realPosition = Vector3.zero;
    // Update is called once per frame
    void LateUpdate()
    {
        if (photonView.isMine)
        {
            return;
            //Do nothing, 로컬에서는 아무것도 안함
        }

        transform.position = Vector3.Lerp(transform.position, realPosition, lastUpdateTime * Time.deltaTime);
       
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
             stream.SendNext((short)(new Half_Float(transform.position.x).Value));
             stream.SendNext((short)(new Half_Float(transform.position.y).Value));
        }
        else
        {
            realPosition.x = Half_Float.ToHalf(((ushort)(short)stream.ReceiveNext()));
            realPosition.y = Half_Float.ToHalf(((ushort)(short)stream.ReceiveNext()));
        }
    }
}
