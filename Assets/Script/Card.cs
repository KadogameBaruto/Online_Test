using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int ID { get; private set; }

    public Text ID_Text;

    public Color color;

    public RawImage cardImage;


    
    // �I������Ă��邩����
    //private bool mIsSelected = false;
    public bool mIsSelected = false;

    // ���ߏ����p
    public CanvasGroup CanGroup;


    // �J�[�h����
    private Sprite _Ura;
    private Sprite Ura
    {
        get
        {
            if (_Ura == null)
            {
                _Ura = Resources.Load<Sprite>("Image/ura");
            }
            return _Ura;
        }
    }

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
        this.cardImage.texture = Ura.texture;

        this.cardImage.color = Color.white;
        ID_Text.text = "";


    }

    ///  <summary>
    /// �J�[�h��\�\�L�ɂ���
    /// </summary>
    public void OpenCard()
    {
        //this.ID_Text.text = (this.ID % (CardManager.Instance.cardMaxSize / 2)).ToString();
        //this.cardImage.texture = null;
        //this.cardImage.color = CardManager.Instance.DefineColorList[this.ID % (CardManager.Instance.cardMaxSize / 2)];
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
