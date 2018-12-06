using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    // タッチした座標を取得する変数
    private Vector3 touchStartPos;
    // 離した座標を取得する変数
    private Vector3 touchEndPos;

    // 座標を取得する関数
    void Flick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            touchStartPos = new Vector3(Input.mousePosition.x,
                                        Input.mousePosition.y,
                                        Input.mousePosition.z);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            touchEndPos = new Vector3(Input.mousePosition.x,
                                      Input.mousePosition.y,
                                      Input.mousePosition.z);
            GetDirection();
        }
    }

    void GetDirection()
    {
        float directionX = touchEndPos.x - touchStartPos.x;
        float directionY = touchEndPos.y - touchStartPos.y;
        string Direction = null;

        if (Mathf.Abs(directionY) < Mathf.Abs(directionX))
        {
            if (30 < directionX)
            {
                //右向きにフリック
                Direction = "right";
            }
            else if (-30 > directionX)
            {
                //左向きにフリック
                Direction = "left";
            }
        }
        else if (Mathf.Abs(directionX) < Mathf.Abs(directionY)){
            if (30 < directionY)
            {
                //上向きにフリック
                Direction = "up";
            }
            else if (-30 > directionY)
            {
                //下向きのフリック
                Direction = "down";
            }
        }
        else
        {
            //タッチを検出
            Direction = "touch";
        }


        switch (Direction)
        {
            case "up":
                Debug.Log("上");
                //上フリックされた時の処理
                break;

            case "down":
                Debug.Log("下");
                //下フリックされた時の処理
                break;

            case "right":
                Debug.Log("右");
                //右フリックされた時の処理
                break;

            case "left":
                Debug.Log("左");
                //左フリックされた時の処理
                break;

            case "touch":
                Debug.Log("タッチされてます");
                //タッチされた時の処理
                break;
        }



    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Flick();
    }
}
