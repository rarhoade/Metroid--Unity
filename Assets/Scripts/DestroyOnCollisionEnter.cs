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
        if (rigid.velocity != Vector3.zero && (other.gameObject.layer == 0) && GetComponent<SkreeMovement>().getHasFallen())
        {
            //Debug.Log(other.gameObject.name + " " + other.gameObject.transform.localPosition);
            //this.transform.position = other.gameObject.transform.position;
            StartCoroutine(WaitAndDestroy());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if((other.gameObject.layer == 0) && GetComponent<SkreeMovement>().getHasFallen())
        {
            StartCoroutine(WaitAndDestroy());
        }
    }

    IEnumerator WaitAndDestroy()
    {
        rigid.velocity = Vector3.zero;
        yield return new WaitForSeconds(timeTillDeath);
        this.GetComponent<ExplodeOnDestroy>().ExplodeDestroy();
    }
}
