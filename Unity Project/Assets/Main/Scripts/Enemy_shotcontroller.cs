using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_shotcontroller : MonoBehaviour
{
    const float shotTimeMax = 3.0f;
    float countTime = 0.0f;
    const float DefShotSpeed = 5.0f;
    public GameObject player;
    Vector3 playerPos;

    void Start()
    {
        /*target = GameObject.FindWithTag("Player").transform;
        GetComponent<Rigidbody2D>().velocity = target.transform.position * speed;
        Destroy(gameObject, 5.0f);*/
        player = GameObject.Find("player");
        playerPos = player.transform.localPosition;
        countTime = shotTimeMax;
    }

    void Update()
    {
        countTime -= Time.deltaTime;
        if (countTime > 0.0f)
        {
            return;
        }
        countTime = shotTimeMax;

        Vector3 shotVec = playerPos - gameObject.transform.localPosition;

        Vector3 shotVecE = shotVec.normalized;

        shotVec = shotVecE * DefShotSpeed;

        GameObject prefab = (GameObject)Resources.Load("Bullet");

        Vector3 shotPos = gameObject.transform.localPosition;
        shotPos += new Vector3(shotVecE.x , shotVecE.y, 0);

        GameObject bulletObj = Instantiate(prefab, shotPos, transform.rotation);
        //加速度設定
        Rigidbody2D bulletRight =
            bulletObj.GetComponent<Rigidbody2D>();
        if (bulletRight != null)
        {
            //加速度設定
            bulletRight.velocity = shotVec;
        }
    }
}


