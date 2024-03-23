using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLogic : MonoBehaviour
{
    [SerializeField] private int _shield;
    [SerializeField] private int _maxShield;
    
    [SerializeField] private int _shootPower;
    [SerializeField] private int _maxShootPower;
    private int _shootPowerSaver;

    [SerializeField] private UImanager _uiManager;

    [Header("Sound")] public AudioClip LaserSound, FlareSound, WallHitSound;
    

    private void Start()
    {
        EventManager.OnShieldChange?.Invoke(_shield);
        EventManager.OnPowerChange?.Invoke(_shootPower);
    }
    private void OnEnable()
    {
        EventManager.OnLaserShooting += ShootLaserLogic;
        EventManager.OnPowerPickup += OnPowerPickedUp;
        EventManager.OnShieldPickOrDmg += ShieldValueChange;
    }
    private void OnDisable()
    {
        EventManager.OnLaserShooting -= ShootLaserLogic;
        EventManager.OnPowerPickup -= OnPowerPickedUp;
        EventManager.OnShieldPickOrDmg -= ShieldValueChange;
    }

    private void ShootLaserLogic(bool isFirstW)
    {
        if (isFirstW)
        {
            if (_shootPowerSaver == 3)
            {
                _shootPower--;
                _shootPowerSaver = 0;
            }
            else
            {
                _shootPowerSaver++;
            }
            AudioManager.instance.PlaySoundEffect(LaserSound, transform, 1f);
        }
        else
        {
            _shootPower--;
            AudioManager.instance.PlaySoundEffect(FlareSound, transform, 1f);
        }
        EventManager.OnPowerChange?.Invoke(_shootPower);
        if (_shootPower <= 0)
        {
            EventManager.OnLaserNoBullet?.Invoke();
            return;
        }
    }
    private void OnPowerPickedUp(int toRecharge, bool isRechargeZone)
    {
        if (_shootPower >= _maxShootPower && isRechargeZone)
        {
            return;
        }
        _shootPower += toRecharge;
        EventManager.OnPowerChange?.Invoke(_shootPower);
    }

    private void ShieldValueChange(int quantity)
    {
        if (_shield + quantity < _maxShield)
        {
            _shield += quantity;
        }
        else
        {
            _shield = _maxShield;
        }
        EventManager.OnShieldChange?.Invoke(_shield);
        if (_shield <= 0)
        {
            EventManager.OnGameEnd?.Invoke(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 6 &&
            other.gameObject.layer != 27 &&
            other.gameObject.layer != 31 &&
            other.gameObject.layer != 28)
        {
            AudioManager.instance.PlaySoundEffect(WallHitSound, transform, 1f);
        }
    }

}
