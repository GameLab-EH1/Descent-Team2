using Unity.VisualScripting;
using UnityEngine;

public abstract class CurrentState
{
    public abstract void EnterState(ClassDrone1 classDrone1);

    public abstract void UpdateState(ClassDrone1 classDrone1);

    
}
