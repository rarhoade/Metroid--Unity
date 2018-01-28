using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidOnPassThrough : MonoBehaviour {

    public Vector3 thisPosition;
	// Use this for initialization
	void Start () {
        thisPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.transform.position.y > this.transform.position.y)
        {
            GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            //GetComponent<BoxCollider>().isTrigger = true;
            //StartCoroutine(MakeSolid());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.position.y > this.transform.position.y)
        {
            GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<SpriteRenderer>().color = Color.yellow;
            //GetComponent<BoxCollider>().isTrigger = true;
            //StartCoroutine(MakeSolid());
        }
    }

    IEnumerator MakeSolid()
    {
        Debug.Log("We did it");
        yield return new WaitForSeconds(2.0f);
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public Vector3 GetThisPosition()
    {
        return thisPosition;
    }
}
