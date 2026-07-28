using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    [Header("Faction Settings")]
    [SerializeField] private TeamTag _teamTag;

    [Header("Healing Values")]
    [SerializeField] private int _healAmount = 15;
    [SerializeField] private float _healInterval = 1.0f;

    private readonly List<Health> _unitsInZone = new List<Health>();
    private Coroutine _healingCoroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out var unitHealth))
        {
            TeamTag unitTeam = other.GetComponent<TeamTag>();

            if (unitTeam != null && unitTeam.GetTeamId() == _teamTag.GetTeamId())
            {
                _unitsInZone.Add(unitHealth);

                Debug.Log($"[HEALING] {other.name} ENTERED the healing zone of team {_teamTag.GetTeamId()}!");

                if (_healingCoroutine == null)
                {
                    _healingCoroutine = StartCoroutine(HealUnitsOverTime());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Health>(out var unitHealth))
        {
            if (_unitsInZone.Contains(unitHealth))
            {
                _unitsInZone.Remove(unitHealth);
                Debug.Log($"[HEALING] {other.name} LEFT the healing zone of team {_teamTag.GetTeamId()}!");
            }
        }
    }

    private IEnumerator HealUnitsOverTime()
    {
        while (_unitsInZone.Count > 0)
        {
            _unitsInZone.RemoveAll(item => item == null);

            foreach (var unit in _unitsInZone)
            {
                if (unit != null && unit.GetHealth() < unit.GetMaxHealth())
                {
                    unit.Heal(_healAmount);
                }
            }

            yield return new WaitForSeconds(_healInterval);
        }

        _healingCoroutine = null;
    }
}
