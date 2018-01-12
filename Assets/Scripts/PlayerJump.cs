using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    Rigidbody rigid;
    Collider col;

    public float jumpPower = 12;
    public float sink = -10;

	// Use this for initialization
	void Awake () {
        rigid = GetComponentInParent<Rigidbody>();
        col = this.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newVelocity = rigid.velocity;

        //Vertical
        if (Input.GetKeyDown(KeyCode.Z) && IsGrounded())
        {
            newVelocity.y = jumpPower;
        }

        rigid.velocity = newVelocity;
        
        newVelocity = rigid.velocity;
        newVelocity.y = sink;
        if(!Input.GetKey(KeyCode.Z) && !IsGrounded())
        {
            rigid.velocity = newVelocity;
        }
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(col.bounds.center, Vector3.down);

        float radius = col.bounds.extents.x - .05f;

        float fullDistance = col.bounds.extents.y + 0.5f;

        if (Physics.SphereCast(ray, radius, fullDistance))
            return true;
        else
            return false;
    }
}
