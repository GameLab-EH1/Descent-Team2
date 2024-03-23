using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : MonoBehaviour
{
    [SerializeField] private int _scoreToAdd;
    [SerializeField] private AudioClip _pickUpSound;
    private void OnTriggerEnter(Collider other)
    {
        WeaponsManager weaponsManager = other.transform.GetComponent<WeaponsManager>();
        Debug.Log(other.transform.name);
        if (weaponsManager != null)
        {
            weaponsManager.PickUpWeapon(1);
            if (weaponsManager.WeaponUsing == 1)
            {
                EventManager.OnShooting?.Invoke(weaponsManager.Ammo[1]);
            }
            EventManager.onScoreChange?.Invoke(_scoreToAdd);
            AudioManager.instance.PlaySoundEffect(_pickUpSound, transform, 1f);
            Destroy(gameObject);
        }
    }
}
