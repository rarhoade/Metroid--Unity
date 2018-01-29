using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZebSpawnerRef : MonoBehaviour {

    public SpawnZeb zebSpawner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetSpawner(SpawnZeb spawnZeb)
    {
        zebSpawner = spawnZeb;
    }
}
