﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reo_Movement : MonoBehaviour {
    Rigidbody rigid;
    Coroutine swoop = null;
    bool swoopingUp = false;
    bool isLeft = false;
    public float reoSpeed = 100f;
	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }


    private void startAggro(GameObject intake, bool lefter)
    {
        Debug.Log("IM HERE");
        if(swoop == null)
        {
            Debug.Log("In Aggro");
            swoop = StartCoroutine(swoopDown(intake, lefter));
        }
    }
    private void FixedUpdate()
    {
        RaycastHit collidedLeft;
        RaycastHit collidedRight;
        //Debug.Log(rigid.velocity);
        bool foundObjLeft = Physics.Raycast(transform.position, Vector3.down + Vector3.left / 2, out collidedLeft, 10f);
        Debug.DrawRay(transform.position, (Vector3.down + Vector3.left / 2) * 13f, Color.red);
        bool foundObjRight = Physics.Raycast(transform.position, Vector3.down + Vector3.right / 2, out collidedRight, 10f);
        Debug.DrawRay(transform.position, (Vector3.down + Vector3.right / 2) * 13f, Color.red);
        if (foundObjLeft || foundObjRight)
        {
            GameObject hitObj = null;
            if (foundObjLeft && collidedLeft.collider.gameObject.layer == 8)
            {
                hitObj = collidedLeft.collider.gameObject;
                isLeft = true; 
            }
            else if (foundObjRight && collidedRight.collider.gameObject.layer == 8)
            {
                hitObj = collidedRight.collider.gameObject;
                isLeft = false;
            }
            if (hitObj != null)
            {
                Debug.Log("HitObj: " + hitObj.name);
                startAggro(hitObj, isLeft);
            }
        }
    }

    IEnumerator swoopUp(Vector3 horiDir)
    {
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = rigid.velocity + (Vector3.up / 2) * reoSpeed;
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = rigid.velocity + (Vector3.up / 2) * reoSpeed;
        yield return new WaitForSeconds(0.4f);
        rigid.velocity = Vector3.up * reoSpeed;
        swoopingUp = true;
        yield break;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (swoopingUp && collision.collider.gameObject.layer == 0)
        {
            Debug.Log("This is the object causing the collision " + collision.collider.gameObject.name);
            swoopingUp = false;
            swoop = null;
            rigid.velocity = Vector3.zero;
        }
    }

    IEnumerator swoopDown(GameObject player, bool isLeftDir)
    {
        Vector3 horiDir;
        if(isLeftDir){
            horiDir = Vector3.left;
        }
        else
        {
            horiDir = Vector3.right;
        }
        rigid.velocity = Vector3.down * reoSpeed;
        yield return new WaitForSeconds(0.4f);
        rigid.velocity = rigid.velocity + (horiDir/2 + Vector3.up / 2) * reoSpeed;
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = rigid.velocity + (horiDir/2 + Vector3.up / 2) * reoSpeed;
        StartCoroutine(moveWhileSameLevel(player, horiDir));
        yield break;
    }

    IEnumerator moveWhileSameLevel(GameObject player, Vector3 horiDir)
    {
        
        while(this.transform.position.y - player.transform.position.y >= -0.25 && this.transform.position.y - player.transform.position.y <= 0.25)
        {
            Debug.Log("REO: " + this.transform.position.y + " Player:" + player.transform.position.y);
            rigid.velocity = horiDir * reoSpeed/2;
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(swoopUp(horiDir));
        yield break;
    }
}
