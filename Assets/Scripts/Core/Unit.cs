using Unity.VisualScripting;
using UnityEngine;

// Automatically adds Health component and prevents its accidental removal in Inspector
[RequireComponent(typeof(Health))]
// Ensures clicking this object in the Scene view selects the root object instead of children
[SelectionBase]

public abstract class Unit : MonoBehaviour
{
    protected Health _health;

    protected virtual void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void Initialize()
    {
        _health.Initialize();
        _health.onDie += Die;
    }

    private void Die()
    {
        Destroy(gameObject, 1f);
    }

    public Health GetHealth() =>  _health;

    public Vector3 Position => transform.position;
}
