using UnityEngine;

public class HeroesFactory : MonoBehaviour
{
    [Header("Hero Settings")]
    [SerializeField] private Unit _prefab;
    [SerializeField] private TeamTag _teamTag;

    private Unit _current;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (!_current.IsNullOrDefault() && !_current.GetHealth().IsNullOrDefault())
            _current.GetHealth().onDie -= Spawn;
        var hero = Instantiate(_prefab, transform);
        hero.gameObject.SetActive(true);
        hero.GetTeam().SetTeamId(_teamTag.GetTeamId());
        hero.GetHealth().onDie += Spawn;
        hero.Initialize();
    }
}
