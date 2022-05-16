using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class GamePlayer : MonoBehaviourPunCallbacks, IPunObservable
{
    //�v���C���[���\���p�ϐ�
    private GameObject pInfo;

    //�t�B�[���h���\���p�ϐ�
    private GameObject fInfo;

    //�J�[�h�}�l�[�W���[
    private GameObject cManager;

    // �V���O���g���̐���
    public static GamePlayer Instance;

    //���_
    private int point=0;

    //���݂̃^�[���v���C���[ID
    private int TurnPlayerID;//�{����FieldInfo�N���X��������ق�������

    void Start()
    {
        if (photonView.IsMine)
        {
            pInfo = GameObject.Find("Canvas/Infomation/Player_Info").gameObject;
            fInfo = GameObject.Find("Canvas/Infomation/Field_Info").gameObject;

            cManager = GameObject.Find("CardManager").gameObject;


            if (Instance == null)
            {
                Instance = this;
            }

            //�v���C���[���̕\��
            string str = "NetworkClientState:" + PhotonNetwork.NetworkClientState.ToString() + "\r\n" +
                         "ActorNumber:" + PhotonNetwork.LocalPlayer.ActorNumber + "\r\n" +
                         "IsMasterClient:" + PhotonNetwork.LocalPlayer.IsMasterClient + "\r\n";
            ShowPlayerInfo(str);

            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("TurnPlayerID", out object value))
            {
                TurnPlayerID = (int)value;
                ShowFieldInfo(MakeFieldInfoString());
            }

            AddPoint(0);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (CardManager.Instance.SelectID != -1)
            {
                //CardManager.Instance.HideCardList(CardManager.Instance.SelectID);

                PhotonView photonView = PhotonView.Get(this);
                photonView.RPC("HideCardList2", RpcTarget.AllViaServer, CardManager.Instance.SelectID);

                CardManager.Instance.SelectID = -1;
            }
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
        //if (stream.IsWriting)
        //{
        //    stream.SendNext(Login.Instance.getCount());
        //}
        //else
        //{
        //    Login.Instance.setCount((int)stream.ReceiveNext());
        //}
    }

    public void ShowPlayerInfo(string str)
    {
        pInfo.GetComponent<TextMeshProUGUI>().text = str;
    }

    public void ShowFieldInfo(string str)
    {
        fInfo.GetComponent<TextMeshProUGUI>().text = str;
    }

    public void AddPoint(int val)
    {
        if (IsMyTurn())
        {
            point += val;
            ShowFieldInfo(MakeFieldInfoString());

            var hashTable = new ExitGames.Client.Photon.Hashtable();
            hashTable["player"+ PhotonNetwork.LocalPlayer.ActorNumber.ToString()] = point;
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashTable);
        }
    }

    public string GetAllPlayerPointStr()
    {
        string str = "";
        Player[] player = PhotonNetwork.PlayerList;
        for (int i = 0; i < player.Length ; i++)
        {
            string tmpPlayerName = "player" + player[i].ActorNumber.ToString();
            int tmpPoint=0;
            if (PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue(tmpPlayerName, out object value))
            {
                tmpPoint = (int)value;
            }

            str += tmpPlayerName + ":" + tmpPoint + "\r\n";
        }

        return str;
    }

    public string MakeFieldInfoString()
    {
        return point.ToString() + " point\r\n" +
               "TurnPlayerID:" + TurnPlayerID.ToString() + "\r\n" +
               "PlayerList:" + Login.Instance.GetPlayersStr();
    }

    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        if (photonView.IsMine)
        {
            //�ύX�̂������v���p�e�B��"TurnPlayerID"���܂܂�Ă���Ȃ�TurnPlayerID���X�V
            if (propertiesThatChanged.TryGetValue("TurnPlayerID", out object value))
            {
                TurnPlayerID = (int)value;
                ShowFieldInfo(MakeFieldInfoString());
            }
        }            
    }

    public bool IsMyTurn()
    {
        return TurnPlayerID == PhotonNetwork.LocalPlayer.ActorNumber;
    }

    public void GoNextTurnPlayer()
    {
        if (IsMyTurn() && photonView.IsMine)
        {
            // Room�ɎQ�����Ă���v���C���[����z��Ŏ擾.
            Player[] player = PhotonNetwork.PlayerList;

            var playerInfo = player.Select((x, indexer) => new { x, indexer })
                                   .Where(e => e.x.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
                                   .FirstOrDefault();

            TurnPlayerID = player[(playerInfo.indexer + 1) % player.Length].ActorNumber;
            ShowFieldInfo(MakeFieldInfoString());

            var hashTable = new ExitGames.Client.Photon.Hashtable();
            hashTable["TurnPlayerID"] = TurnPlayerID;
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashTable);
        }
    }
}
