using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaHolderObj : MonoBehaviour {
    private bool hittingPlayerThisFrame = false;

	// Use this for initialization
	void Start () {
        StartCoroutine(resetBool());
	}

    public void hitPlayer(UnitHealth uh)
    {
        if(!hittingPlayerThisFrame)
        {
            hittingPlayerThisFrame = true;
            Debug.Log("HIT PLAYER");
            uh.TakeDamage(1, 0, transform.position, true, false);
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
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
