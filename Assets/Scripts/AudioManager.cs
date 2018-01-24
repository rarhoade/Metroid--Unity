using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance = null;


    public AudioClip BackgroundMusic;
    public AudioClip LowHealth;
    public AudioClip SamusRunning;
    public AudioClip SamusJumping;
    public float runLockoutTime;

    public AudioClip HealthPickup;
    public AudioClip MisslePickup;
    public AudioClip PowerupPickup;

    public AudioClip SamusDamaged;
    public AudioClip EnemyDamaged;
    public AudioClip DoorSound;

    private AudioSource player;
    private AudioSource enemy;
    private AudioSource background;
    private AudioSource SFX;
    private AudioSource idiot;
    private Coroutine low;

    Coroutine runForestrun;
    Coroutine bg;


    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioSource[] sources = Camera.main.gameObject.GetComponents<AudioSource>();
        player = sources[0];
        enemy = sources[1];
        background = sources[2];
        SFX = sources[3];
        idiot = sources[4];
        bg = StartCoroutine(playBackground());
    }

    IEnumerator playBackground()
    {
        while (true)
        {
            background.PlayOneShot(BackgroundMusic);
            yield return new WaitForSeconds(BackgroundMusic.length);
        }
    }

    public void playSamusRunning()
    {
        if (runForestrun == null)
        {
            runForestrun = StartCoroutine(runSamus());
        }
    }

    IEnumerator runSamus()
    {
        player.PlayOneShot(SamusRunning);
        yield return new WaitForSeconds(runLockoutTime);
        runForestrun = null;
    }

    public void playJump()
    {
        player.PlayOneShot(SamusJumping);
    }

    public void playLowHealth()
    {
        if (low == null)
        {
            low = StartCoroutine(lowHealth());
        }
    }

    public void stopLowHealth()
    {
        if (low != null)
        {
            StopCoroutine(low);
            idiot.Stop();
            low = null;
        }
    }

    IEnumerator lowHealth()
    {
        while (true)
        {
            idiot.PlayOneShot(LowHealth);
            yield return new WaitForSeconds(LowHealth.length-0.4f);
        }
    }

    public void playSamusHit()
    {
        SFX.PlayOneShot(SamusDamaged);
    }

    public void playHealthPickup()
    {
        SFX.PlayOneShot(HealthPickup);
    }

    public void playMisslePickup()
    {
        SFX.PlayOneShot(MisslePickup);
    }

    public void playDoorSound()
    {
        SFX.PlayOneShot(DoorSound);
    }

    public float playPowerupPickup()
    {
        StartCoroutine(doPowerup());
        return PowerupPickup.length;
    }

    IEnumerator doPowerup()
    {
        StopCoroutine(bg);
        background.Stop();
        SFX.PlayOneShot(PowerupPickup);
        bg = StartCoroutine(playBackground());
        yield break;
    }
}
