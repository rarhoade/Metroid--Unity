using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatePinball : MonoBehaviour
{
    PlayerInventory playerInventory;
    PlayerJump playerJump;

    public GameObject Standing;
    public GameObject Morphed;
    public float bufferLookUp = 0.1f;

    private Rigidbody rigid;
    private bool isEnabled = true;
    private bool flying = false;
    private bool standing = true;
    private bool lookingUp = false;
    private bool shooting = false;
    private bool running = false;
    private bool isMissleOn = false;
    private int inRoom = 0;

    private Vector2[] roomMins = { new Vector2(0, 15), new Vector2(78, 15), new Vector2(96, 15), new Vector2(160, 13), new Vector2(176, 120), new Vector2(192, 58), new Vector2(208, 60), new Vector2(112, 150), new Vector2(96, 150) };
    private Vector2[] roomMaxs = { new Vector2(77, 29), new Vector2(95, 29), new Vector2(159, 29), new Vector2(175, 212), new Vector2(191, 135), new Vector2(207, 212), new Vector2(335, 75), new Vector2(159, 165), new Vector2(111, 165) };

    private void Start()
    {
        rigid = this.GetComponent<Rigidbody>();
        playerInventory = this.GetComponent<PlayerInventory>();
        playerJump = this.GetComponentInChildren<PlayerJump>();
        StartCoroutine(ResetFlying());
        StartCoroutine(RunningSound());
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < roomMaxs.Length; i++)
        {
            if (roomMins[i].x < transform.position.x && transform.position.x < roomMaxs[i].x &&
                roomMins[i].y < transform.position.y && transform.position.y < roomMaxs[i].y)
            {
                inRoom = i;
            }
        }
    }

    private void Update()
    {
        if (isEnabled)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isMissleOn && playerInventory.HasMisslePower())
                {
                    Debug.Log("MissleMode is Off");
                    isMissleOn = false;
                }
                else if (!isMissleOn && playerInventory.HasMisslePower())
                {
                    Debug.Log("MissleMode is On");
                    isMissleOn = true;
                }
            }
        }
    }
    // Update is called once per frame
    //TODO update states so that you can only morphball in mid air
    void LateUpdate()
    {

        if (isEnabled)
        {
            playerJump = GetComponentInChildren<PlayerJump>();
            if (standing && Input.GetKeyDown(KeyCode.DownArrow) && playerInventory.HasMorphBall() && (playerJump.IsGrounded() || playerInventory.HasTuckBall()))
            {
                Standing.SetActive(false);
                Morphed.SetActive(true);
                standing = false;
            }

            if (!standing && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.X)))
            {
                if (!Physics.Raycast(transform.GetChild(1).transform.position, Vector3.up, 0.75f))
                {
                    Standing.SetActive(true);
                    Morphed.SetActive(false);
                    StartCoroutine(SetStanding());
                }
            }

            shooting = Input.GetKeyDown(KeyCode.Z);
        }

        running = rigid.velocity.x != 0;
    }

    public bool IsFlying()
    {
        return flying;
    }

    public void SendFlying()
    {
        flying = true;
    }

    public bool IsStanding()
    {
        return standing;
    }

    public bool IsLookingUp()
    {
        return lookingUp;
    }

    public bool IsShooting()
    {
        return shooting;
    }

    public bool IsRunning()
    {
        return running;
    }

    public bool IsMissleOn()
    {
        return isMissleOn;
    }

    public void SetLookingUp(bool l)
    {
        lookingUp = l;
    }

    public int whichRoom()
    {
        return inRoom;
    }

    public bool IsEnabled()
    {
        return isEnabled;
    }

    public void SetEnabled(bool en)
    {
        isEnabled = en;
    }

    IEnumerator SetStanding()
    {
        yield return new WaitForSeconds(bufferLookUp);
        standing = true;
    }

    IEnumerator ResetFlying()
    {
        while (true)
        {
            if (flying)
            {
                playerJump = GetComponentInChildren<PlayerJump>();
                yield return new WaitForSeconds(0.2f);
                while (!playerJump.IsGrounded())
                {
                    yield return new WaitForFixedUpdate();
                }
                flying = false;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator RunningSound()
    {
        while (true)
        {
            if (isEnabled && IsRunning() && playerJump.IsGrounded())
            {
                AudioManager.instance.playSamusRunning();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
