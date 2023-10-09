using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicAS;
    public AudioSource sfxAS;
    public float musicVol = 0.5f;
    public float sfxVol = 1.0f;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicAS.volume = musicVol;
        musicAS.loop = true;
        sfxAS.volume = sfxVol;
        sfxAS.loop = false;
    }

    public void PlaySFX(AudioClip sound)
    {
        sfxAS.PlayOneShot(sound);
    }
    public void SetMusic(AudioClip music)
    {
        if (musicAS.isPlaying)
            musicAS.Stop();
        musicAS.clip = music;
        musicAS.Play();
    }
}
