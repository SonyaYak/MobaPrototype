using UnityEngine;

public class Base : Unit
{
    protected override void Awake()
    {
        base.Awake();
        Initialize();
    }
}
