using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOnParent : MonoBehaviour {

	TransitionRoom parentalUnit;
	// Use this for initialization
	void Start () {
		parentalUnit = GetComponentInParent <TransitionRoom> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider other){
		if (other.gameObject.layer == 8) {
			parentalUnit.initiateTransition ();
		}
	}
}
