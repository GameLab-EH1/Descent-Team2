using UnityEngine;

public class ChasingState : CurrentState
{
    private ClassDrone1 _classDrone1;
    private Vector3 _targetPos;
    private bool _isPlayerLost;

    public ChasingState(ClassDrone1 classDrone1)
    {
        _classDrone1 = classDrone1;
    }

    public override void EnterState(ClassDrone1 classDrone1)
    {
    }

    public override void UpdateState(ClassDrone1 classDrone1)
    {
        if (isPlayerInRange())
        {
            classDrone1.transform.LookAt(classDrone1._ShipController.transform.position);

            if (Vector3.Distance(classDrone1.transform.position, classDrone1._ShipController.transform.position) >
                classDrone1._scriptableObject.StoppingDistance)
            {
                _targetPos = classDrone1._ShipController.transform.position;
                classDrone1.transform.position =
                    Vector3.MoveTowards(classDrone1.transform.position, _targetPos,
                        classDrone1._scriptableObject.MovementSpeed * Time.deltaTime);
            }
        }
        else
        {
            classDrone1.transform.position =
                Vector3.MoveTowards(classDrone1.transform.position, _targetPos,
                    classDrone1._scriptableObject.MovementSpeed * Time.deltaTime);
            if (Vector3.Distance(classDrone1.transform.position, _targetPos) < 0.2)
            {
                _isPlayerLost = true;
            }
        }
        if (_isPlayerLost)
        {
            classDrone1.SwitchState(classDrone1.PatrollingState);
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

        float angleToPlayer = Vector3.Angle(_classDrone1.transform.position, toPlayer);


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
        if (Physics.Raycast(self.position, direction, out hit, maxDistance, _classDrone1._scriptableObject.PlayerLayer))
        {
            return true;
        }
        return false;
    }
}
