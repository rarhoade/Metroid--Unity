using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            StartCoroutine(TempDestroy());
        }
    }

    IEnumerator TempDestroy()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(4.0f);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<BoxCollider>().enabled = true;
    }
}
