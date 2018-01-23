using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDestroy : MonoBehaviour {
    public GameObject explodeBulletPrefab;
    public float firingSpeed;
    public float posOffset;
    // Use this for initialization
    private void OnDestroy()
    {
        GameObject pellet_left = GameObject.Instantiate(explodeBulletPrefab);
        pellet_left.transform.position = new Vector3(this.transform.position.x - posOffset, this.transform.position.y, this.transform.position.z);
        pellet_left.transform.Rotate(Vector3.forward * 180);
        pellet_left.GetComponent<Rigidbody>().velocity = Vector3.left * firingSpeed;

        GameObject pellet_top_left = GameObject.Instantiate(explodeBulletPrefab);
        pellet_top_left.transform.position = new Vector3(this.transform.position.x - posOffset , this.transform.position.y + posOffset, this.transform.position.z);
        pellet_top_left.transform.Rotate(Vector3.forward * 135);
        pellet_top_left.GetComponent<Rigidbody>().velocity = (Vector3.left + Vector3.up) * firingSpeed;

        GameObject pellet_top = GameObject.Instantiate(explodeBulletPrefab);
        pellet_top.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + posOffset, this.transform.position.z);
        pellet_top.transform.Rotate(Vector3.forward * 90);
        pellet_top.GetComponent<Rigidbody>().velocity = Vector3.up * firingSpeed;

        GameObject pellet_top_right = GameObject.Instantiate(explodeBulletPrefab);
        pellet_top_right.transform.position = new Vector3(this.transform.position.x + posOffset, this.transform.position.y + posOffset, this.transform.position.z);
        pellet_top_right.transform.Rotate(Vector3.forward * 45);
        pellet_top_right.GetComponent<Rigidbody>().velocity = (Vector3.up + Vector3.right) * firingSpeed;

        GameObject pellet_right = GameObject.Instantiate(explodeBulletPrefab);
        pellet_right.transform.position = new Vector3(this.transform.position.x + posOffset, this.transform.position.y, this.transform.position.z);
        pellet_right.GetComponent<Rigidbody>().velocity = Vector3.right * firingSpeed;

        Debug.Log("I WAS KILLLED");
    }
}
