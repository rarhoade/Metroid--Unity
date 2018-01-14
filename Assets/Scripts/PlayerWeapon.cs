using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    PlayerDirection playerDirection;
    PlayerInventory playerInventory;

    public GameObject bulletPrefab;
    public GameObject longBulletPrefab;

    public Transform firingPositionForward;
    public Transform firingPositionUpward;

    public float firingSpeed = 10f;

	// Use this for initialization
	void Awake () {
        playerDirection = GetComponentInParent<PlayerDirection>();
        playerInventory = GetComponentInParent<PlayerInventory>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject bulletInstance;
            if (playerInventory.HasLongShot())
            {
                bulletInstance = GameObject.Instantiate(longBulletPrefab);
            }
            else
            {
                bulletInstance = GameObject.Instantiate(bulletPrefab);
            }

            if (playerDirection.IsLookingUp())
            {
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
                    bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.left * firingSpeed;
                }
            }
        }
	}
}
