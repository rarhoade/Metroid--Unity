using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public Text missleCount;

    private PlayerState playerState;
	private UnitHealth uh;
    
    public bool hasMorphBall = false;
    public bool hasLongShot = false;
    public bool hasTuckBall = false;
    public bool hasMisslePower = false;
    private int missles;

    private void Start()
    {
        missles = 3;
        uh = GetComponent<UnitHealth>();
        playerState = GetComponent<PlayerState>();
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
		} else if (other.tag == "TuckBall") {
            Destroy(other.gameObject);
            StartCoroutine(CollectAndPause());
            hasTuckBall = true;
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
        AudioManager.instance.playMisslePickup();
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

    public bool HasTuckBall()
    {
        return hasTuckBall;
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
        float stopTime = AudioManager.instance.playPowerupPickup();
        Debug.Log(stopTime);
        playerState.SetEnabled(false);
		Time.timeScale = 0.001f;
        yield return new WaitForSecondsRealtime(stopTime-.9f);
        playerState.SetEnabled(true);
        Time.timeScale = 1f;
        yield break;
	}
}
