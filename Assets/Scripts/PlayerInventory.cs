using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public bool hasMorphBall = false;
    public bool hasLongShot = false;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "MorphBall")
        {
            Destroy(other.gameObject);
            hasMorphBall = true;
        }
        else if(other.tag == "LongShot")
        {
            Destroy(other.gameObject);
            hasLongShot = true;
        }
    }

    public bool HasMorphBall()
    {
        return hasMorphBall;
    }

    public bool HasLongShot()
    {
        return hasLongShot;
    }
}
