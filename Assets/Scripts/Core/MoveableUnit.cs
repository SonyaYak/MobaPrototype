using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(WeaponHandler))]

// Note: All moving units will inherit from this class instead of standard Unit
public abstract class MoveableUnit : Unit
{
    protected NavMeshAgent _navMeshAgent;
    protected WeaponHandler _weaponHandler;

    protected override void Awake()
    {
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _weaponHandler = GetComponent<WeaponHandler>();
    }

    public override void Initialize()
    {
        base.Initialize();
        transform.SetParent(null);
        //SetStopDistance();
    }


    protected float GetAttackRange() => _weaponHandler.GetWeapon().GetAttackRange() - 1f;
    protected void SetStopDistance(float distance) => _navMeshAgent.stoppingDistance = distance;

    // NavMeshAgent.SetDestination takes exact 3D coordinates on the baked mesh.
    protected virtual void SetDestination(Vector3 direction) => _navMeshAgent.SetDestination(direction);
}
