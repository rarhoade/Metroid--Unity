using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour {

    public float animTime;
    public float shootingWindow;
    public GameObject morphedForm;
    public Sprite[] runningRight;
    public Sprite[] morphRun;
    public Sprite[] shootingRight;
    public Sprite[] shootingUp;
    public Sprite stillJumpRight;
    public Sprite jumpShootRight;
    public Sprite jumpShootUp;
    public Sprite[] jumpSpin;


    private PlayerJump playerJump;
    private PlayerDirection playerDirection;
    private PlayerState playerState;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer morphedRenderer;

    private bool shotInAir = false;
    private float timeSinceLastShot = 0f;

    private bool spinMove = false;
    private int spinInt = 0;

    private bool running = false;
    private int runningInt = 0;

    private bool morphed = false;
    private int morphInt = 0;
   
    // Use this for initialization
    void Start()
    {
        playerDirection = this.GetComponent<PlayerDirection>();
        playerState = this.GetComponent<PlayerState>();
        playerJump = this.GetComponentInChildren<PlayerJump>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        morphedRenderer = morphedForm.GetComponent<SpriteRenderer>();
        StartCoroutine(RunForestRun());
        StartCoroutine(SpinMove());
        StartCoroutine(SonicRun());
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (playerState.IsShooting())
        {
            timeSinceLastShot = 0f;
        }
        else
        {
            timeSinceLastShot += Time.deltaTime;
        }
        if (playerState.IsStanding()) //Standing Form
        {
            morphed = false;
            if (playerJump.IsGrounded()) //On Ground
            {
                shotInAir = false;
                spinMove = false;
                running = playerState.IsRunning();
                if (playerDirection.IsHoldingUp()) //Is holding up key
                {
                    playerState.SetLookingUp(true);
                }
                else //isnt holding up key
                {
                    playerState.SetLookingUp(false);
                }
            }
            else //In Air
            {
                running = false;
                if (playerState.IsShooting()) //is shooting
                {
                    shotInAir = true;
                }
                if (shotInAir)
                {
                    if (playerDirection.IsHoldingUp()) //is holding up key
                    {
                        playerState.SetLookingUp(true);
                        spriteRenderer.sprite = jumpShootUp;
                    }
                    else //isnt holding up key
                    {
                        playerState.SetLookingUp(false);
                        spriteRenderer.sprite = jumpShootRight;
                    }
                    spinMove = false;
                }
                else if (playerJump.IsStillJumped()) //jumped from stationary
                {
                    spriteRenderer.sprite = stillJumpRight;
                    spinMove = false;
                }
                else if (playerJump.IsMoveJumped()) //is beyblade
                {
                    spinMove = true;
                }
            }
        }
        else //morph ball
        {
            morphed = true;
        }


        
    }

    IEnumerator RunForestRun()
    {
        while(true)
        {
            if (running)
            {
                if (playerDirection.IsHoldingUp())
                {
                    spriteRenderer.sprite = shootingUp[(runningInt % (shootingUp.Length - 1)) + 1];
                }
                else if (timeSinceLastShot <= shootingWindow)
                {
                    spriteRenderer.sprite = shootingRight[(runningInt % (shootingRight.Length - 1)) + 1];
                } 
                else
                {
                    spriteRenderer.sprite = runningRight[(runningInt % (runningRight.Length - 1)) + 1];
                }
                runningInt++;
                yield return new WaitForSeconds(animTime);
            }
            else
            {
                runningInt = 0;
                if (playerJump.IsGrounded())
                {
                    if (playerDirection.IsHoldingUp())
                    {
                        spriteRenderer.sprite = shootingUp[runningInt];
                    }
                    else
                    {
                        spriteRenderer.sprite = runningRight[runningInt];
                    }
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }

    //Spinning Coroutine
    IEnumerator SpinMove()
    {
        while(true)
        {
            if (spinMove)
            {
                spriteRenderer.sprite = jumpSpin[spinInt % jumpSpin.Length];
                spinInt++;
                yield return new WaitForSeconds(animTime);
            }
            else
            {
                spinInt = 0;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    IEnumerator SonicRun()
    {
        while(true)
        {
            if (morphed)
            {
                morphedRenderer.sprite = morphRun[morphInt % morphRun.Length];
                if (playerState.IsRunning())
                {
                    morphInt++;
                }
                yield return new WaitForSeconds(animTime);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
