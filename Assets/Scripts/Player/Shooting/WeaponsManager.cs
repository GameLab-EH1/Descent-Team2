using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponsManager : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField, Tooltip("Weapons")] private GameObject[] _bullets;
    [SerializeField] private float[] _fireDelay;
    public int[] _dmg;
    [SerializeField] private int[] _ammo;
    [SerializeField] private int[] _chargingAmmo;
    [SerializeField] private bool[] _unlockedWeapons;
    
    [SerializeField] private float _minigunRange;
    [SerializeField] private GameObject _hitEffect;
    
    [HideInInspector] public int WeaponUsing;
    
    [SerializeField] private Transform[] _shootingPointsW1;
    [SerializeField] private Transform _shootingPointsW2;
    [SerializeField] private Transform _shootingPointsW3;

    private bool isLaserShootable = true;
    
    private float _timerW1;
    private float _timerW2;
    private float _timerW3;

    [Header("Rocket")] [SerializeField] private GameObject[] _rockets;
    [SerializeField] private float _rocketDelay;

    [SerializeField] private int[] _dmgRocket;
    private bool _isNormRocket=true;
    private float _timerR;

    private void Start()
    {
        EventManager.OnWeaponSwap?.Invoke(WeaponUsing);
        EventManager.OnShooting?.Invoke(_ammo[WeaponUsing]);
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
    }
    private void OnDisable()
    {
        EventManager.OnLaserNoBullet -= LaserNotShootable;
    }


    public void ChangeWeapon(int weaponIndex, bool isFromJoystic)
    {
        if (isFromJoystic)
        {
            do
            {
                WeaponUsing++;
            } while (!_unlockedWeapons[WeaponUsing]);
        }
        else if (_unlockedWeapons[weaponIndex])
        {
            WeaponUsing = weaponIndex;
        }
        EventManager.OnWeaponSwap?.Invoke(WeaponUsing);
        EventManager.OnShooting?.Invoke(_ammo[WeaponUsing]);
    }
    public void PickUpWeapon(int weaponIndex)
    {
        if (!_unlockedWeapons[weaponIndex])
        {
            _unlockedWeapons[weaponIndex] = true;
        }
        else
        {
            Debug.Log(_ammo[weaponIndex] + " old ammo");
            _ammo[weaponIndex] += _chargingAmmo[weaponIndex];
            Debug.Log(_ammo[weaponIndex] + " new ammo");
        }
    }
    public void ChangeRocket()
    {
        if (_isNormRocket)
        {
            _isNormRocket = false;
        }
        else
        {
            _isNormRocket = true;
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
                bulScript.Dmg = _dmg[WeaponUsing];
                
                bullet.SetActive(true);
                Debug.Log("sparoArma1");
            }
            _timerW1 = 0;
            EventManager.OnLaserShooting?.Invoke(true);
        }
        else if (WeaponUsing == 1 && _fireDelay[WeaponUsing] < _timerW2 && _ammo[WeaponUsing] > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(_shootingPointsW2.position, transform.TransformDirection(Vector3.forward), out hit,
                    _minigunRange))
            {
                HealthManager enemy = hit.transform.GetComponent<HealthManager>();
                if (enemy != null)
                {
                    enemy.GotDmg(_dmg[WeaponUsing]);
                } 
                GameObject effect = Instantiate(_hitEffect, hit.point, quaternion.identity); 
                Destroy(effect , 1f);
                
            }
            Debug.DrawRay(_shootingPointsW2.position, transform.TransformDirection(Vector3.forward) * 200, Color.cyan);
            
            Debug.Log(hit.point);

            _timerW2 = 0;
            _ammo[WeaponUsing]--;
            EventManager.OnShooting?.Invoke(_ammo[WeaponUsing]);
        }
        else if (WeaponUsing == 2 && _fireDelay[WeaponUsing] < _timerW3 && isLaserShootable)
        {
            ObjectPooler.SharedInstance.objectToPool = _bullets[2];
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
            
            bullet.transform.position = _shootingPointsW3.position;
            bullet.transform.rotation = _shootingPointsW3.rotation;
            LaserBullet bulScript = bullet.GetComponent<LaserBullet>();
            bulScript.Dmg = _dmg[WeaponUsing];

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
                GameObject rocket = Instantiate(_rockets[0], _shootingPointsW2.position, _shootingPointsW2.rotation);
                
                rocket.transform.position = _shootingPointsW2.position;
                rocket.transform.rotation = _shootingPointsW2.rotation;
                RocketScript rocScript = rocket.GetComponent<RocketScript>();
                rocScript.Dmg = _dmgRocket[0];
                rocScript.IsNormalRocket = true;
                
                rocket.SetActive(true);
                Debug.Log("rocket1");
            }
            else
            {
                
                GameObject rocket = Instantiate(_rockets[1], _shootingPointsW2.position, _shootingPointsW2.rotation);
                
                RocketScript rocScript = rocket.GetComponent<RocketScript>();
                rocScript.Dmg = _dmgRocket[1];
                rocScript.IsNormalRocket = false;
                
                Debug.Log("rocket2");
            }
            
            _timerR = 0;
        }
    }
    private void LaserNotShootable()
    {
        isLaserShootable = false;
    }
    
}
