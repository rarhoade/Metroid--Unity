using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHolderObj : MonoBehaviour {
    private bool hittingPlayerThisFrame = false;
    private GameObject player;

	// Use this for initialization
	void Start () {
        StartCoroutine(resetBool());
        player = GameObject.FindGameObjectWithTag("Player");
	}

    public void hitPlayer(UnitHealth uh)
    {
        if(!hittingPlayerThisFrame)
        {
            hittingPlayerThisFrame = true;
            //Debug.Log("HIT PLAYER");
            uh.TakeDamage(1, 0, transform.position, true, false);
            PlayerRun.slowDown();
        }
    }

    IEnumerator resetBool()
    {
        while(true)
        {
            if(hittingPlayerThisFrame)
            {
                yield return new WaitForSecondsRealtime(0.1f);
                hittingPlayerThisFrame = false;
                PlayerRun.speedUp();
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
