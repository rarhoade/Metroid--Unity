using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float smoothingTimeX;
    public float smoothingTimeY;

    public GameObject player;

    private PlayerState ps;
    private Vector2 vel;
    private bool transitioning = false;
    private Vector2[] minBounds = { new Vector2(21.5f,22), new Vector2(87.5f,22), new Vector2(103.5f, 22), new Vector2(167.5f, 20.5f),  new Vector2(183.5f, 127.5f), new Vector2(199.5f, 65.5f),  new Vector2(215.5f, 67.5f), new Vector2(119.5f, 157.5f), new Vector2(103.5f, 157.5f) };
    private Vector2[] maxBounds = { new Vector2(71.5f,22), new Vector2(87.5f,22), new Vector2(151.5f, 22), new Vector2(167.5f, 204.5f), new Vector2(183.5f, 127.5f), new Vector2(199.5f, 219.5f), new Vector2(327.5f, 67.5f), new Vector2(151.5f, 157.5f), new Vector2(103.5f, 157.5f) };


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerState>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (!transitioning)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref vel.x, smoothingTimeX);
            float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref vel.y, smoothingTimeY);

            int roomNum = ps.whichRoom();
            transform.position = new Vector3(Mathf.Clamp(posX, minBounds[roomNum].x, maxBounds[roomNum].x),
                                                Mathf.Clamp(posY, minBounds[roomNum].y, maxBounds[roomNum].y), transform.position.z);
        }
    }

    void transition(int targetRoom)
    {
        transitioning = true;
        //TODO Move camera smoothly from ps.whichRoom() to targetRoom along ONLY X axis
        transitioning = false;
    }
}
