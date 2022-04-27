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
    // ���v���C���[�����[���֎Q���������ɌĂ΂��R�[���o�b�N
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
        //���O�C���̏�Ԃ���ʏ�ɏo��
        GUILayout.Label(PhotonNetwork.NetworkClientState.ToString() + "\r\n" +
            PhotonNetwork.LocalPlayer.ActorNumber + "\r\n" +
            PhotonNetwork.LocalPlayer.IsMasterClient + "\r\n" 

            );
    }


    //���[���ɓ����O�ɌĂяo�����
    public override void OnConnectedToMaster()
    {
        // "room"�Ƃ������O�̃��[���ɎQ������i���[����������΍쐬���Ă���Q������j
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
    }

    //���[���ɓ�����ɌĂяo�����
    public override void OnJoinedRoom()
    {
        //�L�����N�^�[�𐶐�

        if(PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            System.Random r = new System.Random();
            Vector3 spawnPosition = new Vector3(r.Next(0, 500), r.Next(0, 500), 0); //�����ʒu

            GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        }
        ////��������������ł���悤�ɃX�N���v�g��L���ɂ���
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
