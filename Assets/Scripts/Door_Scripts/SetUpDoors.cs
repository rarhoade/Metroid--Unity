using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpDoors : MonoBehaviour {

	public float secondsToWait = 2.0f;
    public float bufferSides = 0.15f;
	// Use this for initialization

	public void doorOpenClose()
	{
		StartCoroutine (DestroyAndReAppearCenter ());
		StartCoroutine (DestroyAndReAppearTopAndBottom ());
	}

	IEnumerator DestroyAndReAppearCenter()
	{
		this.transform.GetChild (1).gameObject.SetActive (false);
		yield return new WaitForSeconds (secondsToWait + bufferSides);
		this.transform.GetChild (1).gameObject.SetActive (true);
	}

	IEnumerator DestroyAndReAppearTopAndBottom()
	{
		yield return new WaitForSeconds (bufferSides);
		this.transform.GetChild (0).gameObject.SetActive (false);
		this.transform.GetChild (2).gameObject.SetActive (false);
		yield return new WaitForSeconds (secondsToWait );
		this.transform.GetChild (0).gameObject.SetActive (true);
		this.transform.GetChild (2).gameObject.SetActive (true);
	}


}
