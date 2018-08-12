using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Local 캐릭터가 아닌 캐릭터의 위치 동기화
public class MyViewTransform : Photon.MonoBehaviour, IPunObservable
{
    Vector3 realPosition = Vector3.zero;
    Quaternion realRotation = Quaternion.identity;
    public float lastUpdateTime = 0.1f;
 

    // Update is called once per frame
    void LateUpdate()
    {
        if (photonView.isMine)
        {
            return;
            //Do nothing, 로컬에서는 아무것도 안함
        }
        if((realPosition-transform.position).magnitude>=0.75f)
        {
            transform.position = realPosition;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, lastUpdateTime * Time.deltaTime);
        }

    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //This is OUR player.We need to send our actual position to the network
            stream.SendNext(transform.position);
        }
        else
        {
            //This is someone else's player.We need to receive their positin (as of a few millisecond ago, and update our version of that player.
            realPosition = (Vector3)stream.ReceiveNext();
        }

    }
}
