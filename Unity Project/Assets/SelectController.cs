using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    //サムネイルの大きさ
    const float SCROLLVISW_MAX_H = 800;
    const float SCROLLVISW_MAX_W = 1300;


    public float SCROLLVISW_X = 0.5f;
    public float SCROLLVISW_Y = 1.5f;

    public GameObject ContentObj;

    //サムネイルステージ画面を取得
    public GameObject[] stageThumbnailObj;

    float h;
    float w;
    int release;

    public int stage;

    //サムネイル表示数　//スクリーン高さ（SCROLLVISW_MAX_H）　横幅（SCROLLVISW_MAX_W）　解放ステージ数（stage）
    void ThumDisplay(float screen_h, float screen_w, int stagerelease)
    {

        if (stagerelease < 5)
        {
            h = screen_h / Mathf.Sqrt(4);
            w = screen_w / Mathf.Sqrt(4);
            release = 4;
        }
        else if (stagerelease < 10)
        {
            h = screen_h / Mathf.Sqrt(9);
            w = screen_w / Mathf.Sqrt(9);
            release = 9;
        }
        else if (stagerelease < 17)
        {
            h = screen_h / Mathf.Sqrt(16);
            w = screen_w / Mathf.Sqrt(16);
            release = 16;
        }
        else if (stagerelease <= 25)
        {
            h = screen_h / Mathf.Sqrt(25);
            w = screen_w / Mathf.Sqrt(25);
            release = 25;
        }
        //表示するための関数を呼び出します
        Display(h, w, release);
    }//               ↓
    //                ↓
    //表示　　　　　　↓
    void Display(float h, float w, int stagerelease)
    {
        //各サムネイルのポジション格納用
        Vector3 pos;
        //表示するステージサムネイル数をカウントします
        int stageCount = 0;
        //表示元（親オブジェクト）の座標
        Vector3 view = ContentObj.transform.localPosition;


        //（y)軸ずらす
        for (int loop0 = 0; loop0 <= Mathf.Sqrt(stagerelease); loop0++)
        {
            //（x)軸ずらす 
            for (int loop1 = 0; loop1 < Mathf.Sqrt(stagerelease); loop1++)
            {
                
                if (stageCount < stagerelease)
                {
                    Debug.Log("デバック=" + stageCount);
                    pos = stageThumbnailObj[stageCount].transform.localPosition;

                    //pos.y = h * -loop0 - SCROLLVISW_Y + view.y;
                    //pos.x = w * loop1 + SCROLLVISW_X + view.x;
                    //posにX,Y座標を代入します
                    pos.y = h * -loop0;
                    pos.x = w * loop1;
                    //表示するオブジェクトに座標を代入します
                    stageThumbnailObj[stageCount].transform.localPosition = pos;
                    //表示します
                    stageThumbnailObj[stageCount].SetActive(true);
                    //表示するステージサムネイル数をカウントUpします
                    stageCount++;
                }
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        //すべてのサムネイルを一旦非表示にします
        for (int loop0 = 0; loop0 < stageThumbnailObj.Length; loop0++)
        {
            stageThumbnailObj[loop0].SetActive(false);
        }
        //サムネイル表示数を実行します
        ThumDisplay(SCROLLVISW_MAX_H, SCROLLVISW_MAX_W, stage);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
