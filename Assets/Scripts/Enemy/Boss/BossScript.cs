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
    private Transform _playerPos;
    private bool _isDoorUnlocked;
    private float _timer;
    
    private void Start()
    {
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
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
    }

    private void DoorUnlocked()
    {
        _isDoorUnlocked = true;
    }

    private bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, _playerPos.position) + 10;
        if (Physics.Raycast(transform.position, _playerPos.position, distance, _playerLayer))
        {
            return true;
        }
        return false;
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPref, transform.position, quaternion.identity);
        Vector3 directionToPlayer = (_playerPos.position - transform.position).normalized;
        bullet.transform.forward = directionToPlayer;
    }

}
