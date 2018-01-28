using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour {

    Rigidbody rigid;

    public float maxMoveSpeed = 5;
    private static bool slowed = false;

    private PlayerState playerState;
    private PlayerJump playerJump;

	// Use this for initialization
	void Awake  () {
        rigid = this.GetComponent<Rigidbody>();
        playerJump = this.GetComponentInChildren<PlayerJump>();
        playerState = this.GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update () {

        if (playerState.IsEnabled())
        {
            float moveSpeed = maxMoveSpeed;
            if (slowed)
            {
                moveSpeed /= 2;
            }
            playerJump = this.GetComponentInChildren<PlayerJump>();
            Vector3 newVelocity = rigid.velocity;

            //Horizontal
            float vel = Input.GetAxis("Horizontal") * moveSpeed;
            if (Mathf.RoundToInt(vel) != 0)
            {
                newVelocity.x += vel;
                if (vel > 0 && newVelocity.x > moveSpeed)
                {
                    newVelocity.x = moveSpeed;
                }
                else if (vel < 0 && newVelocity.x < -1*moveSpeed)
                {
                    newVelocity.x = -moveSpeed;
                }
                rigid.velocity = newVelocity;
            }
            else if ((playerJump.IsGrounded() && !playerState.IsFlying()) || (!playerJump.IsGrounded() && playerJump.IsStillJumped()))
            {
                rigid.velocity = new Vector3(0, rigid.velocity.y);
            }
        }

    }

    public static void slowDown()
    {
        slowed = true;
    }
    public static void speedUp()
    {
        slowed = false;
    }
}
