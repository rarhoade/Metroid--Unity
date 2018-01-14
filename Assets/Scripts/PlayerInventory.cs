﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour {

    public Text missleCount;

	private UnitHealth instance;

    public bool hasMorphBall = false;
    public bool hasLongShot = false;

    private int missles;


	private void Awake(){
		instance = GetComponent<UnitHealth> ();
	}

    private void Start()
    {
        missles = 3;
    }

    public void OnTriggerEnter(Collider other)
    {
		if (other.tag == "MorphBall") {
			Destroy (other.gameObject);
			hasMorphBall = true;
		} else if (other.tag == "LongShot") {
			Destroy (other.gameObject);
			hasLongShot = true;
		} else if (other.tag == "Missle") {
			Destroy (other.gameObject);
			addMissles ();
		} else if (other.tag == "Energy") {
			Destroy (other.gameObject);
			instance.AddHealthFromPickup ();
		}

    }

    public void addMissles()
    {
        missles++;
        missleCount.text = missles.ToString();
    }

    public bool HasMorphBall()
    {
        return hasMorphBall;
    }

    public bool HasLongShot()
    {
        return hasLongShot;
    }
}
