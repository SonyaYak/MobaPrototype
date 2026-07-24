using UnityEngine;

public class Crip : MoveableUnit, INeedTarget
{
    private Unit _target;

    public void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (_target != null)
            SetDestination(_target.Position);
    }

    public void SetTarget(Unit unit) => _target = unit;
}
