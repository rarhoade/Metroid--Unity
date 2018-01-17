using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    PlayerInventory playerInventory;
    PlayerJump playerJump;

    public GameObject Standing;
    public GameObject Morphed;
    public float bufferLookUp = 0.1f;

    private Rigidbody rigid;
    private bool standing = true;
    private bool lookingUp = false;
    private bool shooting = false;
    private bool running = false;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
        playerInventory = this.GetComponent<PlayerInventory>();
        playerJump = this.GetComponentInChildren<PlayerJump>();
    }

    // Update is called once per frame
    //TODO update states so that you can only morphball in mid air
    void LateUpdate()
    {
        if (standing && Input.GetKeyDown(KeyCode.DownArrow) && playerInventory.HasMorphBall() && playerJump.IsGrounded())
        {
            Standing.SetActive(false);
            Morphed.SetActive(true);
            standing = false;
        }

        if (!standing && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.X)))
        {
            Standing.SetActive(true);
            Morphed.SetActive(false);
            StartCoroutine(SetStanding());
        }
        
        shooting = Input.GetKeyDown(KeyCode.Z);

        running = rigid.velocity.x != 0;
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

    public void SetLookingUp(bool l)
    {
        lookingUp = l;
    }

    IEnumerator SetStanding()
    {
        yield return new WaitForSeconds(bufferLookUp);
        standing = true;
    }
}
