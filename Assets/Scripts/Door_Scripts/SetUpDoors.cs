using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpDoors : MonoBehaviour {

	public float secondsToWait = 2.0f;
    public float bufferSides = 0.15f;
    public bool isMissleDoor = false;
    // Use this for initialization

    public int hits = 0;
    private bool shootable = true;

	public void doorOpenClose()
	{
		StartCoroutine (DestroyAndReAppearCenter ());
		StartCoroutine (DestroyAndReAppearTopAndBottom ());
        hits = 0;
    }

    public void doorWasHit(bool wasMissle)
    {
        if (shootable)
        {
            if (!isMissleDoor || hits >= 4)
            {
                doorOpenClose();
            }
            else if (wasMissle)
            {
                hits++;
                StartCoroutine(iframesShoot());
            }
        }
    }

    IEnumerator iframesShoot()
    {
        shootable = false;
        yield return new WaitForSeconds(0.1f);
        shootable = true;
    }

	IEnumerator DestroyAndReAppearCenter()
	{
		this.transform.GetChild (1).gameObject.SetActive (false);
		yield return new WaitForSeconds (secondsToWait + bufferSides);
		this.transform.GetChild (1).gameObject.SetActive (true);
        AudioManager.instance.playDoorSound();
	}

	IEnumerator DestroyAndReAppearTopAndBottom()
	{
		yield return new WaitForSeconds (bufferSides);
		this.transform.GetChild (0).gameObject.SetActive (false);
		this.transform.GetChild (2).gameObject.SetActive (false);
        AudioManager.instance.playDoorSound();
		yield return new WaitForSeconds (secondsToWait - bufferSides);
		this.transform.GetChild (0).gameObject.SetActive (true);
		this.transform.GetChild (2).gameObject.SetActive (true);
	}


}
