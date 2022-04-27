using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Synchronizer : MonoBehaviourPunCallbacks, IPunObservable
{
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Transform�̒l���X�g���[���ɏ�������ő��M����
            stream.SendNext(Login.Instance.getCount());
        }
        else
        {
            Login.Instance.setCount((int)stream.ReceiveNext());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
