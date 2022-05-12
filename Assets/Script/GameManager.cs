using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 一致したカードリストID
    //private List<int> mContainCardIdList = new List<int>();
    public List<int> mContainCardIdList = new List<int>();

    // カード生成マネージャクラス
    public CardManager CardCeator;

    //表の枚数
    private int OmoteCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //クールタイム処理
        if(CardManager.Instance.mCoolTime > 0)
        {
            if(CardManager.Instance.mCoolTime - Time.deltaTime <= 0)
            {
                CardManager.Instance.mCoolTime = 0;
                // カードを裏返す
                this.CardCeator.HideCards(this.mContainCardIdList);
            }
            else
            {
                CardManager.Instance.mCoolTime -= Time.deltaTime;
            }
        }

        if(OmoteCount != CardManager.Instance.SelectedCardIdList.Count)
        {
            // カードを表返す
            this.CardCeator.OpenCards(CardManager.Instance.SelectedCardIdList);

            OmoteCount = CardManager.Instance.SelectedCardIdList.Count;
        }

        // 選択したカードが２枚以上になったら
        if (CardManager.Instance.SelectedCardIdList.Count >= 2)
        {
            List<int> selectIDList = new List<int>();
            // 2枚目にあったカードと一緒だったら
            if (CardManager.Instance.CheckReverseCard(out selectIDList))
            {
                //カードが一致した場合
                // 一致したカードIDを保存する
                this.mContainCardIdList.AddRange(selectIDList);

                GamePlayer.Instance.AddPoint(100);

            }
            else
            {
                //カードが一致しなかった場合
                GamePlayer.Instance.GoNextTurnPlayer();

            }
            
            CardManager.Instance.mCoolTime = 1.0f;

            // 選択したカードリストを初期化する
            CardManager.Instance.SelectedCardIdList.Clear();
        }
    }
}
