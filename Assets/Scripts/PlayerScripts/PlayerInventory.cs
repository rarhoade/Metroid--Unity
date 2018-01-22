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
    public bool hasMisslePower = false;
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
			StartCoroutine (CollectAndPause ());
			hasMorphBall = true;
		} else if (other.tag == "LongShot") {
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
        else if (other.tag == "MisslePower")
        {
            Destroy(other.gameObject);
            StartCoroutine(CollectAndPause());
            Debug.Log("Found Missle PowerUp");
            hasMisslePower = true;
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

    public void subtractMissles()
    {
        //missles = missles - 1;
        if (!uh.IsInvincable())
        {
            missles = missles - 1;
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

    public bool HasMisslePower()
    {
        return hasMisslePower;
    }

    public bool HasNumMissles()
    {
        return (missles > 0);
    }

	private IEnumerator CollectAndPause(){
        pickUpNoise.Play();
		Time.timeScale = 0.001f;
		float pauseEnder = Time.realtimeSinceStartup + 1.0f;
		while (pauseEnder >Time.realtimeSinceStartup) {
			yield return 0;
		}
		Time.timeScale = 1;
	}
}
