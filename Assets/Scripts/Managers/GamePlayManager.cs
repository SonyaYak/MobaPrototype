using System.Collections.Generic;
using System;
using UnityEngine;


public class GamePlayManager : MonoBehaviour
{
    // Global static reference to this manager (Singleton pattern) 
    // allows any script to access it via GamePlayManager.Instance
    public static GamePlayManager Instance { get; internal set; }

    [Header("Registry Settings")]
    [SerializeField] private int _amountTeams = 2;
    [SerializeField] private List<Unit> _allUnits = new List<Unit>();

    public event Action<Unit> onAdded;
    public event Action<Unit> onRemoved;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        CheckInterceptions();
    }

    private void CheckInterceptions()
    {
        var potentialTargets = new List<Unit>(10);

        for (var i = 1; i <= _amountTeams; i++)
        {
            var units = GetAllAllies(i);
            var enemies = GetAllEnemies(i);

            foreach (var unit in units)
            {
                // Pattern matching: checks if the unit implements INeedTarget 
                // and automatically casts it to the 'attacker' variable if true
                if (unit is INeedTarget attacker)
                {
                    // Clears the reused list to wipe targets found for the previous unit
                    potentialTargets.Clear();
                    var viewDistance = attacker.GetViewDistance();
                    foreach (var enemy in enemies)
                    {
                        if (Vector3.Distance(enemy.Position, unit.Position) <= viewDistance)
                            potentialTargets.Add(enemy);
                    }
                    attacker.SetPotentialTargets(potentialTargets);
                }
            }
        }
    }

    public List<T> Find<T>(Func<T, bool> predicate) where T : Unit
    {
        var result = new List<T>();
        foreach (var unit in _allUnits)
        {
            if (unit is T t)
                if (predicate(t))
                    result.Add (t);
        }

        return result;
    }

    public void Register(Unit unit)
    {
        _allUnits.Add(unit);
        onAdded?.Invoke (unit);
    }

    public void Unregister(Unit unit)
    {
        _allUnits.Remove(unit);
        onRemoved?.Invoke(unit);
    }


    public List<Base> GetEnemiesBases(TeamTag team) => Find<Base>(u => u.GetTeam().GetTeamId() != team.GetTeamId());
    public List<Unit> GetAllEnemies(TeamTag team) => Find<Unit>(u => u.GetTeam().GetTeamId() != team.GetTeamId());
    public List<Unit> GetAllAllies(TeamTag team) => Find<Unit>(u => u.GetTeam().GetTeamId() == team.GetTeamId());
    private List<Base> GetEnemiesBases(int teamId) => Find<Base>(u => u.GetTeam().GetTeamId() != teamId);
    private List<Unit> GetAllEnemies(int teamId) => Find<Unit>(u => u.GetTeam().GetTeamId() != teamId);
    private List<Unit> GetAllAllies(int teamId) => Find<Unit>(u => u.GetTeam().GetTeamId() == teamId);
    public IReadOnlyCollection<Unit> GetAllUnits() => _allUnits;
}
