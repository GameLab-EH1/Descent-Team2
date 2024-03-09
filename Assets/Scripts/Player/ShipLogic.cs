using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLogic : MonoBehaviour
{
    [SerializeField] private int _hp;
    [SerializeField] private int _shield;
    
    [SerializeField] private int _shootPower;
    private int _shootPowerSaver;

    [SerializeField] private UImanager _uiManager;

    private void Start()
    {
        EventManager.OnHealthChange?.Invoke(_hp);
        EventManager.OnShieldChange?.Invoke(_shield);
        EventManager.OnPowerChange?.Invoke(_shootPower);
    }
    private void OnEnable()
    {
        EventManager.OnLaserShooting += ShootPowerDecrease;
    }
    private void OnDisable()
    {
        EventManager.OnLaserShooting -= ShootPowerDecrease;
    }

    private void ShootPowerDecrease(bool isFirstW)
    {
        if (isFirstW)
        {
            if (_shootPowerSaver == 3)
            {
                _shootPower--;
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
    


}
