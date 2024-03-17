using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickupScript : MonoBehaviour
{
    [SerializeField] private int _shieldToRecharge;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            EventManager.OnShieldPickOrDmg?.Invoke(_shieldToRecharge);
            Destroy(gameObject);
        }
    }
}
