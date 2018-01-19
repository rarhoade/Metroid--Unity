using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollisionEnter : MonoBehaviour {

    public float timeTillDeath = 2.0f;
    public Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

	private void OnCollisionEnter(Collision other)
    {
        if(rigid.velocity != Vector3.zero && (other.gameObject.name != "Standing" || other.gameObject.name != "Morphed"))
        {
            StartCoroutine(WaitAndDestroy());
        }
    }

    IEnumerator WaitAndDestroy()
    {
        rigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(timeTillDeath);
        Destroy(this.gameObject);
    }
}
