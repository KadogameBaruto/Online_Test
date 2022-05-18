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

    //リザルトマネージャー
    public ResultManager ResultManager;

    //表の枚数
    private int OmoteCount = 0;
    //あたりの枚数
    private int AtariCount = 0;

    //一致したときの音
    public AudioSource AtariSound;
    //一致しなかったときの音
    public AudioSource HazureSound;
    //Congratulation音
    public AudioSource GameClearSound;
    //めくり音
    public AudioSource MekuriSound;

    private bool IsAtari;

    // シングルトンの生成
    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
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

                if(IsAtari)
                {
                    //あたり音発生
                    AtariSound.PlayOneShot(AtariSound.clip);
                    AtariCount += 2;
                }
                else
                {
                    //はずれ音発生
                    HazureSound.PlayOneShot(HazureSound.clip);
                }

                if (AtariCount == CardManager.Instance.cardMaxSize)
                //if(AtariCount == 2)
                {
                    ResultManager.gameObject.SetActive(true);
                }

            }
            else
            {
                CardManager.Instance.mCoolTime -= Time.deltaTime;
            }
        }

        if(OmoteCount != CardManager.Instance.SelectedCardIdList.Count)
        {

            // カードを表返す
            this.CardCeator.OpenCards(CardManager.Instance.SelectedCardIdList,Login.Instance.GetGameLevel());

            OmoteCount = CardManager.Instance.SelectedCardIdList.Count;

            //めくり音発生
            MekuriSound.PlayOneShot(MekuriSound.clip);
        }

        // 選択したカードが２枚以上になったら
        if (CardManager.Instance.SelectedCardIdList.Count >= 2)
        {
            CheckContinueOrNextTurn();
        }
    }

    public void CheckContinueOrNextTurn()
    {
        List<int> selectIDList = new List<int>();
        // 2枚目にあったカードと一緒だったら
        if (CardManager.Instance.CheckReverseCard(out selectIDList))
        {
            //カードが一致した場合
            // 一致したカードIDを保存する
            this.mContainCardIdList.AddRange(selectIDList);

            GamePlayer.Instance.AddPoint(100);

            //あたりフラグ(裏返す際の音で使用)
            IsAtari = true;

        }
        else
        {
            //カードが一致しなかった場合
            GamePlayer.Instance.GoNextTurnPlayer();

            //あたりフラグ(裏返す際の音で使用)
            IsAtari = false;
        }

        CardManager.Instance.mCoolTime = 1.0f;

        // 選択したカードリストを初期化する
        CardManager.Instance.SelectedCardIdList.Clear();
        OmoteCount = 0;

    }

}
