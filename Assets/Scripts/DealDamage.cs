using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour {
    public int myDamage = 8;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        UnitHealth uh = collision.gameObject.GetComponent<UnitHealth>();
        if (uh != null)
        {
            uh.TakeDamage(myDamage);
        }
    }
}
