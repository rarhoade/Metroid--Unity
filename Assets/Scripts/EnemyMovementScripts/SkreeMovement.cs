using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkreeMovement : MonoBehaviour {

    public Rigidbody rigid;

    public Vector3 verticalReach = new Vector3(0, -13, 0);
    public float rad = 5;
    public Vector3 downVel;
    public float xVel = 0;
    public float yVel = -2;
    int layerMask = 1 << 8;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        downVel = new Vector3(xVel, yVel, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //going to need to use a layermask with the playerLayer in order to try and find them
        Collider[] foundObj = Physics.OverlapCapsule(transform.localPosition, transform.localPosition - verticalReach, rad, layerMask);
        if (foundObj != null)
        {
            foreach(Collider obj in foundObj)
            {
                //Debug.Log(obj.gameObject.name);
                if (obj.gameObject.name == "Standing" || obj.gameObject.name == "Morphed")
                {
                    //Debug.Log("player is detected");
                    //implement tracking slope
                    //If our current position is behind the player's x position move to the right
                    if((transform.position.x - obj.gameObject.transform.position.x) < 0)
                    {
                        downVel.x = xVel;
                    }
                    else if((transform.position.x - obj.gameObject.transform.position.y) > 0)
                    {
                        downVel.x = -xVel;
                    }
                    else
                    {
                        downVel.x = 0;
                    }
                    rigid.velocity = downVel;
                }
            }
        }    
    }
}
