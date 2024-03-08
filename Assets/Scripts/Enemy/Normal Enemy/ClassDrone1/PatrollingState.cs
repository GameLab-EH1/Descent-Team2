using UnityEditor;
using UnityEngine;

public class PatrollingState : CurrentState
{
    private ClassDrone1 _classDrone1;
    private Transform _targetPos, _targetPosSaver;
    private float _timer;
    private bool isTimeToMove;

    public PatrollingState(ClassDrone1 classDrone1)
    {
        _classDrone1 = classDrone1;
    }

    public override void EnterState(ClassDrone1 classDrone1)
    {
        isTimeToMove = true;
        int moveIndex = Random.Range(0, classDrone1.PatrollingPoints.Length);
        _targetPos = classDrone1.PatrollingPoints[moveIndex];
        _targetPosSaver = _targetPos;
    }

    public override void UpdateState(ClassDrone1 classDrone1)
    {
        if (isTimeToMove)
        {
            classDrone1.transform.position =
                Vector3.MoveTowards(classDrone1.transform.position, _targetPos.position,
                    classDrone1._scriptableObject.MovementSpeed * Time.deltaTime);
            classDrone1.transform.LookAt(_targetPos);
            if (Vector3.Distance(classDrone1.transform.position, _targetPos.position) < 0.2f)
            {
                isTimeToMove = false;
            }
        }
        else
        {
            _timer += Time.deltaTime;
            if (_timer >= classDrone1._scriptableObject.TimeBeforeMoving)
            {
                while (_targetPos == _targetPosSaver)
                {
                    int moveIndex = Random.Range(0, classDrone1.PatrollingPoints.Length);
                    _targetPos = classDrone1.PatrollingPoints[moveIndex];
                }
                _targetPosSaver = _targetPos;
                isTimeToMove = true;
                _timer = 0;
            }
        }
        if (isPlayerInRange())
        {
            classDrone1.SwitchState(classDrone1.ChasingState);
        }
    }


    private bool isPlayerInRange()
    {
        Vector3 toPlayer = (_classDrone1._ShipController.transform.position - _classDrone1.transform.position);

        if (toPlayer.sqrMagnitude >
            _classDrone1._scriptableObject.VisualRange * _classDrone1._scriptableObject.VisualRange)
        {
            return false;
        }
        toPlayer.Normalize();
        
        float angleToPlayer = Vector3.Angle(_classDrone1.transform.forward, toPlayer);
        
        if (angleToPlayer <= _classDrone1._scriptableObject.VisualAngle && IsPathClear(_classDrone1.transform,
                _classDrone1._ShipController.transform, _classDrone1._scriptableObject.VisualRange))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private bool IsPathClear(Transform self, Transform target, float maxDistance)
    {
        Vector3 direction = target.position - self.position;

        RaycastHit hit;
        if (Physics.Raycast(self.position, direction, out hit, maxDistance + 2, _classDrone1._scriptableObject.PlayerLayer))
        {
            return true;
        }
        return false;
    }
}
