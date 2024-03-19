using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Mathematics;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public int Hp;
    [SerializeField] private float _fireDelay;
    [SerializeField] private float _dmg;
    [SerializeField] private GameObject _bulletPref;
    [SerializeField] private LayerMask _playerLayer;
    private ShipController _shipController;
    private bool _isDoorUnlocked;
    private float _timer;
    
    private void Start()
    {
        _shipController = GameObject.FindGameObjectWithTag("Player").GetComponent<ShipController>();
    }
    private void OnEnable()
    {
        EventManager.OnRedKeyPickup += DoorUnlocked;
    }
    private void OnDisable()
    {
        EventManager.OnRedKeyPickup -= DoorUnlocked;
    }

    private void Update()
    {
        if (_isDoorUnlocked)
        {
            _timer += Time.deltaTime;
            if (_timer > _fireDelay)
            {
                if (IsPlayerInRange())
                {
                    Shoot();
                }
            }
        }
        if (Hp <= 0)
        {
            EventManager.OnBossDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    private void DoorUnlocked()
    {
        _isDoorUnlocked = true;
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, _shipController.transform.position) + 10;
        Debug.DrawRay(transform.position, _shipController.transform.position, Color.green);
        if (Physics.Raycast(transform.position, _shipController.transform.position, 200, _playerLayer))
        {
            Debug.Log("player there");
            return true;
        }
        Debug.Log("u dumb");
        return false;
    }

    private void Shoot()
    {
        Debug.Log("piu piu piu");
        GameObject bullet = Instantiate(_bulletPref, transform.position, quaternion.identity);
        Vector3 directionToPlayer = (_shipController.transform.position - transform.position).normalized;
        bullet.transform.forward = directionToPlayer;
    }

}
