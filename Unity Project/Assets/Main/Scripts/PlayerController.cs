using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのスクリプトです
/// </summary>

public class PlayerController : MonoBehaviour
{

    //プレイヤーシーンの状態を表示します
    //1 シーン開始演出中
    //2 シーンプレイ中
    //3 ゲームオーバー演出
    public enum PlayerState
    {
        None,
        //シーン開始演出
        Start,
        //シーンプレイ中
        PlayStage,
        //ゲームクリア
        Cliar,
        //ゲームオーバー演出中
        Gameover,
    }
    public PlayerState playerState = PlayerState.None;

    //プレイ中のプレイヤーの状態を表します
    //1 通常状態
    //2 ショットモード
    //3 ジャンプモード
    public enum NowPlayerState
    {
        None,
        //通常状態
        NormalMoad,
        //ショットモード
        ShotMoad,
        //チェインモード
        ChainMoad,
    }
    public NowPlayerState nowPlayerState = NowPlayerState.None;

    //-----------Unityコンポーネント関係----------------

    Rigidbody2D rigidbody;

    // プレハブの中身取得
    [SerializeField]
    private GameObject bulletPrefab;
    // タッチした座標を取得する変数
    private Vector3 touchStartPos;
    // タッチした座標を取得する変数
    private Vector3 touchPos;
    // 離した座標を取得する変数
    private Vector3 touchEndPos;
    //プレイヤーの座標
    //private Vector3 objPos;
    //矢印のオブジェクトを取得
    [SerializeField]
    private GameObject arrowObj;
    [SerializeField]
    private Sprite state0;
    [SerializeField]
    private Sprite state1;

    //----------------クラスの定義-------------------------

    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private BulletController bulletController;
    //---------------数値用変数定義---------------------
    // デフォルト値
    const float DefShotPos = 0.5f;
    const float DefShotSpeed = 25.0f;
    [SerializeField]
    private float moveSpeed = 3;
    //プレイヤーのステータス（状態）を変更させる
    //0 通常状態（白猫投下可）
    //1 チェイン状態
    private int playerstate = 0;
    //------------------フラグ用変数定義---------------

    bool jumpFlag = true;
    //[SerializeField]
    public bool buletIn;
    //public bool BuletIn
    //{
    //    get { return buletIn; }
    //    set { buletIn = BuletIn; }
    //}

    bool shot = false;

    //移動、ジャンプボタンが押されているかの判定
    bool rightMove;
    bool leftMove;

    bool ShotFlag;




    // Use this for initialization
    void Start()
    {
        //Rigidbody2Dを参照します
        rigidbody = GetComponent<Rigidbody2D>();
        //シーンの状態をPlayStageに設定します
        playerState = PlayerState.PlayStage;
        //プレイヤーの状態をノーマルモードに設定します
        nowPlayerState = NowPlayerState.NormalMoad;

        arrowObj.SetActive(false);
        //画面上に弾があるか判定する
        buletIn = false;

    }



