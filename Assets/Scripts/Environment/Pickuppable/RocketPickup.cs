using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPickup : MonoBehaviour
{
    [SerializeField] private bool _isNormal;
    [SerializeField] private int _ammoToCharge;
    private void OnTriggerEnter(Collider other)
    {
        WeaponsManager weaponsManager = other.transform.GetComponent<WeaponsManager>();
        if (weaponsManager != null)
        {
            if (_isNormal)
            {
                weaponsManager.RocketAmmoReload(_ammoToCharge, 0);
            }
            else
            {
                weaponsManager.RocketAmmoReload(_ammoToCharge, 1);
            }
            Destroy(gameObject);
        }
    }
}
