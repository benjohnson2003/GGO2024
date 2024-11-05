using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource effectsSource;
    [SerializeField] AudioSource musicSource;

    public void PlaySound(SoundProfile clip)
    {
        effectsSource.PlayOneShot(clip.Clip(), clip.volume);
    }

    public void PlayMusic(SoundProfile clip)
    {
        musicSource.PlayOneShot(clip.Clip(), clip.volume);
    }
}

[System.Serializable]
public struct SoundProfile
{
    public AudioClip[] clips;
    public AudioClip Clip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
    [Range(0, 1)] public float volume;
}