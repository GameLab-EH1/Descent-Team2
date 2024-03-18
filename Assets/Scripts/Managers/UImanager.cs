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
    [SerializeField] private TMP_Text _shieldText;
    [SerializeField] private TMP_Text _scoreText;
    private int _score;

    
    private void OnEnable()
    {
        EventManager.OnWeaponSwap += WeaponSwap;
        EventManager.OnShooting += WeaponAmmo;
        EventManager.OnPowerChange += PowerDecrease;
        EventManager.OnShieldChange += ShieldChange;
        EventManager.onScoreChange += ScoreChange;
    }
    private void OnDisable()
    {
        EventManager.OnWeaponSwap -= WeaponSwap;
        EventManager.OnShooting -= WeaponAmmo;
        EventManager.OnPowerChange -= PowerDecrease;
        EventManager.OnShieldChange -= ShieldChange;
        EventManager.onScoreChange -= ScoreChange;
    }

    private void WeaponSwap(int weaponIndex)
    {
        _weaponText.text = _weaponName[weaponIndex];
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
        _ammoText.text = quantity.ToString();
    }

    private void PowerDecrease(int quantity)
    {
        _powerText.text = quantity.ToString();
    }
    private void ShieldChange(int quantity)
    {
        _shieldText.text = quantity.ToString();
    }
    
    private void ScoreChange(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = scoreToAdd.ToString();
    }
}
