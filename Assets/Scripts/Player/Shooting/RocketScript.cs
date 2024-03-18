using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RocketScript : MonoBehaviour
{
    
    [HideInInspector] public int Dmg;
    [HideInInspector] public bool IsNormalRocket;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private LayerMask _targetLayer;

    private Transform target;
    
    void Update()
    {
        if (IsNormalRocket)
        {
            transform.Translate(Vector3.forward * (_movementSpeed * Time.deltaTime));
            Debug.Log("helloscemo2");
        }
        else
        {
            FindTarget();

            if (target != null)
            {
                Vector3 direction = target.position - transform.position;
                
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, _rotateSpeed * Time.deltaTime);
                Debug.Log("helloscemo");
            }
            transform.Translate(Vector3.forward * _movementSpeed * Time.deltaTime);
            Debug.Log("helloscemo1");
        }
    }
    void FindTarget()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, 10f, _targetLayer);

        if (targetsInViewRadius.Length > 0)
        {
            float shortestDistance = Mathf.Infinity;
            Transform nearestTarget = null;

            foreach (Collider targetCollider in targetsInViewRadius)
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetCollider.transform.position);
                if (distanceToTarget < shortestDistance)
                {
                    shortestDistance = distanceToTarget;
                    nearestTarget = targetCollider.transform;
                }
            }
            
            target = nearestTarget;
        }
        else
        {
            target = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 30)
        {
            {
                HealthManager enemy = other.transform.GetComponent<HealthManager>();
                if (enemy != null)
                {
                    enemy.GotDmg(Dmg);
                }
                
                Destroy(gameObject);
            }
        }
    }
    
}

