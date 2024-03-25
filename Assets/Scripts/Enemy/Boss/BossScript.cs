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
    [SerializeField] private int _score;
    private ShipController _shipController;
    private bool _isDoorUnlocked;
    private float _timer;

    [Header("Audio")] [SerializeField] private AudioClip _shootingAudio;
    [SerializeField] private AudioClip _dieingAudio, _allarmAudio;
    
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
            EventManager.onScoreChange?.Invoke(_score);
            AudioManager.instance.PlaySoundEffect(_dieingAudio, transform, 1f);
            AudioManager.instance.PlaySoundEffect(_allarmAudio, transform, 1f);
            
            Destroy(gameObject);
        }
    }

    private void DoorUnlocked()
    {
        _isDoorUnlocked = true;
    }

    private bool IsPlayerInRange()
    {
        Vector3 direction = _shipController.transform.position - transform.position;
        float distance = Vector3.Distance(transform.position, _shipController.transform.position) + 10;
        Debug.DrawRay(transform.position, direction, Color.green);
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, distance);
        
        if (hit.transform.gameObject.layer == 30)
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
        AudioManager.instance.PlaySoundEffect(_shootingAudio, transform, 1f);
    }

}
