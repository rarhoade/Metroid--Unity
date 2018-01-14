using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour {

    public int healthTotal = 30;
    public bool invulnerability = false;
    public int numberOfBlinks = 2;
    public float blink = 0.4f;

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            invulnerability = !invulnerability;
        }
	}

    public void TakeDamage(int damage)
    {
        if (!invulnerability)
        {
            healthTotal -= damage;
            StartCoroutine(IFrames());
        }
        if (healthTotal <= 0)
        {
            //TODO Die
        }
    }

    IEnumerator IFrames()
    {
        SpriteRenderer[] sprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < numberOfBlinks; i++)
        {
            SetSpritesAlpha(sprites, 0.5f);
            yield return new WaitForSeconds(blink);
            SetSpritesAlpha(sprites, 1f);
            yield return new WaitForSeconds(blink);
        }
    }

    private void SetSpritesAlpha(SpriteRenderer[] s, float alpha)
    {
        for (int i = 0; i < s.Length; i++)
        {
            Color c = s[i].color;
            s[i].color = new Color(c.r, c.g, c.b, alpha);
        }
    }
}
