using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    public GameObject bulletPrefab;
    public GameObject longBulletPrefab;
    public GameObject misslePrefab;

    public Transform firingPositionForward;
    public Transform firingPositionUpward;

    public float firingSpeed = 10f;

    private PlayerState playerState;
    private PlayerDirection playerDirection;
    private PlayerInventory playerInventory;

    // Use this for initialization
    void Start () {
        playerDirection = GetComponentInParent<PlayerDirection>();
        playerInventory = GetComponentInParent<PlayerInventory>();
        playerState = GetComponentInParent<PlayerState>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerState.IsShooting())
        {
            GameObject bulletInstance = null;
            if(playerState.IsMissleOn() && (playerInventory.HasNumMissles() || playerInventory.missleCount.text == "99"))
            {
                if (playerInventory.missleCount.text != "99")
                {
                    playerInventory.subtractMissles();
                }
                bulletInstance = GameObject.Instantiate(misslePrefab);
            }
            else if (playerInventory.HasLongShot() && !playerState.IsMissleOn())
            {
                bulletInstance = GameObject.Instantiate(longBulletPrefab);
            }
            else if(!playerState.IsMissleOn())
            {
                bulletInstance = GameObject.Instantiate(bulletPrefab);
            }

            if (playerState.IsLookingUp())
            {
                bulletInstance.transform.Rotate(Vector3.forward * 90);
                bulletInstance.transform.position = firingPositionUpward.position;
                bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.up * firingSpeed;
            }
            else
            {
                bulletInstance.transform.position = firingPositionForward.position;
                if (playerDirection.IsFacingRight())
                {
                    bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.right * firingSpeed;
                }
                else
                {
                    bulletInstance.GetComponent<SpriteRenderer>().flipX = true;
                    bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.left * firingSpeed;
                }
            }
        }
	}
}
