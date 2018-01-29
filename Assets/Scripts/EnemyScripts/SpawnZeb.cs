using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZeb : MonoBehaviour {

    //public GameObject zebPrefab;
    //public Camera cameraCheck;
    // Use this for initialization
    public GameObject anObject;
    public Collider anObjCollider;
    private Camera cam;
    private Plane[] planes;
    public Vector3 zebSpawnOffset;
    GameObject spawnedZeb = null;
    int layerMask = 1 << 11;

    void Start () {
        cam = Camera.main;
        anObjCollider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(anObjCollider.bounds);
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        if (GeometryUtility.TestPlanesAABB(planes, anObjCollider.bounds))
        {
            Debug.DrawRay(this.transform.position, Vector3.up * 13f, Color.white);
            if (!Physics.Raycast(this.transform.position, Vector3.up, 13f, layerMask) && spawnedZeb == null)
            {
                spawnedZeb = GameObject.Instantiate(anObject);
                spawnedZeb.transform.position = this.transform.position +  zebSpawnOffset;
                spawnedZeb.GetComponent<ZebSpawnerRef>().SetSpawner(this);
            }
        }
        //else
        //    Debug.Log("Nothing has been detected");
    }

    public void setSpawnedZebItem(GameObject o)
    {
        spawnedZeb = o;
    }
}
