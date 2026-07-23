
using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;

    public event Action onDie;
    public Action<int> onChanged;

    // Flag to prevent duplicate death triggers in the same frame
    private bool _dying;

    public void Initialize()
    {
        _currentHealth = _maxHealth;
    }

    private void SetHealth(int value)
    {
        if (_dying)
            return;

        // Mathf.Clamp keeps the value strictly between 0 and _maxHealth
        _currentHealth =  Mathf.Clamp(value, 0, _maxHealth);
        onChanged?.Invoke(_currentHealth);

        if (_currentHealth == 0)
            Die();
    }

    private void Die()
    {
        _dying = true;
        onDie?.Invoke();
    }

    public void Damage(int decrease) => SetHealth( _currentHealth - decrease);

    public void Heal(int increase) => SetHealth( _currentHealth + increase);

    public int GetHealth() => _currentHealth;

    public int GetMaxHealth() => _maxHealth;
}
