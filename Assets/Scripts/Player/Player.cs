using UnityEngine;

[RequireComponent(typeof(UserInputListener))]
[RequireComponent(typeof(SelectionTargetController))]
public class Player : Character
{
    private UserInputListener _userInput;
    private SelectionTargetController _targetController;
    private Unit _target;
    protected override void Awake()
    {
        base.Awake();
        _targetController = GetComponent<SelectionTargetController>();
        _targetController.onTargetChanged += ChangeTarget;
        _userInput = GetComponent<UserInputListener>();
        _userInput.onUnitSelection += _targetController.TrySelectTarget;
        _userInput.onDestinationChanged += InputDestination;
        _userInput.onMovementInput += Movement;
    }
    private void OnDestroy()
    {
        if (_targetController != null)
            _targetController.onTargetChanged -= ChangeTarget;

        if (_userInput != null)
        {
            _userInput.onUnitSelection -= _targetController.TrySelectTarget;
            _userInput.onDestinationChanged -= InputDestination;
            _userInput.onMovementInput -= Movement;
        }
    }
    private void InputDestination(Vector3 obj)
    {
        ResetTarget();
        SetDestination(obj);
    }
    private void ChangeTarget(Unit obj)
    {
        SetStopDistance(GetAttackRange());
        _target = obj;
    }
    private void Movement(Vector2 obj)
    {
        ResetTarget();
        var hor = obj.x * Vector3.right;
        var vert = obj.y * Vector3.forward;
        SetDestination(transform.position + hor + vert);
    }
    private void ResetTarget()
    {
        _targetController.ResetTarget();
        _target = null;
        SetStopDistance(0);
    }
    private void Update()
    {
        if (!_target.IsNullOrDefault())
        {
            SetDestination(_target.Position);
            transform.LookAt(_target.Position);
        }
    }
    public UserInputListener GetInputListener() => _userInput;
    public SelectionTargetController GetTargetController() => _targetController;
}
