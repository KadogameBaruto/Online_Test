using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // ��v�����J�[�h���X�gID
    //private List<int> mContainCardIdList = new List<int>();
    public List<int> mContainCardIdList = new List<int>();

    // �J�[�h�����}�l�[�W���N���X
    public CardManager CardCeator;

    //�\�̖���
    private int OmoteCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�N�[���^�C������
        if(CardManager.Instance.mCoolTime > 0)
        {
            if(CardManager.Instance.mCoolTime - Time.deltaTime <= 0)
            {
                CardManager.Instance.mCoolTime = 0;
                // �J�[�h�𗠕Ԃ�
                this.CardCeator.HideCards(this.mContainCardIdList);
            }
            else
            {
                CardManager.Instance.mCoolTime -= Time.deltaTime;
            }
        }

        if(OmoteCount != CardManager.Instance.SelectedCardIdList.Count)
        {
            // �J�[�h��\�Ԃ�
            this.CardCeator.OpenCards(CardManager.Instance.SelectedCardIdList);

            OmoteCount = CardManager.Instance.SelectedCardIdList.Count;
        }

        // �I�������J�[�h���Q���ȏ�ɂȂ�����
        if (CardManager.Instance.SelectedCardIdList.Count >= 2)
        {
            List<int> selectIDList = new List<int>();
            // 2���ڂɂ������J�[�h�ƈꏏ��������
            if (CardManager.Instance.CheckReverseCard(out selectIDList))
            {
                //�J�[�h����v�����ꍇ
                // ��v�����J�[�hID��ۑ�����
                this.mContainCardIdList.AddRange(selectIDList);

                GamePlayer.Instance.AddPoint(100);

            }
            else
            {
                //�J�[�h����v���Ȃ������ꍇ
                GamePlayer.Instance.GoNextTurnPlayer();

            }
            
            CardManager.Instance.mCoolTime = 1.0f;

            // �I�������J�[�h���X�g������������
            CardManager.Instance.SelectedCardIdList.Clear();
        }
    }
}
