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

    //画面表示時に呼ばれる
    void OnEnable()
    {
        //リザルト画面表示
        ResutMainText.text = GamePlayer.Instance.GetAllPlayerPointStr();
    }
}
