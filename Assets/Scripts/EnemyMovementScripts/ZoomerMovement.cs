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
        Debug.DrawRay(transform.position, -transform.up * 0.65f, Color.white);
        if (!Physics.Raycast(transform.position, -transform.up, 0.65f))
        {
            transform.position = transform.position + (transform.right - transform.up) * 0.6f;
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

    //Face to Direction Movement Mapping
    //Left: Up (y+)
    //Right: Down (y-)
    //Up: Right (x+)
    //Down: Left (x-)
    /*public enum Direction{Left,Right,Up,Down}; //based on what direction the spikes are facing
	public SpriteRenderer spriteRend;
	public Direction currentFace;
	public float zoomerSpeed = 5.0f;
	public Rigidbody rigid;
	public Sprite[] zoomerSprites;

	private Vector3 actingVelocity;
	private Vector3 positionCheck;
	private Transform zoomPos;
	private int playerLayer = 1 << 8;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
		zoomPos = GetComponent<Transform> ();
		spriteRend = GetComponent<SpriteRenderer> ();

	}

	//Face to Wall Collision Mapping:
	//Left: Left (x-)
	//Right: Right (x+)
	//Up: Up (y+)
	//Down: Down (y-)
	// Update is called once per frame

	void FixedUpdate()
	{
		raycastCheck ();
		wallCrashCheck ();
		moveZoomer ();
	}

	void moveZoomer()
	{
		if (currentFace == Direction.Left) {
			//move up
			actingVelocity = Vector3.up * zoomerSpeed;
		} 
		else if (currentFace == Direction.Right) {
			actingVelocity = Vector3.down * zoomerSpeed;
		} 
		else if (currentFace == Direction.Down) {
			actingVelocity = Vector3.left * zoomerSpeed;
		} 
		else if (currentFace == Direction.Up) {
			actingVelocity = Vector3.right * zoomerSpeed;
		}
		rigid.velocity = actingVelocity;
	}

	void raycastCheck()
	{
		//check which direction we need to raycast
		if (currentFace == Direction.Left) {
			//move up
			positionCheck = Vector3.right;
		} 
		else if (currentFace == Direction.Right) {
			positionCheck = Vector3.left;
		} 
		else if (currentFace == Direction.Down) {
			positionCheck = Vector3.up;
		} 
		else if (currentFace == Direction.Up) {
			positionCheck = Vector3.down;
		}
        bool rayCheck = Physics.Raycast(transform.position, positionCheck, 0.75f, ~playerLayer);
        //RAH HERE
        RaycastHit hit;
        Physics.Raycast(transform.position, positionCheck, out hit, 0.75f);
        Debug.DrawRay (transform.position, positionCheck * 0.75f, Color.white);
		if(!rayCheck)
		{
			//if we detect no collision then we need to rotate and change currentFace
			changeFaceRay();
			shiftSprite ();
		}
	}

	void wallCrashCheck()
	{
		if (currentFace == Direction.Left) {
			//move up
			positionCheck = Vector3.up;
		} 
		else if (currentFace == Direction.Right) {
			positionCheck = Vector3.down;
		} 
		else if (currentFace == Direction.Down) {
			positionCheck = Vector3.left;
		} 
		else if (currentFace == Direction.Up) {
			positionCheck = Vector3.right;
		}
		RaycastHit hit;
		bool wallCheck = Physics.Raycast (transform.position, positionCheck, out hit, 0.6f, ~playerLayer);
		Debug.DrawRay (transform.position, positionCheck * 0.6f, Color.red);
		if ((wallCheck && hit.collider.gameObject.name == "Tile_WALL") || (wallCheck && hit.collider.gameObject.name == "Tile_NONE")) {
			changeFaceWall ();
			shiftSprite ();
		}
	}

	void changeFaceRay()
	{
		if (currentFace == Direction.Left) {
			//move up
			currentFace = Direction.Up;
			this.transform.position += Vector3.right * 0.5f + Vector3.up * 0.5f;
		} 
		else if (currentFace == Direction.Right) {
			currentFace = Direction.Down;
			this.transform.position += Vector3.left * 0.5f + Vector3.down * 0.5f;
		} 
		else if (currentFace == Direction.Down) {
			currentFace = Direction.Left;
			this.transform.position += Vector3.up * 0.5f + Vector3.left * 0.5f;
		} 
		else if (currentFace == Direction.Up) {
			currentFace = Direction.Right;
			this.transform.position += Vector3.down * 0.5f + Vector3.right * 0.5f;
		}
	}

	void changeFaceWall()
	{
		if (currentFace == Direction.Left) {
			//move up
			currentFace = Direction.Down;
			//this.transform.position += Vector3.left * 0.5f;
		} 
		else if (currentFace == Direction.Right) {
			currentFace = Direction.Up;
			//this.transform.position += Vector3.right * 0.5f;
		} 
		else if (currentFace == Direction.Down) {
			currentFace = Direction.Right;
			//this.transform.position += Vector3.down * 0.5f;
		} 
		else if (currentFace == Direction.Up) {
			currentFace = Direction.Left;
			//this.transform.position += Vector3.up * 0.5f;
		}
	}

	void shiftSprite()
	{
		if (currentFace == Direction.Left) {
			spriteRend.sprite = zoomerSprites [3];
		} else if (currentFace == Direction.Right) {
			spriteRend.sprite = zoomerSprites [1];
		} else if (currentFace == Direction.Up) {
			spriteRend.sprite = zoomerSprites[0];
		} else if (currentFace == Direction.Down) {
			spriteRend.sprite = zoomerSprites [2];
		}
	}

	/*void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.layer == 0) 
		{
			changeFaceWall ();
		}
	}*/
}
