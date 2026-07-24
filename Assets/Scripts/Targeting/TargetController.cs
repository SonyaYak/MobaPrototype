using System;
using UnityEngine;

public abstract class TargetController : MonoBehaviour
{
    [Header("Target Tracking")]
    [SerializeField] private Unit _currentTarget;

    public event Action<Unit> onTargetChanged;

    protected TeamTag _teamTag;

    protected virtual void Awake()
    {
        _teamTag = GetComponent<TeamTag>();
    }

    protected virtual void SetTarget(Unit target)
    {
        if (_currentTarget == target)
            return;

        _currentTarget = target;
        onTargetChanged?.Invoke(target);
    }
}
