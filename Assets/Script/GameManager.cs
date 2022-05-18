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

    //���U���g�}�l�[�W���[
    public ResultManager ResultManager;

    //�\�̖���
    private int OmoteCount = 0;
    //������̖���
    private int AtariCount = 0;

    //��v�����Ƃ��̉�
    public AudioSource AtariSound;
    //��v���Ȃ������Ƃ��̉�
    public AudioSource HazureSound;
    //Congratulation��
    public AudioSource GameClearSound;
    //�߂��艹
    public AudioSource MekuriSound;

    private bool IsAtari;

    // �V���O���g���̐���
    public static GameManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
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

                if(IsAtari)
                {
                    //�����艹����
                    AtariSound.PlayOneShot(AtariSound.clip);
                    AtariCount += 2;
                }
                else
                {
                    //�͂��ꉹ����
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

            // �J�[�h��\�Ԃ�
            this.CardCeator.OpenCards(CardManager.Instance.SelectedCardIdList,Login.Instance.GetGameLevel());

            OmoteCount = CardManager.Instance.SelectedCardIdList.Count;

            //�߂��艹����
            MekuriSound.PlayOneShot(MekuriSound.clip);
        }

        // �I�������J�[�h���Q���ȏ�ɂȂ�����
        if (CardManager.Instance.SelectedCardIdList.Count >= 2)
        {
            CheckContinueOrNextTurn();
        }
    }

    public void CheckContinueOrNextTurn()
    {
        List<int> selectIDList = new List<int>();
        // 2���ڂɂ������J�[�h�ƈꏏ��������
        if (CardManager.Instance.CheckReverseCard(out selectIDList))
        {
            //�J�[�h����v�����ꍇ
            // ��v�����J�[�hID��ۑ�����
            this.mContainCardIdList.AddRange(selectIDList);

            GamePlayer.Instance.AddPoint(100);

            //������t���O(���Ԃ��ۂ̉��Ŏg�p)
            IsAtari = true;

        }
        else
        {
            //�J�[�h����v���Ȃ������ꍇ
            GamePlayer.Instance.GoNextTurnPlayer();

            //������t���O(���Ԃ��ۂ̉��Ŏg�p)
            IsAtari = false;
        }

        CardManager.Instance.mCoolTime = 1.0f;

        // �I�������J�[�h���X�g������������
        CardManager.Instance.SelectedCardIdList.Clear();
        OmoteCount = 0;

    }

}
