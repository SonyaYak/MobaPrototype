using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]

// Note: All moving units will inherit from this class instead of standard Unit
public abstract class MoveableUnit : Unit
{
    protected NavMeshAgent _navMeshAgent;

    protected override void Awake()
    {
        base.Awake();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override void Initialize()
    {
        base.Initialize();
        //SetStopDistance();
    }

    protected void SetStopDistance(float distance) => _navMeshAgent.stoppingDistance = distance;

    // NavMeshAgent.SetDestination takes exact 3D coordinates on the baked mesh.
    protected virtual void SetDestination(Vector3 direction) => _navMeshAgent.SetDestination(direction);
}
