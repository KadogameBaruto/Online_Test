using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ResultManager : MonoBehaviour
{
    public Text ResutMainText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��ʕ\�����ɌĂ΂��
    void OnEnable()
    {
        //���U���g��ʕ\��
        ResutMainText.text = GamePlayer.Instance.GetAllPlayerPointStr();
    }
}