    // Update is called once per frame
    void Update()
    {
        PlayMain();



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpFlag = true;
        }
    }

    //----------------関数の定義-----------------------------
    //フリックした際の弾と矢印の処理
    public void Flick()
    {
        // 座標設定用変数
        Vector3 pos;

        // キャラクタ管理のデータ取得
        // 回転量
        Vector3 objRot = transform.eulerAngles;
        // 座標
        Vector3 objPos = transform.localPosition;

        //マウス位置から伸びるレイ（光線）を作成
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //レイと交差するコライダーを検出
        var hit = Physics2D.Raycast(ray.origin, ray.direction, 0);
        //Rayを画面に表示
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.green, 5, false);

        //交差している場合
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hit.collider)
            {
                // Debug.Log("当たっているObj＝　" + hit.collider.name);
                if (hit.collider.tag == "Ground" || hit.collider.tag =="Enemy")
                {
                    ShotFlag = false;
                }
            }
            else
            {

                touchStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchStartPos.z = 0;
                ShotFlag = true;
            }
        }
        if (ShotFlag == true)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchPos.z = 0;
                //ベクトル計算
                Vector3 shoto = touchPos - touchStartPos;
                //単位ベクトル計算
                Vector3 firing = shoto.normalized;
                //矢印の表示
                //左側の処理
                if (shoto.x < 0)
                {
                    arrowObj.transform.localPosition = new Vector3(objPos.x+DefShotPos, objPos.y, objPos.z);
                    var rotate = gameObject.transform.localRotation;
                    rotate = new Quaternion(0, 0, 0, 0);
                    gameObject.transform.localRotation = rotate;
                }
                //右側の処理
                else if (shoto.x > 0)
                {
                    arrowObj.transform.localPosition = new Vector3(objPos.x- DefShotPos, objPos.y, objPos.z);
                    var rotate = gameObject.transform.localRotation;
                    rotate = new Quaternion(0, 180, 0, 0);
                    gameObject.transform.localRotation = rotate;
                }

                arrowObj.SetActive(true);


                // ラジアン
                float radian = Mathf.Atan2(shoto.y, shoto.x);

                // 角度
                float degree = radian * Mathf.Rad2Deg;

                Debug.Log("角度 " + degree);

                arrowObj.transform.localEulerAngles = new Vector3(0, 0, degree + 180.0f);

            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                touchEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                touchEndPos.z = 0;
                arrowObj.SetActive(false);

                if (Mathf.Abs(touchEndPos.x - touchStartPos.x) < 0.2 || Mathf.Abs(touchEndPos.y - touchStartPos.y) < 0.2)
                {
                    shot = false;
                }
                else
                {
                    shot = true;
                }
            }
            
            if (shot == true)
            {
                shot = false;
                buletIn = true;
                //ベクトル計算
                Vector3 shoto = touchEndPos - touchStartPos;
                //単位ベクトル計算
                Vector3 firing = shoto.normalized;

                // ショットの移動ベクトル（加速度）
                Vector3 shotMov =
                    new Vector3(-1.0f * firing.x * DefShotSpeed,
                                -1.0f * firing.y * DefShotSpeed);
                //弾の出る位置
                //弾右側の処理
                if (shotMov.x > 0)
                {
                    // ショットポジション計算（ベクトルから座標へ）
                    pos = new Vector3(objPos.x + DefShotPos,
                                      objPos.y,//+ DefShotPos,
                                      objPos.z);

                    // 実体化
                    GameObject bulletObj =
                        Instantiate(bulletPrefab, pos, transform.rotation);

                    // ここで GameSystemを接続する
                    bulletObj.GetComponent<BulletController>().playerController = this;

                    // 物理演算システム取得
                    Rigidbody2D bulletRigit =
                        bulletObj.GetComponent<Rigidbody2D>();

                    // アドレスチェック
                    if (bulletRigit != null)
                    {
                        // 加速度設定
                        bulletRigit.velocity = shotMov;
                    }
                }
                //弾右側の処理
                else if (shotMov.x < 0)
                {
                    // ショットポジション計算（ベクトルから座標へ）
                    pos = new Vector3(objPos.x + -DefShotPos,
                                      objPos.y, //+ DefShotPos,
                                      objPos.z);

                    // 実体化
                    GameObject bulletObj =
                        Instantiate(bulletPrefab, pos, transform.rotation);

                    // 物理演算システム取得
                    Rigidbody2D bulletRigit =
                        bulletObj.GetComponent<Rigidbody2D>();

                    // アドレスチェック
                    if (bulletRigit != null)
                    {
                        // 加速度設定
                        bulletRigit.velocity = shotMov;
                    }
                }
            }
        }
    }



    //***********移動ボタン処理********************
    public void OnClickDownRight()
    {
        rightMove = true;
        var rotate = gameObject.transform.rotation;
        rotate = new Quaternion(0, 0, 0, 0);
        gameObject.transform.rotation = rotate;
    }
    public void OnClickUpRight()
    {
        rightMove = false;
    }

    public void OnClickDownLeft()
    {
        leftMove = true;
        var rotate = gameObject.transform.rotation;
        rotate = new Quaternion(0, 180, 0, 0);
        gameObject.transform.rotation = rotate;
    }
    public void OnClickUpLeft()
    {
        leftMove = false;
    }


    void PlayMain()
    {
        switch (nowPlayerState)
        {
            case NowPlayerState.NormalMoad:
                gameObject.GetComponent<SpriteRenderer>().sprite = state0;
                var horizontal = Input.GetAxis("Horizontal");
                var velocity = rigidbody.velocity;
                velocity.x = horizontal * moveSpeed;


                Vector3 playerPos = gameObject.transform.localPosition;
                if (rightMove)
                {
                    playerPos.x += moveSpeed * 0.1f;
                }
                if (leftMove)
                {
                    playerPos.x -= moveSpeed * 0.1f;
                }


                gameObject.transform.localPosition = playerPos;

                if (!buletIn)
                {
                    Flick();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {

                }
                rigidbody.velocity = velocity;


                break;
            case NowPlayerState.ChainMoad:
                gameObject.GetComponent<SpriteRenderer>().sprite = state1;
                break;
        }
    }
}
