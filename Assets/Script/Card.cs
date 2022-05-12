using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int ID { get; private set; }

    public Text ID_Text;

    public RawImage cardImage;
    
    // �I������Ă��邩����
    //private bool mIsSelected = false;
    public bool mIsSelected = false;

    // ���ߏ����p
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
    /// �I�����ꂽ���̏���
    /// </summary>
    public void OnClick()
    {
        // �J�[�h���\�ʂɂȂ��Ă����ꍇ�͖���
        if (this.mIsSelected ||
            CardManager.Instance.mCoolTime != 0 || 
            CardManager.Instance.SelectID != -1 ||
            GamePlayer.Instance.IsMyTurn() == false)
        {
            return;
        }

        // �I�𔻒�t���O��L���ɂ���
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
    /// �J�[�h��w�ʕ\�L�ɂ���
    /// </summary>
    public void SetHide()
    {
        // �I�𔻒�t���O������������
        this.mIsSelected = false;

        // �J�[�h��w�ʕ\���ɂ���
        //this.CardImage.sprite = Ura;

        cardImage.color = Color.white;
        ID_Text.text = "";

    }

    /// <summary>
    /// �J�[�h���\���ɂ���
    /// </summary>
    public void SetInvisible()
    {
        // �I���ϐݒ�ɂ���
        this.mIsSelected = true;

        // �A���t�@�l��0�ɐݒ� (��\��)
        this.CanGroup.alpha = 0;
    }

}
