using System;
using UnityEngine;

[Serializable]
public class Weapon
{
    [Header("Weapon Stats")]
    [SerializeField] private float _attackRange;
    [SerializeField] private int _damageValue;
    [SerializeField] private float _attackInterval;

    public float GetAttackRange() => _attackRange;
    public int GetDamageValue() => _damageValue;
    public float GetAttackInterval() => _attackInterval;
}
