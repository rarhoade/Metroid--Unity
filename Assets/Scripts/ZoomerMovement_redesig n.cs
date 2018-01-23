using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomerMovement_redesign : MonoBehaviour {


	Rigidbody rigid;
	Vector3 startingDirection;
	float zoomerSpeed;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
		rigid.velocity = startingDirection * zoomerSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void fixedUpdate(){

	}

	void RaycastCheck(){

	}
}
