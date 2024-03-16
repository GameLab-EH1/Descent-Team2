using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBoost : MonoBehaviour
{
    [SerializeField] private int _energyToRecharge;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            EventManager.OnPowerPickup?.Invoke(_energyToRecharge, false);
            Destroy(gameObject);
        }
    }
}
