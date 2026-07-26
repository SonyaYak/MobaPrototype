using UnityEngine;

public class CripFactory : MonoBehaviour
{
    [Header("Prefab Settings")]
    [SerializeField] private Crip _prefab;
    [SerializeField] private TeamTag _teamTag;

    [Header("Spawn Settings")]
    [SerializeField] private float _timeDelay;
    [SerializeField] private int _amount;

    private float _currentDelay = 0;

    private void Update()
    {
        if (_currentDelay > 0)
        {
            _currentDelay -= Time.deltaTime;
            return;
        }

        // Resets the cooldown timer back to the specified time delay interval
        _currentDelay = _timeDelay;

        for (var i = 0; i < _amount; i++)
        {
            // Spawns a new copy of the prefab and automatically assigns it as a child of this factory
            var crip = Instantiate(_prefab, transform);
            // Activates the object after instantiation
            crip.gameObject.SetActive(true);
            crip.GetTeam().SetTeamId(_teamTag.GetTeamId());
            crip.Initialize();
        }
    }
}
