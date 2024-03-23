using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusicStarter : MonoBehaviour
{
    [SerializeField] private AudioClip musicBoss;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            AudioManager.instance.PlayMusicBoss(musicBoss);
        }
    }
}
