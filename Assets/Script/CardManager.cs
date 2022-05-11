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


    // �N�[���^�C��
    public float mCoolTime = 0;

    //�J�[�h�ő喇��
    private const int CARD_MAX_SIZE = 32;

    // Start is called before the first frame update
    void Start()
    {
        // �J�[�h�I�u�W�F�N�g�𐶐�����
        List<int> RandomIDList = Enumerable.Range(0, CARD_MAX_SIZE).Select(i => i).OrderBy(i => System.Guid.NewGuid()).ToList();
        for (int i = 0; i < CARD_MAX_SIZE; i++)
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

    public void HideCardList(List<int> containCardIdList)
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
        //Vector3 spawnPosition = new Vector3(r.Next(0, 200), r.Next(0, 200), 0); //�����ʒu

        //GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        //image.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
}
