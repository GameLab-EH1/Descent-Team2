using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [SerializeField] private string[] _weaponName;
    [SerializeField] private TMP_Text _weaponText;
    [SerializeField] private TMP_Text _ammoText;
    [SerializeField] private TMP_Text _powerText;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _shieldText;
    
    private void OnEnable()
    {
        EventManager.OnWeaponSwap += WeaponSwap;
        EventManager.OnShooting += WeaponAmmo;
        EventManager.OnPowerChange += PowerDecrease;
        EventManager.OnHealthChange += HpChange;
        EventManager.OnShieldChange += ShieldChange;
    }
    private void OnDisable()
    {
        EventManager.OnWeaponSwap -= WeaponSwap;
        EventManager.OnShooting -= WeaponAmmo;
        EventManager.OnPowerChange -= PowerDecrease;
        EventManager.OnHealthChange -= HpChange;
        EventManager.OnShieldChange -= ShieldChange;
    }

    private void WeaponSwap(int weaponIndex)
    {
        _weaponText.text = "You are using: " + _weaponName[weaponIndex];
        if (weaponIndex != 1)
        {
            _ammoText.gameObject.SetActive(false);
        }
        else
        {
            _ammoText.gameObject.SetActive(true);
        }
    }
    public void WeaponAmmo(int quantity)
    {
        _ammoText.text = "Ammo: " + quantity;
    }

    public void PowerDecrease(int quantity)
    {
        _powerText.text = "Laser Power: " + quantity;
    }
    public void HpChange(int quantity)
    {
        _hpText.text = "Hp Value: " + quantity;
    }
    public void ShieldChange(int quantity)
    {
        _shieldText.text = "Shield Value: " + quantity;
    }
}
