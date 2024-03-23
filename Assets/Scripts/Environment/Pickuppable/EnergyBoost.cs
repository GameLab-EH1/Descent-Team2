using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBoost : MonoBehaviour
{
    [SerializeField] private int _energyToRecharge;
    [SerializeField] private int _scoreToAdd;
    [SerializeField] private AudioClip _pickUpSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            EventManager.OnPowerPickup?.Invoke(_energyToRecharge, false);
            EventManager.onScoreChange?.Invoke(_scoreToAdd);
            AudioManager.instance.PlaySoundEffect(_pickUpSound, transform, 1f);
            Destroy(gameObject);
        }
    }
}
