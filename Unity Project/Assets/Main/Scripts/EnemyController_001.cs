using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController_001 : MonoBehaviour {

    Rigidbody2D rigidbody;
    //public GameObject enemy;
    // Use this for initialization
    void Start () {
        //enemy = GameObject.Find("Enemy");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.CompareTag("Bullet"))
        {
            rigidbody = gameObject.GetComponent<Rigidbody2D>();
            rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;

        }

    }



    
}


    
