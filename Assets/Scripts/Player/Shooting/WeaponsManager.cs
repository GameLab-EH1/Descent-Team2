using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField, Tooltip("Weapons")] private GameObject[] _bullets;
    [SerializeField] private float[] _fireSpeed;
    [SerializeField] private int[] _dmg;
    [SerializeField] private int[] _ammo;
    [SerializeField] private bool[] _unlockedWeapons;
    [HideInInspector] public int WeaponUsing;
    [SerializeField] private Transform[] _shootingPointsW1;
    [SerializeField] private Transform _shootingPointsW2;
    [SerializeField] private Transform _shootingPointsW3;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;
    }


    public int DealDmg()
    {
        return _dmg[WeaponUsing];
    }
    public void ChangeWeapon()
    {
        do
        {
            WeaponUsing++;
        } while (!_unlockedWeapons[WeaponUsing]);
        ObjectPooler.SharedInstance.objectToPool = _bullets[WeaponUsing];
    }
    public void UnlockWeapon(int weaponIndex)
    {
        if (!_unlockedWeapons[weaponIndex])
        {
            _unlockedWeapons[weaponIndex] = true;
        }
    }
    public void Shoot()
    {
        if (WeaponUsing == 0 && _fireSpeed[WeaponUsing] > _timer && _ammo[WeaponUsing] > 0)
        {
            for (int i = 0; i < 2; i++)
            {
                GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
                bullet.transform.position = _shootingPointsW1[i].position;
                bullet.transform.rotation = _shootingPointsW1[i].rotation;
                bullet.SetActive(true);
            }
        }
        else if (WeaponUsing == 1 && _fireSpeed[WeaponUsing] > _timer && _ammo[WeaponUsing] > 0)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
            bullet.transform.position = _shootingPointsW2.position;
            bullet.transform.rotation = _shootingPointsW2.rotation;
            bullet.SetActive(true);
        }
        else if (WeaponUsing == 2 && _fireSpeed[WeaponUsing] > _timer && _ammo[WeaponUsing] > 0)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
            bullet.transform.position = _shootingPointsW3.position;
            bullet.transform.rotation = _shootingPointsW3.rotation;
            bullet.SetActive(true);
        }
        
    }
    
}
