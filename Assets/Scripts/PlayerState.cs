﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    PlayerInventory playerInventory;

    public GameObject standing;
    public GameObject morphed;

    bool isStanding = false;

    private void Awake()
    {
        playerInventory = this.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    //TODO update states so that you can only morphball in mid air
    void Update () {
		if(isStanding && Input.GetKeyDown(KeyCode.DownArrow) && playerInventory.HasMorphBall())
        {
            standing.SetActive(false);
            morphed.SetActive(true);
            isStanding = false;
        }

        if(!isStanding && (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            standing.SetActive(true);
            morphed.SetActive(false);
            isStanding = true;
        }
	}
}
