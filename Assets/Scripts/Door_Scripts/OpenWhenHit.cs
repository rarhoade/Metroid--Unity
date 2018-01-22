using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWhenHit : MonoBehaviour {

    public TurnOffDoors controller;

	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            controller.SwitchDoors();
        }
    }


}
