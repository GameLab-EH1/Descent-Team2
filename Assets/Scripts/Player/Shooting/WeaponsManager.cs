using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField, Tooltip("Weapons")] private GameObject[] _bullets;
    [SerializeField] private float[] _fireDelay;
    public int[] _dmg;
    [SerializeField] private int[] _ammo;
    [SerializeField] private int[] _chargingAmmo;
    [SerializeField] private bool[] _unlockedWeapons;
    
    [SerializeField] private float _minigunRange;
    
    [HideInInspector] public int WeaponUsing;
    
    [SerializeField] private Transform[] _shootingPointsW1;
    [SerializeField] private Transform _shootingPointsW2;
    [SerializeField] private Transform _shootingPointsW3;

    private bool isLaserShootable = true;
    
    private float _timer1;
    private float _timer2;
    private float _timer3;

    private void Start()
    {
        EventManager.OnWeaponSwap?.Invoke(WeaponUsing);
        EventManager.OnShooting?.Invoke(_ammo[WeaponUsing]);
    }
    private void Update()
    {
        _timer1 += Time.deltaTime;
        _timer2 += Time.deltaTime;
        _timer3 += Time.deltaTime;
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
    public void Shoot()
    {
        if (WeaponUsing == 0 && _fireDelay[WeaponUsing] < _timer1 && isLaserShootable)
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
            _timer1 = 0;
            EventManager.OnLaserShooting?.Invoke(true);
        }
        else if (WeaponUsing == 1 && _fireDelay[WeaponUsing] < _timer2 && _ammo[WeaponUsing] > 0)
        {
            RaycastHit hit;
            if (Physics.Raycast(_shootingPointsW2.position, transform.TransformDirection(Vector3.forward), out hit,
                    200))
            {
                HealthManager enemy = hit.transform.GetComponent<HealthManager>();
                if (enemy != null)
                {
                    enemy.GotDmg(_dmg[WeaponUsing]);
                }
            }
            Debug.DrawRay(_shootingPointsW2.position, transform.TransformDirection(Vector3.forward) * 200, Color.cyan);
            
            Debug.Log("sparoArma2");

            _timer2 = 0;
            _ammo[WeaponUsing]--;
            EventManager.OnShooting?.Invoke(_ammo[WeaponUsing]);
        }
        else if (WeaponUsing == 2 && _fireDelay[WeaponUsing] < _timer3 && isLaserShootable)
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
            _timer3 = 0;
        }
        
    }
    private void LaserNotShootable()
    {
        isLaserShootable = false;
    }
    
}
