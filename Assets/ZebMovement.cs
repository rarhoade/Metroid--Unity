using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebMovement : MonoBehaviour {

    public GameObject[] playerFound;
    public Rigidbody rigid;
    public float zebSpeed = 1.0f;
    bool playerRight;
    bool switchState = false;
	// Use this for initialization
	void Start () {
        Debug.Log("this is executing");
        playerFound = GameObject.FindGameObjectsWithTag("Player");
        
        rigid = GetComponent<Rigidbody>();
        rigid.velocity = Vector3.up * zebSpeed;
        Debug.Log(playerFound[0].name);
        if(playerFound[0].transform.GetChild(0).transform.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            playerRight = true;
        }
        else
        {
            playerRight = false;
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log(rigid.velocity);
		if(!(this.transform.position.y < playerFound[0].transform.GetChild(0).transform.position.y) && !switchState){
            if (playerRight)
            {
                rigid.velocity = Vector3.right * zebSpeed; 
            }
            else
            {
                rigid.velocity = Vector3.left * zebSpeed;
            }
            switchState = true;
        }
        else if (switchState)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            if (!GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
