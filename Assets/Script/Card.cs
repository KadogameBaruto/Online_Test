using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public int ID { get; private set; }

    public Text ID_Text;

    // ìßâﬂèàóùóp
    public CanvasGroup CanGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        Debug.Log(this.ID);
        
        if(CardManager.Instance.SelectID == -1)
        {
            CardManager.Instance.SelectID = this.ID;
        }
    }

    public void SetID(int id)
    {
        this.ID = id;
        ID_Text.text = id.ToString();
    }
}
