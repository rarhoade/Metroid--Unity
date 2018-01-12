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

    /*bool IsGrounded()
    {
        Collider col = this.GetComponentInChildren<Collider>();

        Ray ray = new Ray(col.bounds.center, Vector3.down);

        float radius = col.bounds.extents.x - .05f;

        float fullDistance = col.bounds.extents.y + 0.5f;

        if (Physics.SphereCast(ray, radius, fullDistance))
            return true;
        else
            return false;
    }*/
}
