using System.Collections.Generic;
using UnityEngine;

public class Crip : MoveableUnit, INeedTarget
{
    private AutoTargetController _targetController;
    private Unit _target;

    protected override void Awake()
    {
        base.Awake();
        _targetController = GetComponent<AutoTargetController>();
    }

    public override void Initialize()
    {
        base.Initialize();

        _targetController.onTargetChanged += SetTarget;

        var bases = GamePlayManager.Instance.GetEnemiesBases(_team);
        SetDestination(bases[0].Position);
    }

    public void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (!_target.IsNullOrDefault())
            SetDestination(_target.Position);
    }

    public void SetPotentialTargets(List<Unit> potentialTargets) => _targetController.SetPotentialTargets(potentialTargets);
    public float GetViewDistance() => _targetController.GetViewDistance();
    public void SetTarget(Unit unit) => _target = unit;
}
