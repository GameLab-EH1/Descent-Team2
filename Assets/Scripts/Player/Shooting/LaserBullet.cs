using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [HideInInspector] public int Dmg;

    
    void Update()
    {
        transform.Translate(Vector3.forward * (_movementSpeed * Time.deltaTime));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30 || other.gameObject.layer == 31)
        {
            return;
        }
        if (other.gameObject.layer == 20)
        {
            BossScript boss = other.transform.GetComponent<BossScript>();
            boss.Hp -= Dmg;
        }
        else if (other.gameObject.layer != 25)
        {
            {
                HealthManager enemy = other.transform.GetComponent<HealthManager>();
                if (enemy != null)
                {
                    enemy.GotDmg(Dmg);
                }
                
                gameObject.SetActive(false);
            }
        }
    }
}
