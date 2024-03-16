using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyRechargeZone : MonoBehaviour
{
    [SerializeField] private float _secToRecharge1;
    private float timer;
    private bool canObtain;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > _secToRecharge1)
        {
            canObtain = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 30 && canObtain)
        {
            EventManager.OnPowerPickup?.Invoke(1);
            timer = 0;
            canObtain = false;
        }
    }
}
