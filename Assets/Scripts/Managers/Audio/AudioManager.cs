using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource _soundObject;
    
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource musicSource2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    private void OnEnable()
    {
        EventManager.OnBossDeath += StopMusicBoss;
    }
    private void OnDisable()
    {
        EventManager.OnBossDeath -= StopMusicBoss;
    }

    public void PlaySoundEffect(AudioClip clip, Transform playPosition, float volume)
    {
        AudioSource audioSource = Instantiate(_soundObject, playPosition.position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        float clipLenght = audioSource.clip.length;
        Destroy(audioSource.gameObject, clipLenght);
    }
    
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlayMusicBoss(AudioClip clip)
    {
        musicSource2.clip = clip;
        musicSource2.Play();
    }
    
    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void StopMusicBoss()
    {
        musicSource2.Stop();
    }
}
