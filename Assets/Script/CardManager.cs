using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CardManager : MonoBehaviour
{
    // 生成するCardオブジェクト
    [SerializeField]
    private Card CardPrefab;

    // Cardオブジェクトの生成先となる親オブジェクト
    [SerializeField]
    private RectTransform CardParent;

    // 生成したカードオブジェクトを保存する
    private List<Card> CardList = new List<Card>();

    public static CardManager Instance;

    //
    public int SelectID = -1;

    // Start is called before the first frame update
    void Start()
    {
        // カードオブジェクトを生成する
        for (int i = 0; i < 16; i++)
        {
            // Instantiate で Cardオブジェクトを生成
            Card card = (Card)Instantiate(this.CardPrefab, this.CardParent);

            // データを設定する
            card.SetID(i);

            // 生成したカードオブジェクトを保存する
            this.CardList.Add(card);

            Debug.Log(i);
        }

        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideCardList(int ID)
    {
        foreach (var _card in this.CardList)
        {
            if (_card.ID == ID)
            {
                _card.CanGroup.alpha = 0;
            }

        }
    }



    public void CreateCard()
    {

        //System.Random r = new System.Random();
        //Vector3 spawnPosition = new Vector3(r.Next(0, 200), r.Next(0, 200), 0); //生成位置

        //GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        //image.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
}
