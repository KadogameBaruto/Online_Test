using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Login : MonoBehaviourPunCallbacks
{

    /*
     *残件
     * ルーム
     * 途中で落ちた場合
     * 難易度(色の自動作成or部屋によって生成する色を変える
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
    // 他プレイヤーがルームへ参加した時に呼ばれるコールバック
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        GamePlayer.Instance.ShowFieldInfo(GamePlayer.Instance.MakeFieldInfoString());
    }
    // 他プレイヤーがルームへ参加した時に呼ばれるコールバック
    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        GamePlayer.Instance.ShowFieldInfo(GamePlayer.Instance.MakeFieldInfoString());
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Instance = this;

        PhotonNetwork.SendRate = 20; // 1秒間にメッセージ送信を行う回数
        PhotonNetwork.SerializationRate = 10; // 1秒間にオブジェクト同期を行う回数
    }

    void OnGUI()
    {
        ////ログインの状態を画面上に出力
        //GUILayout.Label(PhotonNetwork.NetworkClientState.ToString() + "\r\n" +
        //    PhotonNetwork.LocalPlayer.ActorNumber + "\r\n" +
        //    PhotonNetwork.LocalPlayer.IsMasterClient + "\r\n" +
        //    CardManager.Instance.mCoolTime + "\r\n"

        //    );

    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        PhotonNetwork.JoinLobby();
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        //if(PhotonNetwork.LocalPlayer.IsMasterClient)
        //{
        //    System.Random r = new System.Random();
        //    Vector3 spawnPosition = new Vector3(r.Next(0, 400), r.Next(0, 400), 0); //生成位置

        //    GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        //}

        if (PhotonNetwork.IsMasterClient)
        {
            var hashTable = new ExitGames.Client.Photon.Hashtable();
            hashTable["TurnPlayerID"] = PhotonNetwork.LocalPlayer.ActorNumber;
            PhotonNetwork.CurrentRoom.SetCustomProperties(hashTable);
        }

        //プレイヤーを生成
        PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);

    }


    //ルームに入室後に呼び出される
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
