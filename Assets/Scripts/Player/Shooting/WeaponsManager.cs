using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponsManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField, Tooltip("Weapons")] private GameObject[] _bullets;
    [SerializeField] private float[] _fireDelay;
    public int[] Dmg;
    public int[] Ammo;
    [SerializeField] private int[] _chargingAmmo;
    [SerializeField] private bool[] _unlockedWeapons;
    
    [SerializeField] private float _minigunRange;
    [SerializeField] private GameObject _hitEffect;
    
    [HideInInspector] public int WeaponUsing;
    
    [SerializeField] private Transform[] _shootingPointsW1;
    [SerializeField] private Transform _shootingPointsW2;
    [SerializeField] private Transform _shootingPointsW3;

    [SerializeField] private int _maxPowerUpLvlCounter;
    [SerializeField] private int _dmgAddedOnPowerLvlUp;
    private int _currentPowerUpLvl;

    private bool isLaserShootable = true;
    
    private float _timerW1;
    private float _timerW2;
    private float _timerW3;

    [Header("Rocket")] [SerializeField] private GameObject[] _rockets;
    [SerializeField] private float _rocketDelay;
    [SerializeField] private int[] _rocketAmmo;
    
    [SerializeField] private int[] _dmgRocket;
    private bool _isNormRocket=true;
    private float _timerR;

    [Header("Audio")] [SerializeField] private AudioClip _minigunAudio;
    [SerializeField] private AudioClip _rocketAudio;

    private void Start()
    {
        EventManager.OnWeaponSwap?.Invoke(WeaponUsing);
        EventManager.OnShooting?.Invoke(Ammo[WeaponUsing]);
        EventManager.onScoreChange?.Invoke(0);
        EventManager.OnFireRocket?.Invoke(_rocketAmmo[0]);
    }
    private void Update()
    {
        _timerW1 += Time.deltaTime;
        _timerW2 += Time.deltaTime;
        _timerW3 += Time.deltaTime;
        _timerR += Time.deltaTime;
    }
    private void OnEnable()
    {
        EventManager.OnLaserNoBullet += LaserNotShootable;
        EventManager.OnPowerBoostLvl += PowerLvlUp;
    }
    private void OnDisable()
    {
        EventManager.OnLaserNoBullet -= LaserNotShootable;
        EventManager.OnPowerBoostLvl -= PowerLvlUp;
    }


    public void ChangeWeapon(int weaponIndex, bool isFromJoystic)
    {
        if (isFromJoystic)
        {
            do
            {
                if (WeaponUsing == 2)
                {
                    WeaponUsing = 0;
                }
                else
                {
                    WeaponUsing++;
                }
            } while (!_unlockedWeapons[WeaponUsing]);
        }
        else if (_unlockedWeapons[weaponIndex])
        {
            WeaponUsing = weaponIndex;
        }
        EventManager.OnWeaponSwap?.Invoke(WeaponUsing);
        EventManager.OnShooting?.Invoke(Ammo[WeaponUsing]);
    }
    public void PickUpWeapon(int weaponIndex)
    {
        if (!_unlockedWeapons[weaponIndex])
        {
            _unlockedWeapons[weaponIndex] = true;
        }
        else
        {
            Debug.Log(Ammo[weaponIndex] + " old ammo");
            Ammo[weaponIndex] += _chargingAmmo[weaponIndex];
            Debug.Log(Ammo[weaponIndex] + " new ammo");
        }
    }
    public void ChangeRocket()
    {
        if (_isNormRocket)
        {
            _isNormRocket = false;
            EventManager.OnChangingRocket?.Invoke(_rocketAmmo[1]);
        }
        else
        {
            _isNormRocket = true;
            EventManager.OnChangingRocket?.Invoke(_rocketAmmo[0]);
        }
    }
    public void Shoot()
    {
        if (WeaponUsing == 0 && _fireDelay[WeaponUsing] < _timerW1 && isLaserShootable)
        {
            ObjectPooler.SharedInstance.objectToPool = _bullets[0];
            for (int i = 0; i < 2; i++)
            {
                GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
                
                bullet.transform.position = _shootingPointsW1[i].position;
                bullet.transform.rotation = _shootingPointsW1[i].rotation;
                LaserBullet bulScript = bullet.GetComponent<LaserBullet>();
                bulScript.Dmg = Dmg[WeaponUsing];
                
                bullet.SetActive(true);
                Debug.Log("sparoArma1");
            }
            _timerW1 = 0;
            EventManager.OnLaserShooting?.Invoke(true);
        }
        else if (WeaponUsing == 1 && _fireDelay[WeaponUsing] < _timerW2 && Ammo[WeaponUsing] > 0)
        {
            RaycastHit hit;
            int pickuppableLayer = ~(1 << LayerMask.NameToLayer("Pickuppable"));
            if (Physics.Raycast(_shootingPointsW2.position, transform.TransformDirection(Vector3.forward), out hit,
                    _minigunRange, pickuppableLayer))
            {
                HealthManager enemy = hit.transform.GetComponent<HealthManager>();
                if (enemy != null)
                {
                    enemy.GotDmg(Dmg[WeaponUsing]);
                }
                SecretDoorScript door = hit.transform.GetComponent<SecretDoorScript>();
                if (door != null)
                {
                    door.OpenWithMinigun();
                }
                AudioManager.instance.PlaySoundEffect(_minigunAudio, transform, 1f);
                GameObject effect = Instantiate(_hitEffect, hit.point, quaternion.identity); 
                Destroy(effect , 1f);
                
            }
            Debug.DrawRay(_shootingPointsW2.position, transform.TransformDirection(Vector3.forward) * 200, Color.cyan);
            
            Debug.Log(hit.point);

            _timerW2 = 0;
            Ammo[WeaponUsing]--;
            EventManager.OnShooting?.Invoke(Ammo[WeaponUsing]);
        }
        else if (WeaponUsing == 2 && _fireDelay[WeaponUsing] < _timerW3 && isLaserShootable)
        {
            GameObject bullet = Instantiate(_bullets[WeaponUsing]);
            
            bullet.transform.position = _shootingPointsW3.position;
            bullet.transform.rotation = _shootingPointsW3.rotation;
            LaserBullet bulScript = bullet.GetComponent<LaserBullet>();
            bulScript.Dmg = Dmg[WeaponUsing];

            bullet.SetActive(true);
            EventManager.OnLaserShooting?.Invoke(false);
            Debug.Log("sparoArma3");
            _timerW3 = 0;
        }
        
    }

    public void ShootRocket()
    {
        if (_timerR > _rocketDelay)
        {
            if (_isNormRocket)
            {
                if (_rocketAmmo[0] > 0)
                {
                    GameObject rocket = Instantiate(_rockets[0], _shootingPointsW2.position, _shootingPointsW2.rotation);
                    
                    rocket.transform.position = _shootingPointsW2.position;
                    rocket.transform.rotation = _shootingPointsW2.rotation;
                    RocketScript rocScript = rocket.GetComponent<RocketScript>();
                    rocScript.Dmg = _dmgRocket[0];
                    rocScript.IsNormalRocket = true;
                    
                    rocket.SetActive(true);
                    Debug.Log("rocket1");
                    _rocketAmmo[0]--;
                    EventManager.OnFireRocket?.Invoke(_rocketAmmo[0]);
                    AudioManager.instance.PlaySoundEffect(_rocketAudio, transform, 1f);
                }
                else
                {
                    Debug.Log("Finito i rocket 1");
                }
            }
            else
            {
                if (_rocketAmmo[1] > 0)
                {
                    GameObject rocket = Instantiate(_rockets[1], _shootingPointsW2.position, _shootingPointsW2.rotation);
                    
                    RocketScript rocScript = rocket.GetComponent<RocketScript>();
                    rocScript.Dmg = _dmgRocket[1];
                    rocScript.IsNormalRocket = false;
                    Debug.Log("rocket2");
                    _rocketAmmo[1]--;
                    EventManager.OnFireRocket?.Invoke(_rocketAmmo[1]);
                    AudioManager.instance.PlaySoundEffect(_rocketAudio, transform, 1f);
                }
                else
                {
                    Debug.Log("Finito i rocket 2");
                }
            }
            
            _timerR = 0;
        }
    }
    public void RocketAmmoReload(int AmmoToRecharge, int rocIndex)
    {
        _rocketAmmo[rocIndex] += AmmoToRecharge;
        if (rocIndex == 0 && _isNormRocket)
        {
            EventManager.OnFireRocket?.Invoke(_rocketAmmo[rocIndex]);
        }
        else if (rocIndex == 1 && !_isNormRocket)
        {
            EventManager.OnFireRocket?.Invoke(_rocketAmmo[rocIndex]);
        }
    }
    
    private void PowerLvlUp()
    {
        if (_currentPowerUpLvl < _maxPowerUpLvlCounter)
        {
            Dmg[0] += _dmgAddedOnPowerLvlUp;
            _currentPowerUpLvl++;
        }
    }
    
    private void LaserNotShootable()
    {
        isLaserShootable = false;
    }
    
}
