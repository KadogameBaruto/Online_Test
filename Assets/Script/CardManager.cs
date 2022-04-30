using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    public static CardManager Instance;

    //
    public int SelectID = -1;

    // Start is called before the first frame update
    void Start()
    {
        // �J�[�h�I�u�W�F�N�g�𐶐�����
        for (int i = 0; i < 16; i++)
        {
            // Instantiate �� Card�I�u�W�F�N�g�𐶐�
            Card card = (Card)Instantiate(this.CardPrefab, this.CardParent);

            // �f�[�^��ݒ肷��
            card.SetID(i);

            // ���������J�[�h�I�u�W�F�N�g��ۑ�����
            this.CardList.Add(card);

            Debug.Log(i);
        }

        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideCardList(int ID)
    {
        foreach (var _card in this.CardList)
        {
            if (_card.ID == ID)
            {
                _card.CanGroup.alpha = 0;
            }

        }
    }



    public void CreateCard()
    {

        //System.Random r = new System.Random();
        //Vector3 spawnPosition = new Vector3(r.Next(0, 200), r.Next(0, 200), 0); //�����ʒu

        //GameObject image = PhotonNetwork.Instantiate("MyImage", spawnPosition, Quaternion.identity, 0);
        //image.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    }
}
