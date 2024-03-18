using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    [Header("Ui Component")] [SerializeField]
    private GameObject[] _shields, _energy;
    [SerializeField] private GameObject _redKey;
    
    [Header("TMP Things")]
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
        EventManager.OnRedKeyPickup += RedKeyActivation;
    }
    private void OnDisable()
    {
        EventManager.OnWeaponSwap -= WeaponSwap;
        EventManager.OnShooting -= WeaponAmmo;
        EventManager.OnPowerChange -= PowerDecrease;
        EventManager.OnShieldChange -= ShieldChange;
        EventManager.onScoreChange -= ScoreChange;
        EventManager.OnRedKeyPickup -= RedKeyActivation;
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
        if (quantity > 80)
        {
            for (int i = 0; i < _energy.Length; i++)
            {
                _energy[i].SetActive(true);
            }
        }else if (quantity > 60)
        {
            for (int i = 0; i < _energy.Length - 1; i++)
            {
                _energy[i].SetActive(true);
            }
            for (int i = _energy.Length - 1; i < _energy.Length; i++)
            {
                _energy[i].SetActive(false);
            }
        }else if (quantity > 40)
        {
            for (int i = 0; i < _energy.Length - 2; i++)
            {
                _energy[i].SetActive(true);
            }
            for (int i = _energy.Length - 2; i < _energy.Length; i++)
            {
                _energy[i].SetActive(false);
            }
        }else if (quantity > 20)
        {
            for (int i = 0; i < _energy.Length - 3; i++)
            {
                _energy[i].SetActive(true);
            }
            for (int i = _energy.Length - 3; i < _energy.Length; i++)
            {
                _energy[i].SetActive(false);
            }
        }else
        {
            for (int i = 0; i < _energy.Length - 4; i++)
            {
                _energy[i].SetActive(true);
            }
            for (int i = _energy.Length - 4; i < _energy.Length; i++)
            {
                _energy[i].SetActive(false);
            }
        }
    }
    private void ShieldChange(int quantity)
    {
        _shieldText.text = quantity.ToString();
        if (quantity > 75)
        {
            for (int i = 0; i < _shields.Length; i++)
            {
                _shields[i].SetActive(true);
            }
        }else if (quantity > 50)
        {
            for (int i = 0; i < _shields.Length - 1; i++)
            {
                _shields[i].SetActive(true);
            }
            for (int i = _shields.Length - 1; i < _shields.Length; i++)
            {
                _shields[i].SetActive(false);
            }
        }else if (quantity > 25)
        {
            for (int i = 0; i < _shields.Length - 2; i++)
            {
                _shields[i].SetActive(true);
            }
            for (int i = _shields.Length - 2; i < _shields.Length; i++)
            {
                _shields[i].SetActive(false);
            }
        }else
        {
            for (int i = 0; i < _shields.Length - 3; i++)
            {
                _shields[i].SetActive(true);
            }
            for (int i = _shields.Length - 3; i < _shields.Length; i++)
            {
                _shields[i].SetActive(false);
            }
        }
    }

    private void RedKeyActivation()
    {
        _redKey.gameObject.SetActive(true);
    }
    
    private void ScoreChange(int scoreToAdd)
    {
        _score += scoreToAdd;
        _scoreText.text = _score.ToString();
    }
}
