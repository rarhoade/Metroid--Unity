using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour {

    private PlayerState playerState;

    private bool holdingUp = false;
    private bool facingRight = true;

    private void Awake()
    {
        playerState = this.GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (playerState.IsEnabled())
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
            holdingUp = Input.GetKey(KeyCode.UpArrow);
        }

	}

    public bool IsFacingRight()
    {
        return facingRight;
    }
    
    public bool IsHoldingUp()
    {
        return holdingUp;
    }
}
