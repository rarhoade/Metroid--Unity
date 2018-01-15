using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour {

    public Text energy;

	private Rigidbody rigid;

    public int healthTotal = 30;
    public bool invulnerability = false;
    public int numberOfBlinks = 2;
    public float blink = 0.4f;

	void start(){
		rigid = GetComponent<Rigidbody> ();
	}
    // Update is called once per frame
    void Update () {
        if (energy != null)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                invulnerability = !invulnerability;
                Physics.IgnoreLayerCollision(this.gameObject.layer, 10, invulnerability);
            }
        }
	}

	public void TakeDamage(int damage, float knockback, Vector3 otherObj)
    {
        if (!invulnerability)
        {
            //Debug.Log("Health: " + healthTotal.ToString());
            //Debug.Log("Damage: " + damage.ToString());

            healthTotal -= damage;
			//NOT WORKING KNOCKBACK
			/*if (this.name == "Player") 
			{
				//execute knockback
				//calculate by figuring out the direction of the 
				Debug.Log((otherObj - this.transform.position) * knockback);
				rigid.velocity = (otherObj - this.transform.position) * knockback;
			}*/
            if (energy != null)
            {
                energy.text = healthTotal.ToString();
            }
            //TODO implement knockback force (zero out velocity then add force)
            if (blink > 0)
            {
                StartCoroutine(IFrames());
            }
            else
            {
                StartCoroutine(FlashDamage());
            }
        }
        if (healthTotal <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator IFrames()
    {
        Physics.IgnoreLayerCollision(this.gameObject.layer, 10, true);
        SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < numberOfBlinks; i++)
        {
            SetSpritesAlpha(sprites, 0.5f);
            yield return new WaitForSeconds(blink);
            SetSpritesAlpha(sprites, 1f);
            yield return new WaitForSeconds(blink);
        }
        Physics.IgnoreLayerCollision(this.gameObject.layer, 10, false);
        yield break;
    }

    IEnumerator FlashDamage()
    {
        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();

        Color c = new Color(s.color.r, s.color.g, s.color.b);
        s.color = new Color(1, 0, 0, 1);
        yield return new WaitForSeconds(0.2f);
        s.color = new Color(1, 1, 1);
        yield return new WaitForEndOfFrame();
        yield break;
    }

    private void SetSpritesAlpha(SpriteRenderer[] s, float alpha)
    {
        for (int i = 0; i < s.Length; i++)
        {
            Color c = s[i].color;
            s[i].color = new Color(c.r, c.g, c.b, alpha);
        }
    }

	public void AddHealthFromPickup()
	{
		healthTotal += 3;
		if (energy != null)
		{
			energy.text = healthTotal.ToString();
		}
	}
    
    public bool IsInvincable()
    {
        return invulnerability;
    }
}
