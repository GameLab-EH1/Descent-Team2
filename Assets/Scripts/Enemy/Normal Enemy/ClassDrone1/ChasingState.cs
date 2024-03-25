using UnityEngine;

public class ChasingState : CurrentState
{
    private ClassDrone1 _classDrone1;
    private Vector3 _playerPos, _rotatingAround;
    private bool _isPlayerLost;
    private float _rotTimer, _shootTimer;
    

    public ChasingState(ClassDrone1 classDrone1)
    {
        _classDrone1 = classDrone1;
    }

    public override void EnterState(ClassDrone1 classDrone1)
    {
        _isPlayerLost = false;
        _rotTimer = classDrone1._scriptableObject.RotAroundDelay;
        _playerPos = classDrone1._ShipController.transform.position;
    }

    public override void UpdateState(ClassDrone1 classDrone1)
    {
        _shootTimer += Time.deltaTime;
        LookAtLerped(classDrone1.transform,classDrone1._ShipController.transform, 3f);
        if (isPlayerInRange())
        {
            Shoot();
            if (Vector3.Distance(classDrone1.transform.position, classDrone1._ShipController.transform.position) >
                classDrone1._scriptableObject.StoppingDistance)
            {
                _playerPos = classDrone1._ShipController.transform.position;
                classDrone1.transform.position =
                    Vector3.MoveTowards(classDrone1.transform.position, _playerPos,
                        classDrone1._scriptableObject.MovementSpeed * Time.deltaTime);
            }
            else if(isDirectionClear(classDrone1.transform, -classDrone1._ShipController.transform.position) && 
                    Vector3.Distance(classDrone1.transform.position, classDrone1._ShipController.transform.position) < classDrone1._scriptableObject.StoppingDistance - 1)
            {
                Vector3 oppositeDirection = classDrone1.transform.position - classDrone1._ShipController.transform.position;
                classDrone1.transform.position =
                    Vector3.MoveTowards(classDrone1.transform.position, oppositeDirection,
                        classDrone1._scriptableObject.MovementSpeed * Time.deltaTime);
            }
            if (classDrone1._scriptableObject.IsChasingDrone)
            {
                if (Vector3.Distance(classDrone1.transform.position, classDrone1._ShipController.transform.position) >
                    classDrone1._scriptableObject.StoppingDistance - 1)
                {
                    if (_rotTimer > classDrone1._scriptableObject.RotAroundDelay)
                    {
                        _rotatingAround = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
                        _rotTimer = 0;
                    }
                    else
                    {
                        _rotTimer += Time.deltaTime;
                    }
                    Vector3 moveDirection = _rotatingAround.normalized * (classDrone1._scriptableObject.MovementSpeed * Time.deltaTime * 10);
                    
                    if (Physics.Raycast(classDrone1.transform.position, moveDirection, moveDirection.magnitude))
                    {
                        _rotatingAround = -_rotatingAround;
                    }
                    classDrone1.transform.RotateAround(classDrone1._ShipController.transform.position, _rotatingAround, (classDrone1._scriptableObject.MovementSpeed * Time.deltaTime) * 10);
                }
            }
        }
        else
        {
            Vector3 intermediatePoint = (classDrone1.transform.position + _playerPos) / 2f;
            classDrone1.transform.position =
                Vector3.MoveTowards(classDrone1.transform.position, intermediatePoint,
                    classDrone1._scriptableObject.MovementSpeed * Time.deltaTime);
            if (Vector3.Distance(classDrone1.transform.position, _playerPos) < 0.1)
            {
                _isPlayerLost = true;
            }
        }
        if (_isPlayerLost)
        {
            classDrone1.SwitchState(classDrone1.PatrollingState);
        }
    }
    private void Shoot()
    {
        if (_shootTimer > _classDrone1._scriptableObject.FireDelay)
        {
            _classDrone1.Shoot();
            _shootTimer = 0;
            AudioManager.instance.PlaySoundEffect(_classDrone1._scriptableObject.ShootingAudio, _classDrone1.transform, 1f);
        }
    }
    

    private bool isPlayerInRange()
    {
        Vector3 toPlayer = _classDrone1._ShipController.transform.position - _classDrone1.transform.position;
        float distanceSquared = toPlayer.sqrMagnitude;

        if (distanceSquared > _classDrone1._scriptableObject.VisualRange * _classDrone1._scriptableObject.VisualRange)
        {
            return false;
        }

        toPlayer.Normalize();

        float angleToPlayer = Vector3.Angle(_classDrone1.transform.forward, toPlayer);

        if (angleToPlayer <= _classDrone1._scriptableObject.VisualAngle / 2f)
        {
            if (IsPathClear(_classDrone1.transform, _classDrone1._ShipController.transform, _classDrone1._scriptableObject.VisualRange))
            {
                return true;
            }
        }

        return false;
        }

    
    private bool IsPathClear(Transform self, Transform target, float maxDistance)
    {
        Vector3 direction = target.position - self.position;
        Debug.DrawRay(self.position, direction *200 ,Color.green);
        RaycastHit hit;
        if (Physics.Raycast(self.position, direction, out hit, maxDistance + 2, _classDrone1._scriptableObject.PlayerLayer))
        {
            return true;
        }
        return false;
    }
    private bool isDirectionClear(Transform self, Vector3 direction)
    {
        if (Physics.Raycast(self.position, direction, 2f))
        {
            return false;
        }
        return true;
    }
    private void LookAtLerped(Transform self, Transform target, float t)
    {
        Vector3 relativePos = target.position - self.position;
        Quaternion toRotation = Quaternion.LookRotation(relativePos);
        self.rotation = Quaternion.Lerp(self.rotation, toRotation, t);
    }
}
