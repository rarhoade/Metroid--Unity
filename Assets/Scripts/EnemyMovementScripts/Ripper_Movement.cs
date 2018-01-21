using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripper_Movement : MonoBehaviour {

    Rigidbody rigid;
    private SpriteRenderer spriteR;

    private float speed = 2.0f;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        spriteR = GetComponent<SpriteRenderer>();
        rigid.velocity = Vector3.left * 2;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(rigid.velocity);
	}

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.collider.gameObject.layer == 0)
        {
            Debug.Log(rigid.velocity);
            Debug.Log(rigid.velocity);
            if (!spriteR.flipX)
            {
                spriteR.flipX = true;
                rigid.velocity = Vector3.right * speed;
                //this.transform.position += rigid.velocity * 10f;
            }
            else
            {
                spriteR.flipX = false;
                rigid.velocity = Vector3.left * speed;
                //this.transform.position += rigid.velocity * 10f;
            }
        }
    }

}
