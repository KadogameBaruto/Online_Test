using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class testButton : MonoBehaviour
{
    public Text a;

    private int count;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update()
    {
        a.text = count++.ToString();
    }

    public void OnClick()
    {
        count = 0;

    }


}
