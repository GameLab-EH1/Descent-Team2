using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    [SerializeField] private AudioClip _musicClip, _escapeMusicClip;

    private void OnEnable()
    {
        EventManager.OnBossDeath += EscapeMusic;
    }
    private void OnDisable()
    {
        EventManager.OnBossDeath -= EscapeMusic;
    }

    private void Start()
    {
        AudioManager.instance.PlayMusic(_musicClip);
    }

    private void EscapeMusic()
    {
        AudioManager.instance.PlayMusic(_escapeMusicClip);
    }
    
}
