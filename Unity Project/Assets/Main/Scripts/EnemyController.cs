using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {


    //エネミーステータスの状態を表示します
    //1 通常時
    //2 結晶時
    //3 破壊時
    public enum EnemyState
    {
        None,
        //通常時
        Normal,
        //結晶化
        Crystallization,
        //破壊時
        Break,
        //爆発後
        BreakAfter,
    }
    public EnemyState enemyState = EnemyState.None;

    //--------------Unityコンポーネント関係---------------
    //敵の状態によっての画像の変更
    SpriteRenderer Enemysprite;

    //通常状態の敵の画像
    [SerializeField]
    private Sprite Enemynormal;

    //結晶化状態の画像
    [SerializeField]
    private Sprite Crystalsprite;

    //爆発状態の画像
    [SerializeField]
    private Sprite Bombsprite;

    Rigidbody2D rigidbody;
    BoxCollider2D BoxCollider;
    CircleCollider2D CircleCollider;

    //エネミーの移動
    private Vector2 Moveenemy;

    //爆発範囲オブジェクト
    [SerializeField]
    private GameObject Bomb_erea;


    //--------------------クラス定義--------------------------
    //爆発のチェインするための配列
    [SerializeField]
    private List<GameObject> Bombchain = new List<GameObject>();


    //----------------数値用変数定義------------------------------
    //敵の状態判定のための変数の宣言と初期化
    [SerializeField]
    private int Enemystatus = 0;

    //結晶化してからの時間を入れる変数
    private float Freezetime;

    //動き出すまでの時間を設定する変数
    [SerializeField]
    private float Restarttime;

    //エネミーの右方向への折り返し位置
    [SerializeField]
    private float Turnpos = 0;

    //エネミーの左方向への折り返し位置
    [SerializeField]
    private float Returnpos = 0;

    //エネミーの次の弾を撃つまでの時間のカウント
    private float Shottime = 0;

    //エネミーの弾を打つ間隔
    [SerializeField]
    private float span = 0;

    //爆発範囲の拡大可能時間
    float Taptime = 0;

    [SerializeField]
    private float Bombcount = 0;

    //----------------フラグ用変数定義--------------------------
    //攻撃のON/OFF
    public bool Attack = false;

    //爆発範囲を拡大するときの判定
    bool Bomb_circle = false;

    bool Bombstart = false;

   

    // Use this for initialization
    void Start()
    {
        enemyState = EnemyState.Normal;

        Enemysprite = gameObject.GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        //Moveenemy = Vector2.left;       //左方向への移動
        BoxCollider = GetComponent<BoxCollider2D>();
        CircleCollider = GetComponent<CircleCollider2D>();
        GetComponent<CircleCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            //通常状態の敵
            case EnemyState.Normal:
                //一度結晶化した後、また動き出す
                rigidbody.constraints = RigidbodyConstraints2D.None;
                //上のスクリプトで外れたRotationをオンにする
                rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                //折り返し位置で折り返す
                if (transform.position.x < Turnpos)
                {
                   // Moveenemy = Vector2.right;
                }
                //移動開始位置で折り返す
                else if (transform.position.x > Returnpos)
                {
                   // Moveenemy = Vector2.left;
                }
                //エネミーの移動
                //transform.Translate(Moveenemy * 0.03f);
                //通常状態の画像に切り替え
                Enemysprite.sprite = Enemynormal;
                if (Attack == true)
                {
                    //攻撃のスクリプトをtrueに
                    //GetComponent<Enemy_shotcontroller>().enabled = true;
                    //攻撃するまでの時間計測
                    Shottime += Time.deltaTime;
                    //プレイヤー方向に攻撃
                    if (Shottime >= span)
                    {
                        Shottime = 0;
                    }
                }
                CircleCollider.radius = 0.3f;
                break;
            //結晶化状態の敵
            case EnemyState.Crystallization:
                //結晶化すると動きを止める
                rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
                //結晶化の画像に変更
                Enemysprite.sprite = Crystalsprite;
                //攻撃スクリプトの停止
                //GetComponent<Enemy_shotcontroller>().enabled = false;
                //結晶化してからの時間計測
                Freezetime += Time.deltaTime;
                //結晶化してからの時間が、Restarttimeを超えると通常状態に戻る
                if (Freezetime >= Restarttime)
                {
                    enemyState = EnemyState.Normal;
                }

                if (Bomb_circle == true)
                {
                    Taptime += Time.deltaTime;
                    if (Taptime >= 4f)
                    {
                        enemyState = EnemyState.Break;
                        GetComponent<CircleCollider2D>().enabled = true;
                    }
                    CircleCollider.radius += 0.025f;
                    Bomb_erea.transform.localScale += new Vector3(0.1f, 0.05f, 0);
                }
                break;
            //爆発状態の敵
            case EnemyState.Break:
                Enemysprite.sprite = Bombsprite;
                Bombcount += Time.deltaTime;
                int i = 0;
                if (Bombcount >= 3 && i <= Bombchain.Count)
                {
                    enemyState = EnemyState.BreakAfter;
                    i++;
                }
                break;
            //爆発後
            case EnemyState.BreakAfter:
                Enemysprite.enabled = false;
                Bomb_erea.SetActive(false);
                break;
        }


    }


    //プレイヤーに当たると結晶化
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            enemyState = EnemyState.Crystallization;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (Enemystatus == 1 &&collision.tag=="Enemy")
        {
            Bombchain.Add(collision.gameObject);
            enemyState = EnemyState.Break;
        }
    }
    public void OnDown()
    {
        Bomb_circle = true;
        Bombstart = true;
    }
    public void OnUp()
    {
        enemyState = EnemyState.Break;
        Bombchain.Add(gameObject);
        Bomb_circle = false;
        GetComponent<CircleCollider2D>().enabled = true;
    }
}

