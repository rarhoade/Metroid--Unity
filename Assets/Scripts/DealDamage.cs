using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {
    public int myDamage = 8;
    public float knockback = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        UnitHealth uh = collision.gameObject.GetComponent<UnitHealth>();
        if (uh != null)
        {
            uh.TakeDamage(myDamage, knockback);
        }
    }
}
