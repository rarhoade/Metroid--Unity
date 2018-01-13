using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour {

    PlayerInventory playerInventory;

    public GameObject Standing;
    public GameObject Morphed;
    public float bufferLookUp = 0.1f;

    public bool standing = true;

    private void Awake()
    {
        playerInventory = this.GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    //TODO update states so that you can only morphball in mid air
    void LateUpdate () {
		if(standing && Input.GetKeyDown(KeyCode.DownArrow) && playerInventory.HasMorphBall())
        {
            Standing.SetActive(false);
            Morphed.SetActive(true);
            standing = false;
        }

        if(!standing && Input.GetKeyDown(KeyCode.UpArrow))
        {
            Standing.SetActive(true);
            Morphed.SetActive(false);
            StartCoroutine(SetStanding());
        }
	}

    public bool isStanding()
    {
        return standing;
    }

    IEnumerator SetStanding()
    {
        yield return new WaitForSeconds(bufferLookUp);
        standing = true;
    }
}
