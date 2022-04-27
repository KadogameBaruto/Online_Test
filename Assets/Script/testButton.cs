using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class testButton : MonoBehaviour
{
    public Text countText;

    private static int count;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }

    // Update is called once per frame
    void Update()
    {
        countText.text = Login.Instance.getCount().ToString();
    }

    public void OnClick()
    {
        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("Reset", RpcTarget.MasterClient);
    }
    [PunRPC]
    void Reset()
    {
        Login.Instance.resetCount();
    }


    void Awake()
    {
        this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }

}
