using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBulletTransform : MonoBehaviour {


    public GameObject transformer;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        //Debug.Log(other.gameObject.layer);
        if (other.gameObject.layer == 9)
        {
            //Debug.Log("this is going");
            GameObject summon = Instantiate(transformer);
            summon.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }

}
