using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionRoom : MonoBehaviour {


    public Camera cam;
    public GameObject otherDoor;
	public GameObject otherTrigger;
	public int connectedDoor;
    private int timeToWalk = 1;

    private GameObject player;
    private int velo = 0;
    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
	public void initiateTransition(){
        //Disabling the colliders on the other room exit
        //otherDoor.SetActive (false);
        if (velo == 0)
        {
            toggleColliders();

            //Start Camera Move
		    velo = cam.GetComponent<CameraMove> ().transition (connectedDoor);
            //Debug.Log(velo);
            StartCoroutine(movePlayer());
		    //yield return new WaitForSeconds (3.0f);

        }
	}

    private void toggleColliders()
    {
        BoxCollider[] child = otherTrigger.GetComponentsInChildren<BoxCollider>();
        foreach (BoxCollider obj in child)
        {
            obj.enabled = !obj.enabled;
        }
    }

    IEnumerator movePlayer()
    {
        if (velo != 0)
        {
            player.GetComponent<PlayerState>().SetEnabled(false);
            player.GetComponent<Rigidbody>().velocity = new Vector3(velo*2, 0);
            yield return new WaitForSeconds(2);
            SetUpDoors dur = otherDoor.GetComponent<SetUpDoors>();
            dur.doorOpenClose();
            yield return new WaitForSeconds(dur.bufferSides+0.01f);
            player.GetComponent<Rigidbody>().velocity = new Vector3(velo * 2, 0);
            yield return new WaitForSeconds(timeToWalk);
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0);
            yield return new WaitForSeconds(dur.secondsToWait-timeToWalk);
            player.GetComponent<PlayerState>().SetEnabled(true);
            velo = 0;
            toggleColliders();
        }
    }
}
