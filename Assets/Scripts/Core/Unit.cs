using Unity.VisualScripting;
using UnityEngine;

// Automatically adds Health component and prevents its accidental removal in Inspector
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(TeamTag))]
// Ensures clicking this object in the Scene view selects the root object instead of children
[SelectionBase]

public abstract class Unit : MonoBehaviour
{
    protected Health _health;
    protected TeamTag _team;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
        _team = GetComponent<TeamTag>();
    }

    public void Initialize()
    {
        _health.Initialize();
        _health.onDie += Die;
        GamePlayManager.Instance.Register(this);
    }

    private void Die()
    {
        GamePlayManager.Instance.Unregister(this);
        Destroy(gameObject, .1f);
    }

    public TeamTag GetTeam() => _team;
    public Health GetHealth() =>  _health;

    public Vector3 Position => transform.position;
}
