using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Login : MonoBehaviourPunCallbacks
{

    /*
     *�c��
     * ���[��
     * �r���ŗ������ꍇ
     * ��Փx(�F�̎����쐬or�����ɂ���Đ�������F��ς���
     *     
     */
    public static Login Instance;

    private int count;

    [SerializeField]
    private MatchmakingView matchMaker;

    public void resetCount()
    {
        count = 0;
    }
    public void setCount(int c)
    {
        count = c;
    }
    public int getCount()
    {
        return count;
    }
    // ���v���C���[�����[���֎Q���������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GamePlayer.Instance.ShowFieldInfo(GamePlayer.Instance.MakeFieldInfoString());
    }
    // ���v���C���[�����[���֎Q���������ɌĂ΂��R�[���o�b�N
    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        GamePlayer.Instance.ShowFieldInfo(GamePlayer.Instance.MakeFieldInfoString());
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Instance = this;

        PhotonNetwork.SendRate = 20; // 1�b�ԂɃ��b�Z�[�W���M���s����
        PhotonNetwork.SerializationRate = 10; // 1�b�ԂɃI�u�W�F�N�g�������s����
    }

    void OnGUI()
    {
        ////���O�C���̏�Ԃ���ʏ�ɏo��
        //GUILayout.Label(PhotonNetwork.NetworkClientState.ToString() + "\r\n" +
        //    PhotonNetwork.LocalPlayer.ActorNumber + "\r\n" +
        //    PhotonNetwork.LocalPlayer.IsMasterClient + "\r\n" +
        //    CardManager.Instance.mCoolTime + "\r\n"

        //    );

    }


    //���[���ɓ����O�ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        PhotonNetwork.JoinLobby();
    }

    //���[���ɓ�����ɌĂяo�����
    public override void OnJoinedRoom()
    {
        //if(PhotonNetwork.LocalPlayer.IsMasterClient)
        //{
        //    System.Random r = new System.Random();
        //    Vector3 spawnPosition = new Vector3(r.Next(0, 400), r.Next(0, 400), 0); //�����ʒu

        //    GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        //}

        if (PhotonNetwork.IsMasterClient)
        {
            var hashTable = new ExitGames.Client.Photon.Hashtable();
            hashTable["TurnPlayerID"] = PhotonNetwork.LocalPlayer.ActorNumber;
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashTable);
        }

        //�v���C���[�𐶐�
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);

    }


    //���[���ɓ�����ɌĂяo�����
    public override void OnLeftRoom()
    {
        //PhotonNetwork.JoinLobby();

    }

    // Update is called once per frame
    void Update()
    {

        //if (PhotonNetwork.LocalPlayer.IsMasterClient)
        //{
        //    count++;
        //}
    }
    public override void OnJoinedLobby()
    {
        matchMaker.OnJoinedLobby();
    }


    public string GetPlayersStr()
    {
        string str="{";
        Player[] player = PhotonNetwork.PlayerList;
        for (int i = 0; i < player.Length -1; i++)
        {
            str += player[i].ActorNumber.ToString() + ",";
        }
        str += player[player.Length-1].ActorNumber.ToString()+"}";

        return str;
    }

    public void Disconnect()
    {
        if(GamePlayer.Instance.IsMyTurn())
        {
            GamePlayer.Instance.CheckContinueOrNextTurn();
        }
        PhotonNetwork.LeaveRoom();
    }

    public int GetGameLevel()
    {
        string roomName = PhotonNetwork.CurrentRoom.Name;
        Debug.Log(roomName);

        if(roomName =="Room1")
        {
            return 0;
        }
        else
        {
            if (roomName == "Room2")
            {
                return 1;

            }
            else
            {
                return 2;

            }
        }

    }
}
