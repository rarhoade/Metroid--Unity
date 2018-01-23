using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpDoors : MonoBehaviour {


	public int connectedDoor;
	public float secondsToWait = 1.0f;
	// Use this for initialization

	public void doorMoveSetup()
	{
		StartCoroutine (DestroyAndReAppearCenter ());
		StartCoroutine (DestroyAndReAppearTopAndBottom ());
	}

	IEnumerator DestroyAndReAppearCenter()
	{
		this.transform.GetChild (1).gameObject.SetActive (false);
		yield return new WaitForSeconds (secondsToWait + 0.15f);
		this.transform.GetChild (1).gameObject.SetActive (true);
	}

	IEnumerator DestroyAndReAppearTopAndBottom()
	{
		yield return new WaitForSeconds (0.15f);
		this.transform.GetChild (0).gameObject.SetActive (false);
		this.transform.GetChild (2).gameObject.SetActive (false);
		yield return new WaitForSeconds (secondsToWait );
		this.transform.GetChild (0).gameObject.SetActive (true);
		this.transform.GetChild (2).gameObject.SetActive (true);
	}


}
