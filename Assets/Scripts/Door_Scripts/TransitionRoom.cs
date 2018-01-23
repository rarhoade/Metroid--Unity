using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRoom : MonoBehaviour {


	public GameObject otherDoor;
	public GameObject Player;
	public Camera cam;
	public GameObject otherTrigger;
	public int connectedDoor;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void initiateCameraTransition(){
		//otherDoor.SetActive (false);
		BoxCollider[] child = otherTrigger.GetComponentsInChildren<BoxCollider> ();
		foreach (BoxCollider obj in child) {
			obj.enabled = !obj.enabled;
		}
		cam.GetComponent<CameraMove> ().transition (connectedDoor);
		//yield return new WaitForSeconds (3.0f);
	}
}
