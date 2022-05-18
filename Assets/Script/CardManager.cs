using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    // シングルトンの生成
    public static CardManager Instance;
    
    // 選択されたカードIDリスト
    public List<int> SelectedCardIdList = new List<int>();

    //
    public int SelectID = -1;

    //定義済み色リスト
    public List<List<Color>> DefineColorList = new List<List<Color>>();


    // クールタイム
    public float mCoolTime = 0;

    //カード最大枚数
    public readonly int cardMaxSize = 32;

    // Start is called before the first frame update
    void Start()
    {
        // ---- easy ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        List<Color> EasyColorList = new List<Color>();
        EasyColorList.Add(new Color(1.0f, 1.0f, 0.0f, 1.0f));
        EasyColorList.Add(new Color(1.0f, 0.0f, 1.0f, 1.0f));
        EasyColorList.Add(new Color(0.0f, 1.0f, 1.0f, 1.0f));
        EasyColorList.Add(new Color(0.0f, 0.0f, 1.0f, 1.0f));
        EasyColorList.Add(new Color(0.0f, 1.0f, 0.0f, 1.0f));
        EasyColorList.Add(new Color(1.0f, 0.0f, 0.0f, 1.0f));

        EasyColorList.Add(new Color(0.5f, 0.5f, 0.0f, 1.0f));
        EasyColorList.Add(new Color(0.5f, 0.0f, 0.5f, 1.0f));
        EasyColorList.Add(new Color(0.0f, 0.5f, 0.5f, 1.0f));
        EasyColorList.Add(new Color(0.0f, 0.0f, 0.5f, 1.0f));
        EasyColorList.Add(new Color(0.0f, 0.5f, 0.0f, 1.0f));
        EasyColorList.Add(new Color(0.5f, 0.0f, 0.0f, 1.0f));

        EasyColorList.Add(new Color(1.0f, 1.0f, 1.0f, 1.0f));
        EasyColorList.Add(new Color(0.66f, 0.66f, 0.66f, 1.0f));
        EasyColorList.Add(new Color(0.33f, 0.33f, 0.33f, 1.0f));
        EasyColorList.Add(new Color(0.0f, 0.0f, 0.0f, 1.0f));
        DefineColorList.Add(EasyColorList);
        // ---- normal ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        List<Color> NormalColorList = new List<Color>();
        NormalColorList.Add(new Color(0.8f, 0.8f, 0.8f, 1.0f));
        NormalColorList.Add(new Color(0.6f, 0.6f, 0.6f, 1.0f));
        NormalColorList.Add(new Color(0.4f, 0.4f, 0.4f, 1.0f));
        NormalColorList.Add(new Color(0.2f, 0.2f, 0.2f, 1.0f));

        NormalColorList.Add(new Color(1.0f, 0.8f, 0.8f, 1.0f));
        NormalColorList.Add(new Color(1.0f, 0.6f, 0.6f, 1.0f));
        NormalColorList.Add(new Color(1.0f, 0.4f, 0.4f, 1.0f));
        NormalColorList.Add(new Color(1.0f, 0.2f, 0.2f, 1.0f));

        NormalColorList.Add(new Color(0.8f, 1.0f, 0.8f, 1.0f));
        NormalColorList.Add(new Color(0.6f, 1.0f, 0.6f, 1.0f));
        NormalColorList.Add(new Color(0.4f, 1.0f, 0.4f, 1.0f));
        NormalColorList.Add(new Color(0.2f, 1.0f, 0.2f, 1.0f));

        NormalColorList.Add(new Color(0.8f, 0.8f, 1.0f, 1.0f));
        NormalColorList.Add(new Color(0.6f, 0.6f, 1.0f, 1.0f));
        NormalColorList.Add(new Color(0.4f, 0.4f, 1.0f, 1.0f));
        NormalColorList.Add(new Color(0.2f, 0.2f, 1.0f, 1.0f));


        //for (int i = 0; i < cardMaxSize / 2; i++)
        //{
        //    NormalColorList.Add(new Color(1.0f, (float)1.0 / ((i % 4) + 1), (float)1.0 / ((i / 4) +1), 1.0f));
        //}
        DefineColorList.Add(NormalColorList);
        // ---- hard ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- ---- 
        List<Color> HardColorList = new List<Color>();

        for (int i = 0; i < cardMaxSize / 2; i++)
        {
            HardColorList.Add(new Color((float)1.0 / i, (float)1.0 / i, (float)1.0 / i, 1.0f));
        }
        DefineColorList.Add(HardColorList);


        //tmpDefineColorList.Add(new Color(1.0f, 1.0f, 0.5f, 1.0f));
        //tmpDefineColorList.Add(new Color(1.0f, 0.5f, 1.0f, 1.0f));
        //tmpDefineColorList.Add(new Color(0.5f, 1.0f, 1.0f, 1.0f));
        //tmpDefineColorList.Add(new Color(0.5f, 0.5f, 1.0f, 1.0f));
        //tmpDefineColorList.Add(new Color(0.5f, 1.0f, 0.5f, 1.0f));
        //tmpDefineColorList.Add(new Color(1.0f, 0.5f, 0.5f, 1.0f));


        // カードオブジェクトを生成する
        List<int> RandomIDList = Enumerable.Range(0, cardMaxSize).Select(i => i).OrderBy(i => System.Guid.NewGuid()).ToList();
        for (int i = 0; i < cardMaxSize; i++)
        {
            // Instantiate で Cardオブジェクトを生成
            Card card = (Card)Instantiate(this.CardPrefab, this.CardParent);

            // データを設定する
            card.SetID(RandomIDList[i]);

            // 生成したカードオブジェクトを保存する
            this.CardList.Add(card);
        }
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void HideCards(List<int> containCardIdList)
    {
        foreach (var _card in this.CardList)
        {
            // 既に獲得したカードIDの場合、非表示にする
            if (containCardIdList.Contains(_card.ID))
            {
                _card.CanGroup.alpha = 0;
            }
            // 獲得していないカードは裏面表示にする
            else
            {
                // カードを裏面表示にする
                _card.SetHide();
            }
        }
    }

    //カードを表返す処理
    public void OpenCards(List<int> containCardIdList,int gameLevel)
    {
        foreach (var _card in this.CardList)
        {
            if (containCardIdList.Contains(_card.ID))
            {
                //_card.OpenCard();
                _card.cardImage.texture = null;
                _card.cardImage.color = DefineColorList[gameLevel][_card.ID % (CardManager.Instance.cardMaxSize / 2)];
            }
        }
    }

    public bool CheckReverseCard(out List<int> selectIDList)
    {
        if (SelectedCardIdList.Count == 2 && isMatchID(SelectedCardIdList[0], SelectedCardIdList[1]))
        {
            selectIDList = new List<int> { SelectedCardIdList[0], SelectedCardIdList[1] };
            return true;
        }

        selectIDList = null;
        return false;
    }

    private bool isMatchID(int a, int b)
    {
        return a % (cardMaxSize / 2) == b % (cardMaxSize / 2);
    }

    public void CreateCard()
    {

        //System.Random r = new System.Random();
        //Vector3 spawnPosition = new Vector3(r.Next(0, 200), r.Next(0, 200), 0); //生成位置

        //GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        //image.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
}
