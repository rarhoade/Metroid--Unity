﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

    Rigidbody rigid;
    Collider col;
    float moveLeftRightJump;
    public float jumpPower = 12;
    public float sink = -10;
    public float boxSizes = 5;
    bool stillJumped = false;
    bool moveJumped = false;
    private PlayerState playerState;

	// Use this for initialization
	void Start () {
        rigid = GetComponentInParent<Rigidbody>();
        col = this.GetComponent<Collider>();
        playerState = GetComponentInParent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (playerState.IsEnabled())
        {
            Vector3 newVelocity = rigid.velocity;
        
            //Vertical
            if (Input.GetKeyDown(KeyCode.X) && IsGrounded())
            {
                newVelocity.y = jumpPower;
                if (rigid.velocity.x == 0)
                {
                    StartCoroutine(SetStillJumped());
                }
                else
                {
                    StartCoroutine(SetMoveJumped());
                }
            }

            rigid.velocity = newVelocity;
        
            newVelocity = rigid.velocity;
            newVelocity.y = sink;
            if(!Input.GetKey(KeyCode.X) && !IsGrounded())
            {
                rigid.velocity = newVelocity;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!IsGrounded())
        {
            if (moveJumped)
            {
                if(rigid.velocity.x < 0)
                {
                    moveLeftRightJump = -0.5f;
                }
                else if (rigid.velocity.x > 0)
                {
                    moveLeftRightJump = 0.5f;
                }
                else
                {
                    rigid.velocity = new Vector3(moveLeftRightJump, rigid.velocity.y, 0);
                }
            }
            if(Physics.Raycast(transform.position, Vector3.left, 0.5f))
            {
                if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
                }
            }
            else if(Physics.Raycast(transform.position, Vector3.right, 0.5f))
            {
                if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    rigid.velocity = new Vector3(0, rigid.velocity.y, 0);
                }
            }
        }
    }

    public bool IsGrounded()
    {
        Ray ray = new Ray(col.bounds.center, Vector3.down);

        float radius = col.bounds.extents.x - .05f;

        float fullDistance = col.bounds.extents.y + 0.05f;

        if (Physics.SphereCast(ray, radius, fullDistance))
            return true;
        else
            return false;
    }

    public bool IsStillJumped()
    {
        return stillJumped;
    }

    public bool IsMoveJumped()
    {
        return moveJumped;
    }

    IEnumerator SetStillJumped()
    {
        AudioManager.instance.playJump();
        yield return new WaitForSeconds(0.1f);
        stillJumped = true; 
        StartCoroutine(Falling());
    }

    IEnumerator SetMoveJumped()
    {
        AudioManager.instance.playJump();
        yield return new WaitForSeconds(0.1f);
        moveJumped = true;
        StartCoroutine(Falling());
    }

    IEnumerator Falling()
    {
        while(!IsGrounded()) {
            yield return new WaitForEndOfFrame();
        }
        if (stillJumped)
        {
            stillJumped = false;
        }
        else if (moveJumped)
        {
            moveJumped = false;
        }
    }
}
