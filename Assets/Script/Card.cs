using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int ID { get; private set; }

    public Text ID_Text;

    public RawImage cardImage;
    
    // 選択されているか判定
    //private bool mIsSelected = false;
    public bool mIsSelected = false;

    // 透過処理用
    public CanvasGroup CanGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 選択された時の処理
    /// </summary>
    public void OnClick()
    {
        // カードが表面になっていた場合は無効
        if (this.mIsSelected ||
            CardManager.Instance.mCoolTime != 0 || 
            CardManager.Instance.SelectID != -1 ||
            GamePlayer.Instance.IsMyTurn() == false)
        {
            return;
        }

        // 選択判定フラグを有効にする
        this.mIsSelected = true;

        CardManager.Instance.SelectID = this.ID;

        GamePlayer.Instance.AddPoint(-10);
    }

    public void SetID(int id)
    {
        this.ID = id;
        ID_Text.text = "";
    }
    ///  <summary>
    /// カードを背面表記にする
    /// </summary>
    public void SetHide()
    {
        // 選択判定フラグを初期化する
        this.mIsSelected = false;

        // カードを背面表示にする
        //this.CardImage.sprite = Ura;

        cardImage.color = Color.white;
        ID_Text.text = "";

    }

    /// <summary>
    /// カードを非表示にする
    /// </summary>
    public void SetInvisible()
    {
        // 選択済設定にする
        this.mIsSelected = true;

        // アルファ値を0に設定 (非表示)
        this.CanGroup.alpha = 0;
    }

}
