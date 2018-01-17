using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour {

    Rigidbody rigid;

    public float moveSpeed = 5;

	// Use this for initialization
	void Awake  () {
        rigid = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newVelocity = rigid.velocity;

        //Horizontal
        newVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;

        rigid.velocity = newVelocity;
	}
}
