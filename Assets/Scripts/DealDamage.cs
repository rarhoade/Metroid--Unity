using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {
    public int myDamage = 8;
    public float knockback = 10f;

    private void OnTriggerEnter(Collider collision)
    {
		dealDamage(collision.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
		dealDamage(collision.gameObject);
    }

	private void dealDamage(GameObject go)
    {
        UnitHealth uh = go.GetComponent<UnitHealth>();
        if (uh != null)
        {
			Vector3 otherPost = new Vector3 (go.transform.position.x, go.transform.position.y, 0);
			//Debug.Log (otherPost);
			uh.TakeDamage(myDamage, knockback, this.transform.position);
        }
    }
}
