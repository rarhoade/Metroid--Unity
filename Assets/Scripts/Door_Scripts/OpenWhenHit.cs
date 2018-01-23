using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWhenHit : MonoBehaviour {

	public SetUpDoors parentalUnit;

	void Start(){
		parentalUnit = GetComponentInParent<SetUpDoors> ();
	}
	// Use this for initialization
	void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
			parentalUnit.doorMoveSetup ();
        }
    }
		


}
