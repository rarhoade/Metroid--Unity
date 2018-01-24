using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance = null;

    public AudioClip BackgroundMusic;
    public AudioClip LowHealth;
    public AudioClip SamusRunning;
    public float runLockoutTime;


    Coroutine runForestrun;

    private AudioSource aud;

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
        aud = Camera.main.gameObject.GetComponent<AudioSource>();
        //StartCoroutine(playBackground());
    }

    IEnumerator playBackground()
    {
        while(true)
        {
            aud.PlayOneShot(BackgroundMusic);
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
        aud.PlayOneShot(SamusRunning);
        yield return new WaitForSeconds(runLockoutTime);
        runForestrun = null;
    }

    public void playLowHealth()
    {
        aud.PlayOneShot(LowHealth);
    }
}
