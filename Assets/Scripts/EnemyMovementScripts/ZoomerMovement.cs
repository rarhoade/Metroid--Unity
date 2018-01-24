using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomerMovement : MonoBehaviour {

    Rigidbody rigid;
    SpriteRenderer spriteRend;
    public Vector3 startVel = Vector3.right;
    public float zoomerSpeed = 1.0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        //zoomPos = GetComponent<Transform>();
        spriteRend = GetComponent<SpriteRenderer>();
        rigid.velocity = startVel * zoomerSpeed;
    }

    private void FixedUpdate()
    {
        rayCastCheck();
        changeWallCheck();
        //rigid.velocity = startVel * zoomerSpeed;
    }

    void rayCastCheck()
    {
        Debug.DrawRay(transform.position, -transform.up * 0.55f, Color.white);
        if (!Physics.Raycast(transform.position, -transform.up, 0.65f))
        {
            transform.position = transform.position + (transform.right - transform.up) * 0.5f;
            transform.Rotate(Vector3.forward * -90);
            rigid.velocity = transform.right * zoomerSpeed;
            //Debug.Log("Velocity:" + rigid.velocity);
            Debug.DrawRay(transform.position, -transform.up * 0.65f, Color.blue);
        }
    }

    private void changeWallCheck()
    {
        RaycastHit hit;
        bool wallCheck = Physics.Raycast(transform.position, transform.right, out hit, 0.6f);
        //Debug.DrawRay(transform.position, transform.right * 0.55f, Color.red);
        //Debug.Log(hit.collider.gameObject.name);
        if (wallCheck && hit.collider.gameObject.layer==0)//(hit.collider.gameObject.name == "Tile_WALL" || hit.collider.gameObject.name == "Tile_NONE" || hit.collider.gameObject.name == "Tile_DOOR" || hit.collider.gameObject.name == "Tile_LAVA 1"))
        {
            //Debug.Log("Hit a wall");
            transform.Rotate(Vector3.forward * 90);
            rigid.velocity = transform.right * zoomerSpeed;
        }
    }
}
