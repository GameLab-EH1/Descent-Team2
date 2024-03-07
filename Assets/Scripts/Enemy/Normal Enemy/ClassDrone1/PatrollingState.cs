using UnityEngine;

public class PatrollingState : CurrentState
{
    private ClassDrone1 _classDrone1;
    private Transform _targetPos;
    private float _timer;
    private bool isTimeToMove;

    public PatrollingState(ClassDrone1 classDrone1)
    {
        _classDrone1 = classDrone1;
    }
    
    public override void EnterState(ClassDrone1 classDrone1)
    {
        isTimeToMove = true;
    }

    public override void UpdateState(ClassDrone1 classDrone1)
    {
        
        if (isTimeToMove)
        {
            int moveIndex = Random.Range(0, classDrone1.PatrollingPoints.Length + 1);
            _targetPos = classDrone1.PatrollingPoints[moveIndex];
            
            classDrone1.transform.position = 
                Vector3.MoveTowards(classDrone1.transform.position, _targetPos.position, classDrone1._scriptableObject.MovementSpeed * Time.deltaTime);
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
                isTimeToMove = true;
            }
        }
        if(isPlayerInRange(classDrone1))
        {
            classDrone1.SwitchState(classDrone1.ChasingState);
        }
    }
    

    private bool isPlayerInRange(ClassDrone1 classDrone1)
    {
        Vector3 toPlayer = (classDrone1._ShipController.transform.position - classDrone1.transform.position);
        
        if (
            toPlayer.sqrMagnitude > classDrone1._scriptableObject.VisualRange * classDrone1._scriptableObject.VisualRange
            )
        {
            return false;
        }
        toPlayer.Normalize();
        
        float angleToPlayer = Vector3.Angle(classDrone1.transform.position, toPlayer);


        if (angleToPlayer <= classDrone1._scriptableObject.VisualAngle && IsPathClear(classDrone1.transform, classDrone1._ShipController.transform, classDrone1._scriptableObject.VisualRange))
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

