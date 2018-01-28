using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {

    public float probability = 0.33f;
    public float missleProbability = 0.5f;
    public GameObject missleDrop;
    public GameObject energyDrop;

    PlayerState playerState;
    bool hasMissles;
	// Use this for initialization
	void Start () {
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        //missleDrop = (GameObject)Resources.Load("/Prefabs/Pickups/Missle.prefab");
        //energyDrop = (GameObject)Resources.Load("/Prefabs/Pickups/Energy.prefab");
	}
	
	// Update is called once per frame
	void Update () {
        hasMissles = playerState.IsMissleOn();
	}

    public void dropItem()
    {
        float rand = Random.value;
        Debug.Log(rand);
        if (rand < probability)
        {
            if (hasMissles && rand < missleProbability * probability)
            {
                Instantiate(missleDrop, this.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(energyDrop, this.transform.position, Quaternion.identity);
            }
        }
    }
}
