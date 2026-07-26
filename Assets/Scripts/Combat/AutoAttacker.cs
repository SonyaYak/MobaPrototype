using UnityEngine;

[RequireComponent (typeof(TargetController))]
[RequireComponent(typeof(WeaponHandler))]

public class AutoAttacker : MonoBehaviour
{
    [Header("Current Target")]
    [SerializeField] private Unit _currentTarget;

    private WeaponHandler _weaponHandler;
    private TargetController _targetController;
    private float _currentDelay;

    protected virtual void Awake()
    {
        _weaponHandler = GetComponent<WeaponHandler>();
        _targetController = GetComponent<TargetController>();
        _targetController.onTargetChanged += Change;
    }
    protected virtual void OnDestroy()
    {
        if (_targetController != null)
        {
            _targetController.onTargetChanged -= Change;
        }
    }

    protected virtual void Update()
    {
        if (_currentTarget == null)
            return;

        if (Vector3.Distance(_currentTarget.Position, Position) > Weapon.GetAttackRange())
            return;

        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }

        _currentDelay = Weapon.GetAttackInterval();
        _currentTarget.GetHealth().Damage(Weapon.GetDamageValue());
    }

    public Vector3 Position => transform.position;
    public float GetAttackRange() => Weapon.GetAttackRange();
    private void Change(Unit obj) => _currentTarget = obj;
    private Weapon Weapon => _weaponHandler.GetWeapon();
}
