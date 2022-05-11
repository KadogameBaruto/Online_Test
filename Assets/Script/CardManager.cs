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


    // クールタイム
    public float mCoolTime = 0;

    //カード最大枚数
    private const int CARD_MAX_SIZE = 32;

    // Start is called before the first frame update
    void Start()
    {
        // カードオブジェクトを生成する
        List<int> RandomIDList = Enumerable.Range(0, CARD_MAX_SIZE).Select(i => i).OrderBy(i => System.Guid.NewGuid()).ToList();
        for (int i = 0; i < CARD_MAX_SIZE; i++)
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

    public void HideCardList(List<int> containCardIdList)
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
                Debug.Log(_card.ID);
                _card.mIsSelected = false;
            }
        }
    }

    public void ReverseCardList(List<int> containCardIdList)
    {
        foreach (var _card in this.CardList)
        {
            if (containCardIdList.Contains(_card.ID))
            {
                _card.ID_Text.text = _card.ID.ToString();
                _card.cardImage.color = Color.red;
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

        selectIDList = new List<int> { -1 };
        return false;
    }

    private bool isMatchID(int a, int b)
    {
        return a % (CARD_MAX_SIZE / 2) == b % (CARD_MAX_SIZE / 2);
    }

    public void CreateCard()
    {

        //System.Random r = new System.Random();
        //Vector3 spawnPosition = new Vector3(r.Next(0, 200), r.Next(0, 200), 0); //生成位置

        //GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        //image.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
}
