using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomerMovement : MonoBehaviour {
	//Face to Direction Movement Mapping
	//Left: Up (y+)
	//Right: Down (y-)
	//Up: Right (x+)
	//Down: Left (x-)
	public enum Direction{Left,Right,Up,Down}; //based on what direction the spikes are facing
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
		Debug.Log (transform.position);
		bool rayCheck = Physics.Raycast (transform.position, positionCheck, 0.75f, ~playerLayer);
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
		bool wallCheck = Physics.Raycast (transform.position, positionCheck, 0.6f, ~playerLayer);
		Debug.DrawRay (transform.position, positionCheck * 0.6f, Color.red);
		if (wallCheck) {
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
