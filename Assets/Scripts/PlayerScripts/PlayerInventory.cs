using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public Text missleCount;

	private UnitHealth uh;

	AudioSource pickUpNoise;
    public bool hasMorphBall = false;
    public bool hasLongShot = false;
    private int missles;

    private void Start()
    {
        missles = 3;
        uh = GetComponent<UnitHealth>();
		pickUpNoise = GetComponent<AudioSource> ();
    }

    private void Update()
    {
        if (uh.IsInvincable())
        {
            missleCount.text = "99";
        }
        else
        {
            missleCount.text = missles.ToString();
        }
    }

    public void OnTriggerEnter(Collider other)
	{
		if (other.tag == "MorphBall") {
			Destroy (other.gameObject);
			pickUpNoise.Play ();
			StartCoroutine (CollectAndPause ());
			hasMorphBall = true;
		} else if (other.tag == "LongShot") {
			pickUpNoise.Play ();
			Destroy (other.gameObject);
			StartCoroutine(CollectAndPause());
			hasLongShot = true;
		} else if (other.tag == "Missle") {
			Destroy (other.gameObject);
			addMissles ();
		} else if (other.tag == "Energy") {
			Destroy (other.gameObject);
			uh.AddHealthFromPickup ();
		}

    }

    public void addMissles()
    {
        missles++;
        if (!uh.IsInvincable())
        {
            missleCount.text = missles.ToString();
        }
    }

    public bool HasMorphBall()
    {
        return hasMorphBall;
    }

    public bool HasLongShot()
    {
        return hasLongShot;
    }

	private IEnumerator CollectAndPause(){
		Time.timeScale = 0.001f;
		float pauseEnder = Time.realtimeSinceStartup + 1.0f;
		while (pauseEnder >Time.realtimeSinceStartup) {
			yield return 0;
		}
		Time.timeScale = 1;
	}
}
