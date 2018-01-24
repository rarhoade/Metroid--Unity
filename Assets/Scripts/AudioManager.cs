using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance = null;

    public AudioClip BackgroundMusic;
    public AudioClip LowHealth;
    public AudioClip SamusRunning;
    public AudioClip BulletSound;
    public AudioClip MissleSound;



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

    public void playSamusRunning()
    {
        AudioSource.PlayClipAtPoint(SamusRunning, Camera.main.transform.position);
    }
}
