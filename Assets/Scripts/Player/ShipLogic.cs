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

    private void Start()
    {
        EventManager.OnShieldChange?.Invoke(_shield);
        EventManager.OnPowerChange?.Invoke(_shootPower);
    }
    private void OnEnable()
    {
        EventManager.OnLaserShooting += ShootPowerDecrease;
        EventManager.OnPowerPickup += OnPowerPickedUp;
        EventManager.OnShieldPickOrDmg += ShieldValueChange;
    }
    private void OnDisable()
    {
        EventManager.OnLaserShooting -= ShootPowerDecrease;
        EventManager.OnPowerPickup -= OnPowerPickedUp;
        EventManager.OnShieldPickOrDmg -= ShieldValueChange;
    }

    private void ShootPowerDecrease(bool isFirstW)
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
        }
        else
        {
            _shootPower--;
        }
        EventManager.OnPowerChange?.Invoke(_shootPower);
        if (_shootPower <= 0)
        {
            EventManager.OnLaserNoBullet?.Invoke();
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
    


}
