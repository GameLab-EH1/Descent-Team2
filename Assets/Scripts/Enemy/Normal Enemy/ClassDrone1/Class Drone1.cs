using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassDrone1 : MonoBehaviour
{
    //states
    private CurrentState _currentState;
    public ChasingState _chasingState = new ChasingState();
    public PatrollingState _patrollingState = new PatrollingState();
    
    //Movement
    public Transform[] PatrollingPoints;
    
    //player ref
    public ShipController _ShipController;
    
    //statistics
    private int _hp;
    
    public EnemyScriptableObject _scriptableObject;

    private void Awake()
    {
        _hp = _scriptableObject.Hp;
    }
    private void Start()
    {
        _currentState = _patrollingState;
        _currentState.EnterState(this);
    }
    private void Update()
    {
        _currentState.UpdateState(this);
    }
    public void SwitchState(CurrentState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 31)
        {
            _hp -= _ShipController.BulletDmg[_ShipController.WeaponUsing];
        }
    }
}
