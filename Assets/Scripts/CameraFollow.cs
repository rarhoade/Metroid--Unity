using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //TODO: stop at the end of a level and wait for transition into next level before shifting over
    //TODO: vertical movement of camera


    public GameObject player;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate ()
    {
        //TODO:fix vertical shifting on the player should only move if there is no walls above the player
        Vector3 shift = new Vector3(player.transform.position.x, transform.position.y, offset.z);
        transform.position = shift;
	}
}
