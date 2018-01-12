using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

    public Sprite spriteLookingForward;
    public Sprite spriteLookingUpward;

    bool lookingUp = false;
    bool facingRight = true;

    private PlayerState playerState;

    private void Start()
    {
        playerState = this.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update () {
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
            spriteRenderer.sprite = spriteLookingForward;
        }
        else if(!lookingUp && holdingUp && playerState.isStanding())
        {
            lookingUp = true;
            spriteRenderer.sprite = spriteLookingUpward;
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
