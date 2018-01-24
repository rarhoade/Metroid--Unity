using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public float smoothingTimeX;
    public float smoothingTimeY;
    public GameObject player;
    public int transitionSpeed;
	private bool transitioning;
	private int targetedRoom;
    private int velo;
    private PlayerState ps;
    private Vector2 vel;
    private Vector2[] minBounds = { new Vector2(21.5f,22), new Vector2(87.5f,22), new Vector2(103.5f, 22), new Vector2(167.5f, 20.5f),  new Vector2(183.5f, 127.5f), new Vector2(199.5f, 65.5f),  new Vector2(215.5f, 67f), new Vector2(119.5f, 157.5f), new Vector2(103.5f, 157.5f) };
    private Vector2[] maxBounds = { new Vector2(71.5f,22), new Vector2(87.5f,22), new Vector2(151.5f, 22), new Vector2(167.5f, 204.5f), new Vector2(183.5f, 127.5f), new Vector2(199.5f, 219.5f), new Vector2(327.5f, 67f), new Vector2(151.5f, 157.5f), new Vector2(103.5f, 157.5f) };
	private IDictionary<string, int> map = new Dictionary<string, int>();

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerState>();
		mapInit ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!transitioning) {
			//Debug.Log ("Uh oh");
			float posX = Mathf.SmoothDamp (transform.position.x, player.transform.position.x, ref vel.x, smoothingTimeX);
			float posY = Mathf.SmoothDamp (transform.position.y, player.transform.position.y, ref vel.y, smoothingTimeY);

			int roomNum = ps.whichRoom ();
			transform.position = new Vector3 (Mathf.Clamp (posX, minBounds [roomNum].x, maxBounds [roomNum].x),
				Mathf.Clamp (posY, minBounds [roomNum].y, maxBounds [roomNum].y), transform.position.z);
		} 
		else 
		{
			//Debug.Log ("Camera:" + this.transform.position);
			transform.position = new Vector3(transform.position.x + (transitionSpeed*velo*Time.fixedDeltaTime), transform.position.y, transform.position.z);
			if (velo < 0) {
				if (this.transform.position.x < maxBounds [targetedRoom].x) {
					//Debug.Log ("Transitioned");
					transitioning = false;
				}
			} 
			else {
				if (this.transform.position.x > minBounds [targetedRoom].x) {
					//Debug.Log ("Transitioned");
					transitioning = false;
				}
			}
			//Debug.Log ("Camera:" + this.transform.position);
		}
    }

    public int transition(int targetRoom)
    {
        if (!transitioning)
        {
            transitioning = true;
		    targetedRoom = targetRoom;
            string hasher = ps.whichRoom().ToString() + targetedRoom.ToString();
            Debug.Log (hasher);
            velo = map[hasher];
            return velo;
        }
        return 0;
    }

	void mapInit(){
		map.Add ("01", 1);
		map.Add ("10", -1);
		map.Add ("12", 1);
		map.Add ("21", -1);
		map.Add ("23", 1);
		map.Add ("32", -1);
		map.Add ("34", 1);
		map.Add ("43", -1);
		map.Add ("45", 1);
		map.Add ("54", -1);
		map.Add ("56", 1);
		map.Add ("65", -1);
		map.Add ("37", -1);
		map.Add ("73", 1);
		map.Add ("78", -1);
		map.Add ("87", 1);
	}
}
