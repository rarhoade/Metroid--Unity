using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

    public Sprite spriteLookingForward;
    public Sprite spriteLookingUpward;

    bool lookingUp = false;
    bool facingRight = true;
    bool firstTime = true;

    private PlayerState playerState;
    private PlayerJump playerJump;

    private void Start()
    {
        playerState = this.GetComponent<PlayerState>();
        playerJump = GetComponentInChildren<PlayerJump>();
    }

    // Update is called once per frame
    void Update ()
    {
        // Horizontal
        float horizontalAxis = Input.GetAxis("Horizontal");
        if(facingRight && horizontalAxis < 0)
        {
            facingRight = false;
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (!facingRight && horizontalAxis > 0)
        {
            facingRight = true;
            this.transform.localScale = new Vector3(1, 1, 1);
        }

        // Vertical
        bool holdingUp = Input.GetKey(KeyCode.UpArrow);
        if (lookingUp && !holdingUp && playerState.isStanding())
        {
            lookingUp = false;
            if (!playerJump.IsStillJumped())
            {
                spriteRenderer.sprite = spriteLookingForward;
            }
        }
        else if(!lookingUp && holdingUp && playerState.isStanding())
        {
            lookingUp = true;
            if (!playerJump.IsStillJumped())
            {
                spriteRenderer.sprite = spriteLookingUpward;
            }
        }
	}

    public bool IsFacingRight()
    {
        return facingRight;
    }

    public bool IsLookingUp()
    {
        return lookingUp;
    }
}
