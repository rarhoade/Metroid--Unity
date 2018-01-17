using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkreeMovement : MonoBehaviour {

    public Rigidbody rigid;

    public Vector3 verticalReach = new Vector3(0, -13, 0);
    public float rad = 5;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //going to need to use a layermask with the playerLayer in order to try and find them
        Collider[] foundObj = Physics.OverlapCapsule(transform.position, transform.position - verticalReach, rad);
        if (Physics.OverlapCapsule(transform.position, transform.position - verticalReach, rad) != null)
        {
            if()
            Debug.Log("activated");
        }    
    }
}
