using System;
using UnityEngine;

[Serializable]
public struct AttackPriority
{
    public string Enemy;
    [Tooltip("The one with the highest priority is selected first"), Range(0, 99)] public int Priority;

    public AttackPriority(Type type, int priority)
    {
        Enemy = type.FullName;
        Priority = priority;
    }
   
    public Type EnemyType => Type.GetType(Enemy);
}
