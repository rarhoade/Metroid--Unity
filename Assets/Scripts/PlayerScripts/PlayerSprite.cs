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

    public Sprite[] missleRunningRight;
    public Sprite[] missleMorphRun;
    public Sprite[] missleShootingRight;
    public Sprite[] missleShootingUp;
    public Sprite missleStillJumpRight;
    public Sprite missleJumpShootRight;
    public Sprite missleJumpShootUp;
    public Sprite[] missleJumpSpin;



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

    private Coroutine run;
    private Coroutine spin;
    private Coroutine sonic;
   
    // Use this for initialization
    void Awake()
    {
        Awaken();
    }

    public void Awaken()
    {
        playerDirection = this.GetComponent<PlayerDirection>();
        playerState = this.GetComponent<PlayerState>();
        playerJump = this.GetComponentInChildren<PlayerJump>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        morphedRenderer = morphedForm.GetComponent<SpriteRenderer>();
        if (run == null)
            run = StartCoroutine(RunForestRun());
        if (spin == null)
            spin = StartCoroutine(SpinMove());
        if (sonic == null)
            sonic = StartCoroutine(SonicRun());

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
                        if (playerState.IsMissleOn())
                        {
                            spriteRenderer.sprite = missleJumpShootUp;
                        }
                        else
                        {
                            spriteRenderer.sprite = jumpShootUp;
                        }
                    }
                    else //isnt holding up key
                    {
                        playerState.SetLookingUp(false);

                        if (playerState.IsMissleOn())
                        {
                            spriteRenderer.sprite = missleJumpShootRight;
                        }
                        else
                        {
                            spriteRenderer.sprite = jumpShootRight;
                        }
                    }
                    spinMove = false;
                }
                else if (playerJump.IsStillJumped()) //jumped from stationary
                {
                    if (playerState.IsMissleOn())
                    {
                        spriteRenderer.sprite = missleStillJumpRight;
                    }
                    else
                    {
                        spriteRenderer.sprite = stillJumpRight;
                    }

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
                    if (playerState.IsMissleOn())
                    {
                        spriteRenderer.sprite = missleShootingUp[(runningInt % (shootingUp.Length - 1)) + 1];
                    }
                    else
                    {
                        spriteRenderer.sprite = shootingUp[(runningInt % (shootingUp.Length - 1)) + 1];
                    }
                }
                else if (timeSinceLastShot <= shootingWindow)
                {
                    if (playerState.IsMissleOn())
                    {
                        spriteRenderer.sprite = missleShootingRight[(runningInt % (shootingRight.Length - 1)) + 1];
                    }
                    else
                    {
                        spriteRenderer.sprite = shootingRight[(runningInt % (shootingRight.Length - 1)) + 1];
                    }
                } 
                else
                {
                    if (playerState.IsMissleOn())
                    {
                        spriteRenderer.sprite = missleRunningRight[(runningInt % (runningRight.Length - 1)) + 1];
                    }
                    else
                    {
                        spriteRenderer.sprite = runningRight[(runningInt % (runningRight.Length - 1)) + 1];
                    }
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
                        if (playerState.IsMissleOn())
                        {
                            spriteRenderer.sprite = missleShootingUp[runningInt];
                        }
                        else
                        {
                            spriteRenderer.sprite = shootingUp[runningInt];
                        }
                    }
                    else
                    {
                        if (playerState.IsMissleOn())
                        {
                            spriteRenderer.sprite = missleRunningRight[runningInt];
                        }
                        else
                        {
                            spriteRenderer.sprite = runningRight[runningInt];
                        }
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
                if (playerState.IsMissleOn())
                {
                    spriteRenderer.sprite = missleJumpSpin[spinInt % jumpSpin.Length];
                }
                else
                {
                    spriteRenderer.sprite = jumpSpin[spinInt % jumpSpin.Length];
                }
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
                if (playerState.IsMissleOn())
                {
                    morphedRenderer.sprite = missleMorphRun[morphInt % morphRun.Length];
                }
                else
                {
                    morphedRenderer.sprite = morphRun[morphInt % morphRun.Length];
                }
                morphInt++;
                yield return new WaitForSeconds(animTime);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
