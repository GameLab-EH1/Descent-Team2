using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClassDrone1 : MonoBehaviour
{
    //states
    private CurrentState _currentState;
    public PatrollingState PatrollingState;
    public ChasingState ChasingState;
    
    //Movement
    public Transform[] PatrollingPoints;
    
    //player ref
    public ShipController _ShipController;
    
    //statistics
    private int _hp;
    
    public EnemyScriptableObject _scriptableObject;

    private void Awake()
    {
        ChasingState = new ChasingState(this);
        PatrollingState = new PatrollingState(this);
        _hp = _scriptableObject.Hp;
    }
    private void Start()
    {
        _currentState = PatrollingState;
        _currentState.EnterState(this);
    }
    private void Update()
    {
        _currentState.UpdateState(this);
        if (_hp <= 0)
        {
            Debug.Log("i'm dead");
            
        }
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
            _hp -= _ShipController.DealingDmg();
        }
    }
}
