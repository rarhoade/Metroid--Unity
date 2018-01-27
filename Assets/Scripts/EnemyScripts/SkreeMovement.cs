using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkreeMovement : MonoBehaviour {

    public Rigidbody rigid;

    public Vector3 verticalReach = new Vector3(5, 1, 0);
    bool hasFallen;
    public float rad = 5;
    public Vector3 downVel;
    public float xVel = 0;
    public float yVel = -2;
    int layerMask = 1 << 8;
    GameObject hitObj = null;

    private bool hasCollided = false;
    public UnitHealth thisSkree;
    Animator anim;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        downVel = new Vector3(xVel, yVel, 0);
        anim = GetComponent<Animator>();
        thisSkree = GetComponent<UnitHealth>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        //going to need to use a layermask with the playerLayer in order to try and find them
        RaycastHit collidedLeft;
        RaycastHit collidedRight;
        RaycastHit collidedCenter;
        bool foundObjLeft = Physics.Raycast(transform.position, Vector3.down + Vector3.left/2, out collidedLeft, 13f);
        Debug.DrawRay(transform.position, (Vector3.down + Vector3.left/2)*13f, Color.red);
        bool foundObjRight = Physics.Raycast(transform.position, Vector3.down + Vector3.right/2, out collidedRight, 13f);
        Debug.DrawRay(transform.position, (Vector3.down + Vector3.right/2) * 13f, Color.red);
        bool foundObjCenter = Physics.Raycast(transform.position, Vector3.down, out collidedCenter, 13f);
        Debug.DrawRay(transform.position, (Vector3.down) * 13f, Color.red);
        if ((foundObjLeft || foundObjRight || foundObjCenter) && !hasFallen)
        {
            
            if (foundObjLeft && collidedLeft.collider.gameObject.layer == 8)
            {
                hitObj = collidedLeft.collider.gameObject;
            }
            else if(foundObjRight && collidedRight.collider.gameObject.layer == 8)
            {
                hitObj = collidedRight.collider.gameObject;
            }
            else if(foundObjCenter && collidedCenter.collider.gameObject.layer == 8)
            {
                hitObj = collidedCenter.collider.gameObject;
            }
            if(hitObj != null)
            {
                //Debug.Log(obj.gameObject.name);
                if (hitObj.name == "Standing" || hitObj.name == "Morphed")
                {
                    //Debug.Log("player is detected");
                    //implement tracking slope
                    //If our current position is behind the player's x position move to the right
                    if ((transform.position.x - hitObj.transform.position.x) < 0)
                    {
                        downVel.x = xVel;
                    }
                    else if((transform.position.x - hitObj.transform.position.y) > 0)
                    {
                        downVel.x = -xVel;
                    }
                    else
                    {
                        downVel.x = 0;
                    }
                    rigid.velocity = downVel;
                    hasFallen = true;
                    anim.SetTrigger("Falling");
                }
            }
        }
        else if (hasFallen)
        {
            if (!thisSkree.hasPaused())
            {
                if ((transform.position.x - hitObj.transform.position.x) < 0)
                {
                    downVel.x = xVel;
                }
                else if ((transform.position.x - hitObj.transform.position.y) > 0)
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
        else if (hasCollided)
        {
            downVel.x = 0;
            rigid.velocity = downVel;
        }
    }

    private void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.name == "Standing" || obj.gameObject.name == "Morphed")
        {
            hasCollided = true;
            //Debug.Log("Collided");
        }
        else if (obj.gameObject.layer == 0)
        {
            anim.SetTrigger("Hit_Floor");
        }
    }

    public bool getHasFallen()
    {
        return hasFallen;
    }
}
