using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Login : MonoBehaviourPunCallbacks
{
    public static Login Instance;

    private int count;

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
        //count = 20000;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Instance = this;
    }

    void OnGUI()
    {
        //ログインの状態を画面上に出力
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString() + "\r\n" +
            PhotonNetwork.LocalPlayer.ActorNumber + "\r\n" +
            PhotonNetwork.LocalPlayer.IsMasterClient + "\r\n" 

            );
    }


    //ルームに入室前に呼び出される
    public override void OnConnectedToMaster()
    {
        // "room"という名前のルームに参加する（ルームが無ければ作成してから参加する）
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //ルームに入室後に呼び出される
    public override void OnJoinedRoom()
    {
        //キャラクターを生成

        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            System.Random r = new System.Random();
            Vector3 spawnPosition = new Vector3(r.Next(0, 500), r.Next(0, 500), 0); //生成位置

            GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        }
        ////自分だけが操作できるようにスクリプトを有効にする
        //MonsterScript monsterScript = monster.GetComponent<MonsterScript>();
        //monsterScript.enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            count++;
        }
    }
}
