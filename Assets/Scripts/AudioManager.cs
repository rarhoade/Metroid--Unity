using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance = null;


    public AudioClip BackgroundMusic;
    public AudioClip LowHealth;
    public AudioClip SamusRunning;
    public AudioClip SamusJumping;
    public AudioClip HealthPickup;
    public AudioClip EnemyDamaged;
    public AudioClip PowerupPickup;
    public float runLockoutTime;

    private AudioSource player;
    private AudioSource enemy;
    private AudioSource background;
    private AudioSource SFX;


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
        bg = StartCoroutine(playBackground());
    }

    IEnumerator playBackground()
    {
        while(true)
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
        background.PlayOneShot(LowHealth);
    }

    public void playHealthPickup()
    {
        SFX.PlayOneShot(HealthPickup);
    }

    public void playPowerupPickup()
    {
        StopCoroutine(bg);
        background.Stop();
        SFX.PlayOneShot(PowerupPickup);
        bg = StartCoroutine(playBackground());
    }
}
