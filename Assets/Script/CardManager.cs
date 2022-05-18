using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // ��������Card�I�u�W�F�N�g
    [SerializeField]
    private Card CardPrefab;

    // Card�I�u�W�F�N�g�̐�����ƂȂ�e�I�u�W�F�N�g
    [SerializeField]
    private RectTransform CardParent;

    // ���������J�[�h�I�u�W�F�N�g��ۑ�����
    private List<Card> CardList = new List<Card>();

    // �V���O���g���̐���
    public static CardManager Instance;
    
    // �I�����ꂽ�J�[�hID���X�g
    public List<int> SelectedCardIdList = new List<int>();

    //
    public int SelectID = -1;

    //��`�ςݐF���X�g
    public List<List<Color>> DefineColorList = new List<List<Color>>();


    // �N�[���^�C��
    public float mCoolTime = 0;

    //�J�[�h�ő喇��
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


        // �J�[�h�I�u�W�F�N�g�𐶐�����
        List<int> RandomIDList = Enumerable.Range(0, cardMaxSize).Select(i => i).OrderBy(i => System.Guid.NewGuid()).ToList();
        for (int i = 0; i < cardMaxSize; i++)
        {
            // Instantiate �� Card�I�u�W�F�N�g�𐶐�
            Card card = (Card)Instantiate(this.CardPrefab, this.CardParent);

            // �f�[�^��ݒ肷��
            card.SetID(RandomIDList[i]);

            // ���������J�[�h�I�u�W�F�N�g��ۑ�����
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
            // ���Ɋl�������J�[�hID�̏ꍇ�A��\���ɂ���
            if (containCardIdList.Contains(_card.ID))
            {
                _card.CanGroup.alpha = 0;
            }
            // �l�����Ă��Ȃ��J�[�h�͗��ʕ\���ɂ���
            else
            {
                // �J�[�h�𗠖ʕ\���ɂ���
                _card.SetHide();
            }
        }
    }

    //�J�[�h��\�Ԃ�����
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
        //Vector3 spawnPosition = new Vector3(r.Next(0, 200), r.Next(0, 200), 0); //�����ʒu

        //GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        //image.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
}
