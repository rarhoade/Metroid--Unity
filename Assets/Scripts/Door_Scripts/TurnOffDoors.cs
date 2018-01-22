using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffDoors : MonoBehaviour {
    public GameObject[] doors;

    public bool enteredRight = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchDoors()
    {
        foreach(GameObject door in doors)
        {
            if (door.activeSelf)
            {
                door.SetActive(false);
            }
            else
            {
                door.SetActive(true);
            }
        }
    }
}
