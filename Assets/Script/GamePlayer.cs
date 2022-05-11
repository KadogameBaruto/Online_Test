using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class GamePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    //�v���C���[���\���p�ϐ�
    private GameObject pInfo;

    //�J�[�h�}�l�[�W���[
    private GameObject cManager;




    void Start()
    {
        pInfo = GameObject.Find("Canvas/Infomation/Player_Info").gameObject;

        cManager = GameObject.Find("CardManager").gameObject;
    }

    void Update()
    {
       //�v���C���[���̕\��
        pInfo.GetComponent<TextMeshProUGUI>().text = GetPlayerInfo() + "\r\n";


        if(CardManager.Instance.SelectID != -1)
        {
            //CardManager.Instance.HideCardList(CardManager.Instance.SelectID);

            PhotonView photonView = PhotonView.Get(this);
            photonView.RPC("HideCardList2", RpcTarget.AllViaServer, CardManager.Instance.SelectID);

            CardManager.Instance.SelectID = -1;

        }

        if (photonView.IsMine)
        {

        }
    }

    [PunRPC]
    public void HideCardList2(int ID)
    {
        //CardManager.Instance.HideCardList(ID);

        // �I������CardId��ۑ�
        CardManager.Instance.SelectedCardIdList.Add(ID);
    }

    void OnGUI()
    {
        //if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
        //{
        //    print("You clicked the button!");
        //}
    }

    //�������̂����
    void IPunObservable.OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(Login.Instance.getCount());
        }
        else
        {
            Login.Instance.setCount((int)stream.ReceiveNext());
        }
    }

    public string GetPlayerInfo()
    {
        string rtn = "NetworkClientState:" + PhotonNetwork.NetworkClientState.ToString() + "\r\n" +
                     "ActorNumber:" + PhotonNetwork.LocalPlayer.ActorNumber + "\r\n" +
                     "IsMasterClient:" + PhotonNetwork.LocalPlayer.IsMasterClient + "\r\n";

        return rtn;
    }
}
