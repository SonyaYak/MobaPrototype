using UnityEngine;
using System.Collections.Generic;

[RequireComponent (typeof(TeamTag))]
public class AutoTargetController : TargetController, INeedTarget
{
    [SerializeField] private float _viewDistance;
    [SerializeField] private List<AttackPriority> _priorities = new List<AttackPriority>()
    {
        new AttackPriority(typeof(Base), 1),
        new AttackPriority(typeof(Crip), 20),
    };

    public void SetPotentialTargets(List<Unit> potentialTargets)
    {
        var mostTarget = GetMostTarget(potentialTargets);
        SetTarget(mostTarget);
    }

    private Unit FindEnemyBase()
    {
        var bases = GamePlayManager.Instance.GetEnemiesBases(_teamTag);
        return bases[0];
    }

    private Unit GetMostTarget(List<Unit> potentialTargets)
    {
        var minDistance = float.MaxValue;
        var maxPriority = float.MinValue;
        Unit mostTarget = default;

        foreach (var target in potentialTargets)
        {
            var priority = GetPriority(target);
            if (priority < maxPriority)
                continue;

            maxPriority = priority;

            var distance = Vector3.Distance(target.Position, Position);

            if (distance >= minDistance)
                continue;

            minDistance = distance;

            mostTarget = target;
        }

        return mostTarget.IsNullOrDefault() ? FindEnemyBase() : mostTarget;
    }

    private int GetPriority(Unit type)
    {
        foreach (var data in _priorities)
        {
            if(data.EnemyType == type.GetType() || type.GetType().InheritsFrom(data.EnemyType))
                return data.Priority;
        }

        return int.MinValue;
    }

    public Vector3 Position => transform.position;

    public float GetViewDistance( ) => _viewDistance;
}
