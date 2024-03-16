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
    [SerializeField] private TMP_Text _scoreText;
    private int _score;

    
    private void OnEnable()
    {
        EventManager.OnWeaponSwap += WeaponSwap;
        EventManager.OnShooting += WeaponAmmo;
        EventManager.OnPowerChange += PowerDecrease;
        EventManager.OnHealthChange += HpChange;
        EventManager.OnShieldChange += ShieldChange;
        EventManager.onScoreChange += ScoreChange;
    }
    private void OnDisable()
    {
        EventManager.OnWeaponSwap -= WeaponSwap;
        EventManager.OnShooting -= WeaponAmmo;
        EventManager.OnPowerChange -= PowerDecrease;
        EventManager.OnHealthChange -= HpChange;
        EventManager.OnShieldChange -= ShieldChange;
        EventManager.onScoreChange -= ScoreChange;
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
    private void WeaponAmmo(int quantity)
    {
        _ammoText.text = "Ammo: " + quantity;
    }

    private void PowerDecrease(int quantity)
    {
        _powerText.text = "Laser Power: " + quantity;
    }
    private void HpChange(int quantity)
    {
        _hpText.text = "Hp Value: " + quantity;
    }
    private void ShieldChange(int quantity)
    {
        _shieldText.text = "Shield Value: " + quantity;
    }
    
    private void ScoreChange(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = "Score: " + _score;
    }
}
