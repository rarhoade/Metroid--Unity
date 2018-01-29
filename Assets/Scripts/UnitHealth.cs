using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnitHealth : MonoBehaviour {

    public Text energy;

	private Rigidbody rigid;
    private PlayerState playerState;

    public bool isEnemy;
    public bool isPaused;

    public int healthTotal = 30;
    public bool invulnerability = false;
    public int numberOfBlinks = 2;
    public float blink = 0.4f;
    public float timeToPause = 0.3f;
    public float lowHTime=0.35f;

	void Start(){
		rigid = GetComponent<Rigidbody> ();
        playerState = GetComponent<PlayerState>();
        if (playerState != null)
        {
            StartCoroutine(lowHealth());
        }
	}

    // Update is called once per frame
    void Update () {
        if (energy != null)
        {
            if (playerState.IsEnabled())
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    invulnerability = !invulnerability;
                    Physics.IgnoreLayerCollision(this.gameObject.layer, 10, invulnerability);
                }
            }

        }
	}

    public void TakeDamage(int damage, float knockback, Vector3 otherObj, bool lateralKnockback)
    {
        TakeDamage(damage, knockback, otherObj, lateralKnockback, true);
    }


    public void TakeDamage(int damage, float knockback, Vector3 otherObj, bool lateralKnockback, bool allowInvulnerability)
    {
        if (!invulnerability)
        {
            //Debug.Log("Health: " + healthTotal.ToString());
            //Debug.Log("Damage: " + damage.ToString());
            //Debug.Log("Striker " + otherObj.ToString());
            //Debug.Log("My Pos: " + this.gameObject.transform.position.ToString());

            //Debug.Log("Health Tot: " + healthTotal + " Taking: " + damage + " energy is " + energy==null);
            healthTotal -= damage;
            //Debug.Log("Outcome: " + healthTotal);
            //NOT WORKING KNOCKBACK
            //execute knockback
            //calculate by figuring out the direction of the 
            //Debug.Log("Calc: " + (this.transform.position - otherObj).ToString());
            //rigid.velocity = (this.transform.position - otherObj) * knockback;
            if (lateralKnockback)
            {
                rigid.velocity = Vector3.zero;
                if (this.transform.position.x - otherObj.x < 0.3f)
                {
                    rigid.AddForce(transform.right * -1 * knockback);
                }
                else
                {
                    rigid.AddForce((this.transform.position - otherObj) * knockback);
                }
            }
            else
            {
                //rigid.AddForce(new Vector3((this.transform.position - otherObj).x * knockback, 10));
                if(playerState)
                {
                    playerState.SendFlying();
                }
                else if (isEnemy)
                {
                    StartCoroutine(pauseOnHit());
                }
                if (knockback != 0)
                {
                    rigid.velocity = Vector3.zero;
                    rigid.velocity = (this.transform.position - otherObj) * knockback;
                }
            }
			
            if (healthTotal < 0)
            {
                healthTotal = 0;
            }

            if (energy != null)
            {
                energy.text = healthTotal.ToString();
            }
            if (healthTotal <= 0)
            {
                if (isEnemy)
                {
                    Debug.Log("drop is being called");
                    GameObject g = GetComponent<DropItem>().dropItem();
                    ZebSpawnerRef z = GetComponent<ZebSpawnerRef>();
                    if (z != null)
                    {
                        z.zebSpawner.setSpawnedZebItem(g);
                    }
                    Destroy(this.gameObject);
                }
                else
                {
                    if (GameManager.instance == null)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    else
                    {
                        GameManager.instance.resetGame();
                    }
                }
            }
            //TODO implement knockback force (zero out velocity then add force)
            else if (blink > 0)
            {
                if(allowInvulnerability)
                {
                    StartCoroutine(IFrames(false));
                    AudioManager.instance.playSamusHit();
                }
            }
            else
            {
                StartCoroutine(FlashDamage());
            }
        }
    }

    public void resetGame()
    {
        playerState.SetEnabled(false);
        energy.text = "0... You've Died";
        StartCoroutine(IFrames(true));
    }

    IEnumerator IFrames(bool doMore)
    {
        Physics.IgnoreLayerCollision(this.gameObject.layer, 10, true);
        SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        int blinkers = numberOfBlinks;
        if (doMore)
        {
            blinkers *= 3;
        }
        for (int i = 0; i < blinkers; i++)
        {
            if (i == 2*numberOfBlinks)
            {
                energy.text = "0... Respawning";
            }
            else if (i > numberOfBlinks)
            {
                energy.text += ".";
            }
            SetSpritesAlpha(sprites, 0.5f);
            yield return new WaitForSeconds(blink);
            SetSpritesAlpha(sprites, 1f);
            yield return new WaitForSeconds(blink);
        }
        if (!invulnerability)
        {
            Physics.IgnoreLayerCollision(this.gameObject.layer, 10, false);
        }

        if (doMore)
        {
            energy.text = "30";
            healthTotal = 30;
            playerState.SetEnabled(true);
        }
        yield break;
    }

    IEnumerator FlashDamage()
    {
        SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();

        //Color c = new Color(s.color.r, s.color.g, s.color.b);
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
        AudioManager.instance.playHealthPickup();
	}
    
    public bool IsInvincable()
    {
        return invulnerability;
    }

    IEnumerator lowHealth()
    {
        while(true)
        {
            if(!invulnerability && healthTotal <= 15)
            {
                AudioManager.instance.playLowHealth();
            }
            else
            {
                AudioManager.instance.stopLowHealth();
            }
            yield return new WaitForSeconds(lowHTime);
        }
    }

    IEnumerator pauseOnHit()
    {
        Vector3 holdVel = rigid.velocity;
        rigid.velocity = Vector3.zero;
        Debug.Log("Pausing");
        isPaused = true;
        yield return new WaitForSeconds(timeToPause);
        isPaused = false;
        rigid.velocity = holdVel;

    }

    public bool hasPaused()
    {
        return isPaused;
    }
}
