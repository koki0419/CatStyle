using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float moveSpeed = 0.1f;
    new Rigidbody2D rigidbody;
    public new Vector3 bulletPos;

    //弾のバウンド回数
    int bulletBound;



    public PlayerController playerController;
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        bulletBound = 0;
    }

    // Update is called once per frame
    void Update()
    {
 
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        bulletBound++;
        if (bulletBound >= 10)
        {
            Destroy(this.gameObject);
            playerController.buletIn = false;
            bulletBound = 0;
        }

        //if (collision.gameObject.CompareTag("Ground"))
        //{
        //    bulletBound++;
        //    if (bulletBound >= 5)
        //    {
        //        Destroy(this.gameObject);
        //        playerController.buletIn = false;
        //        bulletBound = 0;
        //    }

        //}
        if (collision.gameObject.CompareTag("Enemy"))
        {



        }
    }
}

