using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour {

    LavaHolderObj parent;

	// Use this for initialization
	void Start () {
        parent = GetComponentInParent<LavaHolderObj>();
	}

    private void OnTriggerEnter(Collider collision)
    {
        UnitHealth uh = collision.gameObject.GetComponent<UnitHealth>();
        if (uh != null)
        {
            parent.hitPlayer(uh);
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        UnitHealth uh = collision.gameObject.GetComponent<UnitHealth>();
        if (uh != null)
        {
            parent.hitPlayer(uh);
        }
    }
}
