using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.position.y > this.transform.position.y && other.gameObject.layer == 8)
        {
            setCheckpoint(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.position.y > this.transform.position.y && other.gameObject.layer == 8)
        {
            setCheckpoint(other.gameObject);
        }
    }

    private void setCheckpoint(GameObject g)
    {
        if (GameManager.instance != null)
        {
            if (g.GetComponent<PlayerState>() != null)
            {
                GameManager.instance.updateCheckpoint(g, Camera.main.transform);
            }
        }
    }
}
