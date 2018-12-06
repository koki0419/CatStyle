using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのステータス変更ボタンスクリプト
/// </summary>

public class PlayerStatus : MonoBehaviour
{

    //----------------Unityコンポーネント関係----------------
    //通常状態画像
    [SerializeField]
    private Sprite state0;
    //攻撃乗頼画像
    [SerializeField]
    private Sprite state1;

    //-------------クラスの定義----------------------------
    [SerializeField]
    private　PlayerController playerController;


    //-------------数値用の変数定義----------------------------


    //-------------フラグ用の変数定義----------------------------

    // Use this for initialization
    void Start()
    {
        if (playerController.nowPlayerState == PlayerController.NowPlayerState.NormalMoad)
        {
            gameObject.GetComponent<Image>().sprite = state1;
        }
        else if (playerController.nowPlayerState == PlayerController.NowPlayerState.ChainMoad)
        {
            gameObject.GetComponent<Image>().sprite = state0;
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //---------------関数の定義----------------------

    //***************ステータス変更ボタン********************
    public void OnClickStateButton()
    {
        //通常モードからチェインモードに変更
        if(playerController.nowPlayerState == PlayerController.NowPlayerState.NormalMoad)
        {
            playerController.nowPlayerState = PlayerController.NowPlayerState.ChainMoad;
            gameObject.GetComponent<Image>().sprite = state0;
        }
        //チェインモードから通常モードに変更
        else if (playerController.nowPlayerState == PlayerController.NowPlayerState.ChainMoad)
        {
            playerController.nowPlayerState = PlayerController.NowPlayerState.NormalMoad;
            gameObject.GetComponent<Image>().sprite = state1;
        }
        
    }
}
